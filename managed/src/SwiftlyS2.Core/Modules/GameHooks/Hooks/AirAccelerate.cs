using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private unsafe delegate void CCSPlayerMovementServicesAirAccelerate( nint movementServices, nint moveData, Vector* wishDirection, float wishSpeed, float acceleration );

    internal static unsafe Guid HookAirAccelerate()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::AirAccelerate");
        if (ptr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::AirAccelerate.");

        var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesAirAccelerate>(ptr);
        return unmanagedFunction.AddHook(next =>
        {
            return ( movementServices, moveData, wishDirection, wishSpeed, acceleration ) =>
            {
                _dummyPawnComponent.DangerousSetHandle(movementServices);
                var player = _dummyPawnComponent.ToPlayer();
                if (player == null) { next()(movementServices, moveData, wishDirection, wishSpeed, acceleration); return; }

                var preCtx = new AirAccelerateMovementPreContext {
                    Params = new AirAccelerateMovementParams {
                        Player = player,
                        MoveData = new CMoveDataImpl { Address = moveData },
                        WishDirection = wishDirection != null ? *wishDirection : default,
                        WishSpeed = wishSpeed,
                        Acceleration = acceleration
                    }
                };

                InvokeAirAcceleratePre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal) return;

                if (wishDirection != null) *wishDirection = preCtx.Params.WishDirection;
                next()(movementServices, moveData, wishDirection, preCtx.Params.WishSpeed, preCtx.Params.Acceleration);

                var postCtx = new AirAccelerateMovementPostContext { Params = preCtx.Params };

                InvokeAirAcceleratePost(ref postCtx);
            };
        });
    }

    internal static unsafe Guid UnhookAirAccelerate()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.AirAccelerate, out var hookId))
        {
            var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::AirAccelerate");
            if (ptr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::AirAccelerate.");

            var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesAirAccelerate>(ptr);
            unmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeAirAcceleratePre( ref AirAccelerateMovementPreContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeAirAcceleratePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeAirAcceleratePost( ref AirAccelerateMovementPostContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeAirAcceleratePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }
}
