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

                var preCtx = new SimulateUserCommandsPreContext { Params = new SimulateUserCommandsParams { Player = player } };

                InvokeSimulateUserCommandsPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal) return;

                next()(controller);

                var postCtx = new SimulateUserCommandsPostContext { Params = preCtx.Params };

                InvokeSimulateUserCommandsPost(ref postCtx);
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

    internal static void InvokeSimulateUserCommandsPre( ref SimulateUserCommandsPreContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeSimulateUserCommandsPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeSimulateUserCommandsPost( ref SimulateUserCommandsPostContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeSimulateUserCommandsPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }
}
