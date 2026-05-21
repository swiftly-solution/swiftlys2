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
                _dummyPawnComponent.DangerousSetHandle(movementServices);
                var player = _dummyPawnComponent.ToPlayer();
                if (player == null) { next()(movementServices, moveData); return; }

                var preCtx = new WaterMoveMovementPreContext {
                    Params = new WaterMoveMovementParams {
                        Player = player,
                        MoveData = new CMoveDataImpl { Address = moveData }
                    }
                };

                InvokeWaterMovePre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal) return;

                next()(movementServices, moveData);

                var postCtx = new WaterMoveMovementPostContext { Params = preCtx.Params };

                InvokeWaterMovePost(ref postCtx);
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
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeWaterMovePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeWaterMovePost( ref WaterMoveMovementPostContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeWaterMovePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }
}
