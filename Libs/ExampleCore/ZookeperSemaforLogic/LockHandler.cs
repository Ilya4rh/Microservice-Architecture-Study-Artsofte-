namespace ExampleCore.ZookeperSemaforLogic;

public class LockHandler(DistributedSemaphore semaphore, string nodePath) : IAsyncDisposable
{
    public async ValueTask DisposeAsync()
    {
        await semaphore.ReleaseAsync(nodePath).ConfigureAwait(false);
    }
}