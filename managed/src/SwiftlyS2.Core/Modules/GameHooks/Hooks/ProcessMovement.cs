using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate void CCSPlayerMovementServicesProcessMovement( nint movementServices, nint moveData );

    internal static Guid HookProcessMovement()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var ProcessMovementPtr = _core.GameData.GetSignature("CCSPlayer_MovementServices::ProcessMovement");
        if (ProcessMovementPtr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::ProcessMovement.");

        var ProcessMovementUmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesProcessMovement>(ProcessMovementPtr);
        return ProcessMovementUmanagedFunction.AddHook(next =>
        {
            return ( movementServices, moveData ) =>
            {
                _dummyPawnComponent.DangerousSetHandle(movementServices);
                var player = _dummyController.ToPlayer();
                if (player == null) { next()(movementServices, moveData); return; }

                var preCtx = new ProcessMovementMovementPreContext {
                    Params = new ProcessMovementMovementParams {
                        Player = player,
                        MoveData = new CMoveDataImpl { Address = moveData }
                    }
                };

                InvokeProcessMovementPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal) return;

                next()(movementServices, moveData);

                var postCtx = new ProcessMovementMovementPostContext { Params = preCtx.Params };

                InvokeProcessMovementPost(ref postCtx);
            };
        });
    }

    internal static Guid UnhookProcessMovement()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.ProcessMovement, out var hookId))
        {
            var ProcessMovementPtr = _core.GameData.GetSignature("CCSPlayer_MovementServices::ProcessMovement");
            if (ProcessMovementPtr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::ProcessMovement.");

            var ProcessMovementUmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesProcessMovement>(ProcessMovementPtr);

            ProcessMovementUmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeProcessMovementPre( ref ProcessMovementMovementPreContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeProcessMovementPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeProcessMovementPost( ref ProcessMovementMovementPostContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeProcessMovementPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }
}
