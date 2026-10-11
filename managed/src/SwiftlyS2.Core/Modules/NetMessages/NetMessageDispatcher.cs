using System.Collections.Concurrent;
using SwiftlyS2.Core.Events;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.NetMessages;

internal enum NetMessageHookType
{
    Server = 0,
    Client = 1,
    ServerInternal = 2
}

internal static class NetMessageDispatcher
{
    private const int CustomHudClickedId = (int)ECstrike15UserMessages.CS_UM_CustomHudClicked;

    private static readonly ConcurrentDictionary<int, List<NetMessageHookCallback>>[] callbacks = CreateCallbackMaps();

    private static readonly Lock writeLock = new();

    internal static void Initialize()
    {
        NativeNetMessages.RegisterMessageHook((int)NetMessageHookType.Client, CustomHudClickedId);
    }

    internal static void Register( NetMessageHookCallback callback )
    {
        lock (writeLock)
        {
            var map = callbacks[(int)callback.HookType];
            if (map.TryGetValue(callback.MessageId, out var existing))
            {
                map[callback.MessageId] = [.. existing, callback];
                return;
            }

            map[callback.MessageId] = [callback];
            NativeNetMessages.RegisterMessageHook((int)callback.HookType, callback.MessageId);
        }
    }

    internal static void Unregister( NetMessageHookCallback callback )
    {
        lock (writeLock)
        {
            var map = callbacks[(int)callback.HookType];
            if (!map.TryGetValue(callback.MessageId, out var existing) || !existing.Contains(callback))
            {
                return;
            }

            var remaining = existing.FindAll(other => !ReferenceEquals(other, callback));
            if (remaining.Count != 0)
            {
                map[callback.MessageId] = remaining;
                return;
            }

            _ = map.TryRemove(callback.MessageId, out _);

            if (callback.HookType == NetMessageHookType.Client && callback.MessageId == CustomHudClickedId)
            {
                return;
            }

            NativeNetMessages.UnregisterMessageHook((int)callback.HookType, callback.MessageId);
        }
    }

    internal static int DispatchClientMessage( int playerId, int msgId, nint pMessage )
    {
        var result = Dispatch(NetMessageHookType.Client, msgId, (playerId, pMessage),
            static ( callback, state ) => callback.InvokeAsClient(state.playerId, state.pMessage));

        if (result == (int)HookResult.Continue && msgId == CustomHudClickedId)
        {
            EventPublisher.InvokeOnCustomHudClicked(playerId, pMessage);
        }

        return result;
    }

    internal static int DispatchServerMessage( nint pPlayerMask, int msgId, nint pMessage ) =>
        Dispatch(NetMessageHookType.Server, msgId, (pPlayerMask, pMessage),
            static ( callback, state ) => callback.InvokeAsServer(state.pPlayerMask, state.pMessage));

    internal static int DispatchServerInternalMessage( int playerId, int msgId, nint pMessage ) =>
        Dispatch(NetMessageHookType.ServerInternal, msgId, (playerId, pMessage),
            static ( callback, state ) => callback.InvokeAsServerInternal(state.playerId, state.pMessage));

    private static int Dispatch<TState>( NetMessageHookType type, int msgId, TState state, Func<NetMessageHookCallback, TState, HookResult> invoke )
    {
        if (!callbacks[(int)type].TryGetValue(msgId, out var messageCallbacks))
        {
            return (int)HookResult.Continue;
        }

        var cancelOriginal = false;
        foreach (var callback in messageCallbacks)
        {
            switch (invoke(callback, state))
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

    private static ConcurrentDictionary<int, List<NetMessageHookCallback>>[] CreateCallbackMaps()
    {
        var maps = new ConcurrentDictionary<int, List<NetMessageHookCallback>>[Enum.GetValues<NetMessageHookType>().Length];
        for (var i = 0; i < maps.Length; i++)
        {
            maps[i] = new();
        }

        return maps;
    }
}
