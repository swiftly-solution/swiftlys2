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

                IAirAccelerateMovement @event = new AirAccelerateMovementData {
                    Player = player,
                    MoveData = new CMoveDataImpl { Address = moveData },
                    WishDirectionPtr = (nint)wishDirection,
                    WishSpeed = wishSpeed,
                    Acceleration = acceleration,
                    Result = HookResult.Continue
                };

                InvokeAirAcceleratePre(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.CancelOriginal) return;

                next()(movementServices, moveData, wishDirection, @event.WishSpeed, @event.Acceleration);

                @event.Result = HookResult.Continue;

                InvokeAirAcceleratePost(ref @event);
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

    internal static void InvokeAirAcceleratePre( ref IAirAccelerateMovement @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeAirAcceleratePre(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeAirAcceleratePost( ref IAirAccelerateMovement @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeAirAcceleratePost(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.Handled) return;
            }
        }
    }
}
