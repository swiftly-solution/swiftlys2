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
                    var dummy = _pawnComponentPool.Rent();
                    dummy.DangerousSetHandle(movementServices);
                    var player = dummy.ToPlayer();
                    _pawnComponentPool.Return(dummy);
                    if (player == null) { next()(movementServices, moveData, frameTime, wishDirection, wishSpeed, acceleration); return; }

                    var moveDataImpl = _moveDataPool.Rent();
                    moveDataImpl.Address = moveData;

                    var preCtx = new GroundAccelerateMovementPreContext {
                        Params = new GroundAccelerateMovementParams {
                            Player = player,
                            MoveData = moveDataImpl,
                            FrameTime = frameTime,
                            WishDirection = wishDirection != null ? *wishDirection : default,
                            WishSpeed = wishSpeed,
                            Acceleration = acceleration
                        }
                    };

                    InvokeGroundAcceleratePre(ref preCtx);
                    if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                    {
                        moveDataImpl.Address = 0;
                        _moveDataPool.Return(moveDataImpl);
                        return;
                    }

                    if (wishDirection != null) *wishDirection = preCtx.Params.WishDirection;
                    next()(movementServices, moveData, preCtx.Params.FrameTime, wishDirection, preCtx.Params.WishSpeed, preCtx.Params.Acceleration);

                    var postCtx = new GroundAccelerateMovementPostContext { Params = preCtx.Params };

                    InvokeGroundAcceleratePost(ref postCtx);

                    moveDataImpl.Address = 0;
                    _moveDataPool.Return(moveDataImpl);
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
                    var dummy = _pawnComponentPool.Rent();
                    dummy.DangerousSetHandle(movementServices);
                    var player = dummy.ToPlayer();
                    _pawnComponentPool.Return(dummy);
                    if (player == null) { next()(movementServices, moveData, wishDirection, frameTime, wishSpeed, acceleration); return; }

                    var moveDataImpl = _moveDataPool.Rent();
                    moveDataImpl.Address = moveData;

                    var preCtx = new GroundAccelerateMovementPreContext {
                        Params = new GroundAccelerateMovementParams {
                            Player = player,
                            MoveData = moveDataImpl,
                            FrameTime = frameTime,
                            WishDirection = wishDirection != null ? *wishDirection : default,
                            WishSpeed = wishSpeed,
                            Acceleration = acceleration
                        }
                    };

                    InvokeGroundAcceleratePre(ref preCtx);
                    if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                    {
                        moveDataImpl.Address = 0;
                        _moveDataPool.Return(moveDataImpl);
                        return;
                    }

                    if (wishDirection != null) *wishDirection = preCtx.Params.WishDirection;
                    next()(movementServices, moveData, wishDirection, preCtx.Params.FrameTime, preCtx.Params.WishSpeed, preCtx.Params.Acceleration);

                    var postCtx = new GroundAccelerateMovementPostContext { Params = preCtx.Params };

                    InvokeGroundAcceleratePost(ref postCtx);

                    moveDataImpl.Address = 0;
                    _moveDataPool.Return(moveDataImpl);
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
        var subs = GetSubscribers(HookListener.GroundAccelerate);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeGroundAcceleratePre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }

    internal static void InvokeGroundAcceleratePost( ref GroundAccelerateMovementPostContext ctx )
    {
        var subs = GetSubscribers(HookListener.GroundAccelerate);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeGroundAcceleratePost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }
}
