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

                    var preCtx = new GroundAccelerateMovementPreContext {
                        Params = new GroundAccelerateMovementParams {
                            Player = player,
                            MoveData = new CMoveDataImpl { Address = moveData },
                            FrameTime = frameTime,
                            WishDirection = wishDirection != null ? *wishDirection : default,
                            WishSpeed = wishSpeed,
                            Acceleration = acceleration
                        }
                    };

                    InvokeGroundAcceleratePre(ref preCtx);
                    if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal) return;

                    if (wishDirection != null) *wishDirection = preCtx.Params.WishDirection;
                    next()(movementServices, moveData, preCtx.Params.FrameTime, wishDirection, preCtx.Params.WishSpeed, preCtx.Params.Acceleration);

                    var postCtx = new GroundAccelerateMovementPostContext { Params = preCtx.Params };

                    InvokeGroundAcceleratePost(ref postCtx);
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

                    var preCtx = new GroundAccelerateMovementPreContext {
                        Params = new GroundAccelerateMovementParams {
                            Player = player,
                            MoveData = new CMoveDataImpl { Address = moveData },
                            FrameTime = frameTime,
                            WishDirection = wishDirection != null ? *wishDirection : default,
                            WishSpeed = wishSpeed,
                            Acceleration = acceleration
                        }
                    };

                    InvokeGroundAcceleratePre(ref preCtx);
                    if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal) return;

                    if (wishDirection != null) *wishDirection = preCtx.Params.WishDirection;
                    next()(movementServices, moveData, wishDirection, preCtx.Params.FrameTime, preCtx.Params.WishSpeed, preCtx.Params.Acceleration);

                    var postCtx = new GroundAccelerateMovementPostContext { Params = preCtx.Params };

                    InvokeGroundAcceleratePost(ref postCtx);
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

    internal static void InvokeGroundAcceleratePre( ref GroundAccelerateMovementPreContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeGroundAcceleratePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeGroundAcceleratePost( ref GroundAccelerateMovementPostContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeGroundAcceleratePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }
}
