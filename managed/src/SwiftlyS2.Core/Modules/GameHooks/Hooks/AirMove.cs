using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate void CCSPlayerMovementServicesAirMove( nint movementServices, nint moveData );

    internal static Guid HookAirMove()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::AirMove");
        if (ptr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::AirMove.");

        var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesAirMove>(ptr);
        return unmanagedFunction.AddHook(next =>
        {
            return ( movementServices, moveData ) =>
            {
                _dummyPawnComponent.DangerousSetHandle(movementServices);
                var player = _dummyPawnComponent.ToPlayer();
                if (player == null) { next()(movementServices, moveData); return; }

                var preCtx = new AirMoveMovementPreContext {
                    Params = new AirMoveMovementParams {
                        Player = player,
                        MoveData = new CMoveDataImpl { Address = moveData }
                    }
                };

                InvokeAirMovePre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal) return;

                next()(movementServices, moveData);

                var postCtx = new AirMoveMovementPostContext { Params = preCtx.Params };

                InvokeAirMovePost(ref postCtx);
            };
        });
    }

    internal static Guid UnhookAirMove()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.AirMove, out var hookId))
        {
            var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::AirMove");
            if (ptr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::AirMove.");

            var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesAirMove>(ptr);
            unmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeAirMovePre( ref AirMoveMovementPreContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeAirMovePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeAirMovePost( ref AirMoveMovementPostContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeAirMovePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }
}
