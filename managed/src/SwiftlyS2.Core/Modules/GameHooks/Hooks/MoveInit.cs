using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate byte CCSPlayerMovementServicesMoveInit( nint movementServices, nint moveData );

    internal static Guid HookMoveInit()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::MoveInit");
        if (ptr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::MoveInit.");

        var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesMoveInit>(ptr);
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

                var preCtx = new MoveInitMovementPreContext {
                    Params = new MoveInitMovementParams {
                        Player = player,
                        MoveData = moveDataImpl
                    }
                };

                InvokeMoveInitPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                {
                    moveDataImpl.Address = 0;
                    _moveDataPool.Return(moveDataImpl);
                    return preCtx.IsReturnSet ? (preCtx.Return ? (byte)1 : (byte)0) : (byte)0;
                }

                var result = next()(movementServices, moveData);

                var postCtx = new MoveInitMovementPostContext { Params = preCtx.Params, Return = result != 0 };

                InvokeMoveInitPost(ref postCtx);

                moveDataImpl.Address = 0;
                _moveDataPool.Return(moveDataImpl);
                return postCtx.Return ? (byte)1 : (byte)0;
            };
        });
    }

    internal static Guid UnhookMoveInit()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.MoveInit, out var hookId))
        {
            var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::MoveInit");
            if (ptr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::MoveInit.");

            var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesMoveInit>(ptr);
            unmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeMoveInitPre( ref MoveInitMovementPreContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnMoveInitMovementPreDelegate>.Snapshot))
        {
            subscriber.InvokeMoveInitPre(ref ctx);
            if (ctx.HookResult is HookResult.Stop or HookResult.Handled) return;
        }
    }

    internal static void InvokeMoveInitPost( ref MoveInitMovementPostContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnMoveInitMovementPostDelegate>.Snapshot))
        {
            subscriber.InvokeMoveInitPost(ref ctx);
            if (ctx.HookResult is HookResult.Stop or HookResult.Handled) return;
        }
    }
}
