using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate void CCSPlayerMovementServicesWaterMove( nint movementServices, nint moveData );

    internal static Guid HookWaterMove()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::WaterMove");
        if (ptr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::WaterMove.");

        var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesWaterMove>(ptr);
        return unmanagedFunction.AddHook(next =>
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

                var preCtx = new WaterMoveMovementPreContext {
                    Params = new WaterMoveMovementParams {
                        Player = player,
                        MoveData = moveDataImpl
                    }
                };

                InvokeWaterMovePre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                {
                    moveDataImpl.Address = 0;
                    _moveDataPool.Return(moveDataImpl);
                    return;
                }

                next()(movementServices, moveData);

                var postCtx = new WaterMoveMovementPostContext { Params = preCtx.Params };

                InvokeWaterMovePost(ref postCtx);

                moveDataImpl.Address = 0;
                _moveDataPool.Return(moveDataImpl);
            };
        });
    }

    internal static Guid UnhookWaterMove()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.WaterMove, out var hookId))
        {
            var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::WaterMove");
            if (ptr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::WaterMove.");

            var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesWaterMove>(ptr);
            unmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeWaterMovePre( ref WaterMoveMovementPreContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnWaterMoveMovementPreDelegate>.Snapshot))
        {
            subscriber.InvokeWaterMovePre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }

    internal static void InvokeWaterMovePost( ref WaterMoveMovementPostContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnWaterMoveMovementPostDelegate>.Snapshot))
        {
            subscriber.InvokeWaterMovePost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }
}
