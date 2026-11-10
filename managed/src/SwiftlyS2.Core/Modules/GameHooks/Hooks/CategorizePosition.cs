using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate void CCSPlayerMovementServicesCategorizePosition( nint movementServices, nint moveData, byte stayOnGround );

    internal static Guid HookCategorizePosition()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::CategorizePosition");
        if (ptr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::CategorizePosition.");

        var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesCategorizePosition>(ptr);
        return unmanagedFunction.AddHook(next =>
        {
            return ( movementServices, moveData, stayOnGround ) =>
            {
                var dummy = _pawnComponentPool.Rent();
                dummy.DangerousSetHandle(movementServices);
                var player = dummy.ToPlayer();
                _pawnComponentPool.Return(dummy);
                if (player == null) { next()(movementServices, moveData, stayOnGround); return; }

                var moveDataImpl = _moveDataPool.Rent();
                moveDataImpl.Address = moveData;

                var preCtx = new CategorizePositionMovementPreContext {
                    Params = new CategorizePositionMovementParams {
                        Player = player,
                        MoveData = moveDataImpl,
                        StayOnGround = stayOnGround != 0
                    }
                };

                InvokeCategorizePositionPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                {
                    moveDataImpl.Address = 0;
                    _moveDataPool.Return(moveDataImpl);
                    return;
                }

                next()(movementServices, moveData, (byte)(preCtx.Params.StayOnGround ? 1 : 0));

                var postCtx = new CategorizePositionMovementPostContext { Params = preCtx.Params };

                InvokeCategorizePositionPost(ref postCtx);

                moveDataImpl.Address = 0;
                _moveDataPool.Return(moveDataImpl);
            };
        });
    }

    internal static Guid UnhookCategorizePosition()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.CategorizePosition, out var hookId))
        {
            var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::CategorizePosition");
            if (ptr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::CategorizePosition.");

            var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesCategorizePosition>(ptr);
            unmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeCategorizePositionPre( ref CategorizePositionMovementPreContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnCategorizePositionMovementPreDelegate>.Snapshot))
        {
            subscriber.InvokeCategorizePositionPre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }

    internal static void InvokeCategorizePositionPost( ref CategorizePositionMovementPostContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnCategorizePositionMovementPostDelegate>.Snapshot))
        {
            subscriber.InvokeCategorizePositionPost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }
}
