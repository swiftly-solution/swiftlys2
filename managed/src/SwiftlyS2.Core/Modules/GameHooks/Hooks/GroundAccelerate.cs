using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private unsafe delegate void CCSPlayerMovementServicesGroundAccelerateWindows( nint movementServices, nint moveData, float frameTime, Vector* wishDirection, float wishSpeed, float acceleration );
    private unsafe delegate void CCSPlayerMovementServicesGroundAccelerateLinux( nint movementServices, nint moveData, Vector* wishDirection, float frameTime, float wishSpeed, float acceleration );

    internal static unsafe Guid HookGroundAccelerate()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::GroundAccelerate");
        if (ptr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::GroundAccelerate.");

        if (IsWindows)
        {
            var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesGroundAccelerateWindows>(ptr);
            return unmanagedFunction.AddHook(next =>
            {
                return ( movementServices, moveData, frameTime, wishDirection, wishSpeed, acceleration ) =>
                {
                    _dummyPawnComponent.DangerousSetHandle(movementServices);
                    var player = _dummyPawnComponent.ToPlayer();
                    if (player == null) { next()(movementServices, moveData, frameTime, wishDirection, wishSpeed, acceleration); return; }

                    IGroundAccelerateMovement @event = new GroundAccelerateMovementData {
                        Player = player,
                        MoveData = new CMoveDataImpl { Address = moveData },
                        WishDirectionPtr = (nint)wishDirection,
                        FrameTime = frameTime,
                        WishSpeed = wishSpeed,
                        Acceleration = acceleration,
                        Result = HookResult.Continue
                    };

                    InvokeGroundAcceleratePre(ref @event);
                    if (@event.Result == HookResult.Stop || @event.Result == HookResult.CancelOriginal) return;

                    next()(movementServices, moveData, frameTime, wishDirection, @event.WishSpeed, @event.Acceleration);

                    @event.Result = HookResult.Continue;

                    InvokeGroundAcceleratePost(ref @event);
                };
            });
        }
        else
        {
            var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesGroundAccelerateLinux>(ptr);
            return unmanagedFunction.AddHook(next =>
            {
                return ( movementServices, moveData, wishDirection, frameTime, wishSpeed, acceleration ) =>
                {
                    _dummyPawnComponent.DangerousSetHandle(movementServices);
                    var player = _dummyPawnComponent.ToPlayer();
                    if (player == null) { next()(movementServices, moveData, wishDirection, frameTime, wishSpeed, acceleration); return; }

                    IGroundAccelerateMovement @event = new GroundAccelerateMovementData {
                        Player = player,
                        MoveData = new CMoveDataImpl { Address = moveData },
                        WishDirectionPtr = (nint)wishDirection,
                        FrameTime = frameTime,
                        WishSpeed = wishSpeed,
                        Acceleration = acceleration,
                        Result = HookResult.Continue
                    };

                    InvokeGroundAcceleratePre(ref @event);
                    if (@event.Result == HookResult.Stop || @event.Result == HookResult.CancelOriginal) return;

                    next()(movementServices, moveData, wishDirection, frameTime, @event.WishSpeed, @event.Acceleration);

                    @event.Result = HookResult.Continue;

                    InvokeGroundAcceleratePost(ref @event);
                };
            });
        }
    }

    internal static unsafe Guid UnhookGroundAccelerate()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.GroundAccelerate, out var hookId))
        {
            var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::GroundAccelerate");
            if (ptr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::GroundAccelerate.");

            if (IsWindows)
            {
                var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesGroundAccelerateWindows>(ptr);
                unmanagedFunction.RemoveHook(hookId);
            }
            else
            {
                var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesGroundAccelerateLinux>(ptr);
                unmanagedFunction.RemoveHook(hookId);
            }
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeGroundAcceleratePre( ref IGroundAccelerateMovement @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeGroundAcceleratePre(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeGroundAcceleratePost( ref IGroundAccelerateMovement @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeGroundAcceleratePost(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.Handled) return;
            }
        }
    }
}
