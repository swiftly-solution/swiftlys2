using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate void CBasePlayerControllerSimulateUserCommands( nint controller );

    internal static Guid HookSimulateUserCommands()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var simulateUserCommandsPtr = _core.GameData.GetSignature("CBasePlayerController::OnSimulateUserCommands");
        if (simulateUserCommandsPtr == 0)
            throw new InvalidOperationException("Failed to find signature for CBasePlayerController::OnSimulateUserCommands.");

        var simulateUserCommandsUmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBasePlayerControllerSimulateUserCommands>(simulateUserCommandsPtr);
        return simulateUserCommandsUmanagedFunction.AddHook(next =>
        {
            return ( controller ) =>
            {
                _dummyController.DangerousSetHandle(controller);
                var player = _dummyController.ToPlayer();
                if (player == null) { next()(controller); return; }

                ISimulateUserCommandsController @event = new SimulateUserCommands {
                    Player = player,
                    Result = HookResult.Continue
                };

                InvokeSimulateUserCommandsPre(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.CancelOriginal) return;

                next()(controller);

                @event.Result = HookResult.Continue;

                InvokeSimulateUserCommandsPost(ref @event);
            };
        });
    }

    internal static Guid UnhookSimulateUserCommands()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.SimulateUserCommands, out var hookId))
        {
            var simulateUserCommandsPtr = _core.GameData.GetSignature("CBasePlayerController::OnSimulateUserCommands");
            if (simulateUserCommandsPtr == 0)
                throw new InvalidOperationException("Failed to find signature for CBasePlayerController::OnSimulateUserCommands.");

            var simulateUserCommandsUmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBasePlayerControllerSimulateUserCommands>(simulateUserCommandsPtr);

            simulateUserCommandsUmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeSimulateUserCommandsPre( ref ISimulateUserCommandsController @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeSimulateUserCommandsPre(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeSimulateUserCommandsPost( ref ISimulateUserCommandsController @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeSimulateUserCommandsPost(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.Handled) return;
            }
        }
    }
}
