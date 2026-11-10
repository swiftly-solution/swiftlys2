using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate byte CCSPlayerMovementServicesPlayerMove( nint movementServices, nint moveData );

    internal static Guid HookPlayerMove()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::PlayerMove");
        if (ptr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::PlayerMove.");

        var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesPlayerMove>(ptr);
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

                var preCtx = new PlayerMoveMovementPreContext {
                    Params = new PlayerMoveMovementParams {
                        Player = player,
                        MoveData = moveDataImpl
                    }
                };

                InvokePlayerMovePre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                {
                    moveDataImpl.Address = 0;
                    _moveDataPool.Return(moveDataImpl);
                    return preCtx.IsReturnSet ? (preCtx.Return ? (byte)1 : (byte)0) : (byte)0;
                }

                var result = next()(movementServices, moveData);

                var postCtx = new PlayerMoveMovementPostContext { Params = preCtx.Params, Return = result != 0 };

                InvokePlayerMovePost(ref postCtx);

                moveDataImpl.Address = 0;
                _moveDataPool.Return(moveDataImpl);
                return postCtx.Return ? (byte)1 : (byte)0;
            };
        });
    }

    internal static Guid UnhookPlayerMove()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.PlayerMove, out var hookId))
        {
            var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::PlayerMove");
            if (ptr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::PlayerMove.");

            var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesPlayerMove>(ptr);
            unmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokePlayerMovePre( ref PlayerMoveMovementPreContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnPlayerMoveMovementPreDelegate>.Snapshot))
        {
            subscriber.InvokePlayerMovePre(ref ctx);
            if (ctx.HookResult is HookResult.Stop or HookResult.Handled) return;
        }
    }

    internal static void InvokePlayerMovePost( ref PlayerMoveMovementPostContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnPlayerMoveMovementPostDelegate>.Snapshot))
        {
            subscriber.InvokePlayerMovePost(ref ctx);
            if (ctx.HookResult is HookResult.Stop or HookResult.Handled) return;
        }
    }
}
