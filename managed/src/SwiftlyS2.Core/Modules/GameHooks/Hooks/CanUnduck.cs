using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate byte CCSPlayerMovementServicesCanUnduck( nint movementServices, nint moveData );

    internal static Guid HookCanUnduck()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::CanUnduck");
        if (ptr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::CanUnduck.");

        var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesCanUnduck>(ptr);
        return unmanagedFunction.AddHook(next =>
        {
            return ( movementServices, moveData ) =>
            {
                var dummy = _pawnComponentPool.Rent();
                dummy.DangerousSetHandle(movementServices);
                var player = dummy.ToPlayer();
                _pawnComponentPool.Return(dummy);
                if (player == null) return next()(movementServices, moveData);

                var moveDataImpl = _moveDataPool.Rent();
                moveDataImpl.Address = moveData;

                var preCtx = new CanUnduckMovementPreContext {
                    Params = new CanUnduckMovementParams {
                        Player = player,
                        MoveData = moveDataImpl
                    }
                };

                InvokeCanUnduckPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                {
                    moveDataImpl.Address = 0;
                    _moveDataPool.Return(moveDataImpl);
                    return preCtx.IsReturnSet ? (preCtx.Return ? (byte)1 : (byte)0) : (byte)0;
                }

                var result = next()(movementServices, moveData);

                var postCtx = new CanUnduckMovementPostContext { Params = preCtx.Params, Return = result != 0 };

                InvokeCanUnduckPost(ref postCtx);

                moveDataImpl.Address = 0;
                _moveDataPool.Return(moveDataImpl);
                return postCtx.Return ? (byte)1 : (byte)0;
            };
        });
    }

    internal static Guid UnhookCanUnduck()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.CanUnduck, out var hookId))
        {
            var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::CanUnduck");
            if (ptr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::CanUnduck.");

            var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesCanUnduck>(ptr);
            unmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeCanUnduckPre( ref CanUnduckMovementPreContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnCanUnduckMovementPreDelegate>.Snapshot))
        {
            subscriber.InvokeCanUnduckPre(ref ctx);
            if (ctx.HookResult is HookResult.Stop or HookResult.Handled) return;
        }
    }

    internal static void InvokeCanUnduckPost( ref CanUnduckMovementPostContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnCanUnduckMovementPostDelegate>.Snapshot))
        {
            subscriber.InvokeCanUnduckPost(ref ctx);
            if (ctx.HookResult is HookResult.Stop or HookResult.Handled) return;
        }
    }
}
