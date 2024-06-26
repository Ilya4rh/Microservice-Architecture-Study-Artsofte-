﻿using org.apache.zookeeper;

namespace ExampleCore.ZookeperSemaforLogic;

public class WaitCompletionSource(TaskCompletionSource<bool> waitCompletionSource) : Watcher
{
    public override Task process(WatchedEvent @event)
    {
        if (@event.getState() == Event.KeeperState.SyncConnected)
            waitCompletionSource.TrySetResult(true);

        return Task.CompletedTask;
    }
}