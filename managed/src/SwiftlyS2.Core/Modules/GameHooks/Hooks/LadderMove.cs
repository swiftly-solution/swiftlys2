using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate byte CCSPlayerMovementServicesLadderMove( nint movementServices, nint moveData );

    internal static Guid HookLadderMove()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::LadderMove");
        if (ptr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::LadderMove.");

        var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesLadderMove>(ptr);
        return unmanagedFunction.AddHook(next =>
        {
            return ( movementServices, moveData ) =>
            {
                var dummy = _pawnComponentPool.Rent();
                dummy.DangerousSetHandle(movementServices);
                var player = dummy.ToPlayer();
                _pawnComponentPool.Return(dummy);
                if (player == null) return next()(movementServices, moveData);

                var moveDataImpl = _moveDataPool.Rent();
                moveDataImpl.Address = moveData;

                var preCtx = new LadderMoveMovementPreContext {
                    Params = new LadderMoveMovementParams {
                        Player = player,
                        MoveData = moveDataImpl
                    }
                };

                InvokeLadderMovePre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                {
                    moveDataImpl.Address = 0;
                    _moveDataPool.Return(moveDataImpl);
                    return preCtx.IsReturnSet ? (preCtx.Return ? (byte)1 : (byte)0) : (byte)0;
                }

                var result = next()(movementServices, moveData);

                var postCtx = new LadderMoveMovementPostContext { Params = preCtx.Params, Return = result != 0 };

                InvokeLadderMovePost(ref postCtx);

                moveDataImpl.Address = 0;
                _moveDataPool.Return(moveDataImpl);
                return postCtx.Return ? (byte)1 : (byte)0;
            };
        });
    }

    internal static Guid UnhookLadderMove()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.LadderMove, out var hookId))
        {
            var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::LadderMove");
            if (ptr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::LadderMove.");

            var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesLadderMove>(ptr);
            unmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeLadderMovePre( ref LadderMoveMovementPreContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnLadderMoveMovementPreDelegate>.Snapshot))
        {
            subscriber.InvokeLadderMovePre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }

    internal static void InvokeLadderMovePost( ref LadderMoveMovementPostContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnLadderMoveMovementPostDelegate>.Snapshot))
        {
            subscriber.InvokeLadderMovePost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }
}
