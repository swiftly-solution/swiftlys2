using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate void CCSPlayerMovementServicesCheckVelocity( nint movementServices, nint moveData, nint unknown );

    internal static Guid HookCheckVelocity()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::CheckVelocity");
        if (ptr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::CheckVelocity.");

        var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesCheckVelocity>(ptr);
        return unmanagedFunction.AddHook(next =>
        {
            return ( movementServices, moveData, unknown ) =>
            {
                var dummy = _pawnComponentPool.Rent();
                dummy.DangerousSetHandle(movementServices);
                var player = dummy.ToPlayer();
                _pawnComponentPool.Return(dummy);
                if (player == null)
                {
                    next()(movementServices, moveData, unknown);
                    return;
                }

                var moveDataImpl = _moveDataPool.Rent();
                moveDataImpl.Address = moveData;

                var preCtx = new CheckVelocityMovementPreContext {
                    Params = new CheckVelocityMovementParams {
                        Player = player,
                        MoveData = moveDataImpl
                    }
                };

                InvokeCheckVelocityPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                {
                    moveDataImpl.Address = 0;
                    _moveDataPool.Return(moveDataImpl);
                    return;
                }

                next()(movementServices, moveData, unknown);

                var postCtx = new CheckVelocityMovementPostContext { Params = preCtx.Params };

                InvokeCheckVelocityPost(ref postCtx);

                moveDataImpl.Address = 0;
                _moveDataPool.Return(moveDataImpl);
                return;
            };
        });
    }

    internal static Guid UnhookCheckVelocity()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.CheckVelocity, out var hookId))
        {
            var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::CheckVelocity");
            if (ptr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::CheckVelocity.");

            var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesCheckVelocity>(ptr);
            unmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeCheckVelocityPre( ref CheckVelocityMovementPreContext ctx )
    {
        var subs = GetSubscribers(HookListener.CheckVelocity);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCheckVelocityPre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }

    internal static void InvokeCheckVelocityPost( ref CheckVelocityMovementPostContext ctx )
    {
        var subs = GetSubscribers(HookListener.CheckVelocity);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCheckVelocityPost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }
}
