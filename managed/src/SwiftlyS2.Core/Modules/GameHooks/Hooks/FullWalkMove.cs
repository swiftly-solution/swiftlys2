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
                _dummyPawnComponent.DangerousSetHandle(movementServices);
                var player = _dummyPawnComponent.ToPlayer();
                if (player == null) { next()(movementServices, moveData, ground); return; }

                var preCtx = new FullWalkMoveMovementPreContext {
                    Params = new FullWalkMoveMovementParams {
                        Player = player,
                        MoveData = new CMoveDataImpl { Address = moveData },
                        Ground = ground != 0
                    }
                };

                InvokeFullWalkMovePre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal) return;

                next()(movementServices, moveData, (byte)(preCtx.Params.Ground ? 1 : 0));

                var postCtx = new FullWalkMoveMovementPostContext { Params = preCtx.Params };

                InvokeFullWalkMovePost(ref postCtx);
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
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeFullWalkMovePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeFullWalkMovePost( ref FullWalkMoveMovementPostContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeFullWalkMovePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }
}
