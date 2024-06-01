using System.Text;
using org.apache.zookeeper;

namespace ExampleCore.ZookeperSemaforLogic;

public class DistributedSemaphore(ZooKeeper zooKeeper, string semaphorePath, int maxCount)
{
    private static readonly IReadOnlyList<byte> acquiredMarker;
    
    public async Task<LockHandler> AcquireAsync(TimeOutValue timeout, CancellationToken cancellationToken = default)
    {
        string nodePath;
        var timeoutSource = new CancellationTokenSource(timeout.TimeSpan);

        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                nodePath = await zooKeeper.createAsync(semaphorePath + "/lock-", null, ZooDefs.Ids.OPEN_ACL_UNSAFE,
                        CreateMode.EPHEMERAL_SEQUENTIAL)
                    .ConfigureAwait(false);

                var children = await zooKeeper.getChildrenAsync(semaphorePath, false)
                    .ConfigureAwait(false);

                var sortedChildren = await SequentialPathHelper.FilterAndSortAsync(
                    parentNode: semaphorePath,
                    childrenNames: children.Children,
                    prefix: "semaphore-",
                    zooKeeper,
                    alternatePrefix: null
                ).ConfigureAwait(false);


                var state = new State(nodePath, sortedChildren);

                var currentNodeIndex = Array.FindIndex(state.SortedChildren, t => t.Path == state.EphemeralNodePath);

                if (currentNodeIndex < maxCount)
                {
                    await zooKeeper.setDataAsync(nodePath, acquiredMarker.ToArray()).ConfigureAwait(false);

                    return new LockHandler(this, nodePath);
                }
                
                var waitCompletionSource = new TaskCompletionSource<bool>();

                await using var timeoutRegistration = timeoutSource.Token.Register(c => 
                        ((TaskCompletionSource<bool>)c).TrySetResult(false), waitCompletionSource);
                await using var cancellationRegistration = cancellationToken.Register(c => 
                        ((TaskCompletionSource<bool>)c).TrySetCanceled(), waitCompletionSource);

                var watcher = new WaitCompletionSource(waitCompletionSource);
                
                if (!waitCompletionSource.Task.IsCompleted && await WaitAsync(zooKeeper, watcher, state))
                    waitCompletionSource.TrySetResult(true);

                if (!await waitCompletionSource.Task.ConfigureAwait(false))
                    return null;
            }
            finally
            {
                timeoutSource.Dispose();
            }
        }
    }

    private async Task<bool> WaitAsync(ZooKeeper zooKeeper, Watcher watcher, State state)
    {
        var index = Array.FindIndex(state.SortedChildren, t => t.Path == state.EphemeralNodePath);

        if (index == maxCount)
        {
            var childNames = new HashSet<string>((await zooKeeper
                .getChildrenAsync(semaphorePath, watcher)
                .ConfigureAwait(false)).Children);
            
            return state.SortedChildren.Take(index)
                .Any(t => !childNames
                .Contains(t.Path[(t.Path.LastIndexOf('/') + 1)..]));
        }

        var nextLowestChildData = await zooKeeper.getDataAsync(state.SortedChildren[index - 1].Path, watcher)
                .ConfigureAwait(false);

        return nextLowestChildData.Data.SequenceEqual(acquiredMarker);
    }

    public async Task ReleaseAsync(string nodePath) => await zooKeeper.deleteAsync(nodePath).ConfigureAwait(false);

    public record State(string EphemeralNodePath, (string Path, int SequenceNumber, string Prefix)[] SortedChildren);
}