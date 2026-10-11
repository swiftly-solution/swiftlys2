using System.Collections.Concurrent;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameEvents;

internal static class GameEventDispatcher
{
    private static readonly ConcurrentDictionary<uint, List<GameEventCallback>> callbacks = new();
    private static readonly Lock writeLock = new();

    internal static void Register( GameEventCallback callback )
    {
        lock (writeLock)
        {
            if (callbacks.TryGetValue(callback.EventHash, out var existing))
            {
                callbacks[callback.EventHash] = [.. existing, callback];
                return;
            }

            callbacks[callback.EventHash] = [callback];
            NativeGameEvents.RegisterListener(callback.EventName);
        }
    }

    internal static void Unregister( GameEventCallback callback )
    {
        lock (writeLock)
        {
            if (!callbacks.TryGetValue(callback.EventHash, out var existing) || !existing.Contains(callback))
            {
                return;
            }

            var remaining = existing.FindAll(other => !ReferenceEquals(other, callback));
            if (remaining.Count != 0)
            {
                callbacks[callback.EventHash] = remaining;
                return;
            }

            _ = callbacks.TryRemove(callback.EventHash, out _);
            NativeGameEvents.UnregisterListener(callback.EventName);
        }
    }

    internal static int DispatchPre( uint hash, nint pEvent, nint pDontBroadcast ) =>
        Dispatch(hash, pEvent, pDontBroadcast, pre: true);

    internal static int DispatchPost( uint hash, nint pEvent, nint pDontBroadcast ) =>
        Dispatch(hash, pEvent, pDontBroadcast, pre: false);

    private static int Dispatch( uint hash, nint pEvent, nint pDontBroadcast, bool pre )
    {
        if (!callbacks.TryGetValue(hash, out var eventCallbacks))
        {
            return (int)HookResult.Continue;
        }

        var cancelOriginal = false;
        foreach (var callback in eventCallbacks)
        {
            if (callback.IsPreHook != pre)
            {
                continue;
            }

            switch (callback.Invoke(pEvent, pDontBroadcast))
            {
                case HookResult.Stop:
                    return (int)HookResult.Stop;
                case HookResult.Handled:
                    return (int)HookResult.Handled;
                case HookResult.CancelOriginal:
                    cancelOriginal = true;
                    break;
            }
        }

        return (int)(cancelOriginal ? HookResult.CancelOriginal : HookResult.Continue);
    }
}
