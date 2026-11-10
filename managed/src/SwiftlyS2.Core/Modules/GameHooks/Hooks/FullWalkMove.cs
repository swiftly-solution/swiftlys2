using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate void CCSPlayerMovementServicesFullWalkMove( nint movementServices, nint moveData, byte ground );

    internal static Guid HookFullWalkMove()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::FullWalkMove");
        if (ptr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::FullWalkMove.");

        var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesFullWalkMove>(ptr);
        return unmanagedFunction.AddHook(next =>
        {
            return ( movementServices, moveData, ground ) =>
            {
                var dummy = _pawnComponentPool.Rent();
                dummy.DangerousSetHandle(movementServices);
                var player = dummy.ToPlayer();
                _pawnComponentPool.Return(dummy);
                if (player == null) { next()(movementServices, moveData, ground); return; }

                var moveDataImpl = _moveDataPool.Rent();
                moveDataImpl.Address = moveData;

                var preCtx = new FullWalkMoveMovementPreContext {
                    Params = new FullWalkMoveMovementParams {
                        Player = player,
                        MoveData = moveDataImpl,
                        Ground = ground != 0
                    }
                };

                InvokeFullWalkMovePre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                {
                    moveDataImpl.Address = 0;
                    _moveDataPool.Return(moveDataImpl);
                    return;
                }

                next()(movementServices, moveData, (byte)(preCtx.Params.Ground ? 1 : 0));

                var postCtx = new FullWalkMoveMovementPostContext { Params = preCtx.Params };

                InvokeFullWalkMovePost(ref postCtx);

                moveDataImpl.Address = 0;
                _moveDataPool.Return(moveDataImpl);
            };
        });
    }

    internal static Guid UnhookFullWalkMove()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.FullWalkMove, out var hookId))
        {
            var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::FullWalkMove");
            if (ptr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::FullWalkMove.");

            var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesFullWalkMove>(ptr);
            unmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeFullWalkMovePre( ref FullWalkMoveMovementPreContext ctx )
    {
        var subs = GetSubscribers(HookListener.FullWalkMove);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeFullWalkMovePre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }

    internal static void InvokeFullWalkMovePost( ref FullWalkMoveMovementPostContext ctx )
    {
        var subs = GetSubscribers(HookListener.FullWalkMove);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeFullWalkMovePost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }
}
