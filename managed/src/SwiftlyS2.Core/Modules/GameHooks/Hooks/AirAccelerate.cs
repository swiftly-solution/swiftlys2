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
                var dummy = _pawnComponentPool.Rent();
                dummy.DangerousSetHandle(movementServices);
                var player = dummy.ToPlayer();
                _pawnComponentPool.Return(dummy);
                if (player == null) { next()(movementServices, moveData, wishDirection, wishSpeed, acceleration); return; }

                var moveDataImpl = _moveDataPool.Rent();
                moveDataImpl.Address = moveData;

                var preCtx = new AirAccelerateMovementPreContext {
                    Params = new AirAccelerateMovementParams {
                        Player = player,
                        MoveData = moveDataImpl,
                        WishDirection = wishDirection != null ? *wishDirection : default,
                        WishSpeed = wishSpeed,
                        Acceleration = acceleration
                    }
                };

                InvokeAirAcceleratePre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                {
                    moveDataImpl.Address = 0;
                    _moveDataPool.Return(moveDataImpl);
                    return;
                }

                if (wishDirection != null) *wishDirection = preCtx.Params.WishDirection;
                next()(movementServices, moveData, wishDirection, preCtx.Params.WishSpeed, preCtx.Params.Acceleration);

                var postCtx = new AirAccelerateMovementPostContext { Params = preCtx.Params };

                InvokeAirAcceleratePost(ref postCtx);

                moveDataImpl.Address = 0;
                _moveDataPool.Return(moveDataImpl);
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
        foreach (var subscriber in Volatile.Read(ref Listeners<OnAirAccelerateMovementPreDelegate>.Snapshot))
        {
            subscriber.InvokeAirAcceleratePre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }

    internal static void InvokeAirAcceleratePost( ref AirAccelerateMovementPostContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnAirAccelerateMovementPostDelegate>.Snapshot))
        {
            subscriber.InvokeAirAcceleratePost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }
}
