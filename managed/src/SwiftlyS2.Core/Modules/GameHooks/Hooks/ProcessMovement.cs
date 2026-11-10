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
                var dummy = _pawnComponentPool.Rent();
                dummy.DangerousSetHandle(movementServices);
                var player = dummy.ToPlayer();
                _pawnComponentPool.Return(dummy);
                if (player == null) { next()(movementServices, moveData); return; }

                var moveDataImpl = _moveDataPool.Rent();
                moveDataImpl.Address = moveData;

                var preCtx = new ProcessMovementMovementPreContext {
                    Params = new ProcessMovementMovementParams {
                        Player = player,
                        MoveData = moveDataImpl
                    }
                };

                InvokeProcessMovementPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                {
                    moveDataImpl.Address = 0;
                    _moveDataPool.Return(moveDataImpl);
                    return;
                }

                next()(movementServices, moveData);

                var postCtx = new ProcessMovementMovementPostContext { Params = preCtx.Params };

                InvokeProcessMovementPost(ref postCtx);

                moveDataImpl.Address = 0;
                _moveDataPool.Return(moveDataImpl);
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
        foreach (var subscriber in Volatile.Read(ref Listeners<OnProcessMovementMovementPreDelegate>.Snapshot))
        {
            subscriber.InvokeProcessMovementPre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }

    internal static void InvokeProcessMovementPost( ref ProcessMovementMovementPostContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnProcessMovementMovementPostDelegate>.Snapshot))
        {
            subscriber.InvokeProcessMovementPost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }
}
