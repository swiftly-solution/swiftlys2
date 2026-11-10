using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate byte CCSPlayerMovementServicesCheckWater( nint movementServices, nint moveData );

    internal static Guid HookCheckWater()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::CheckWater");
        if (ptr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::CheckWater.");

        var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesCheckWater>(ptr);
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

                var preCtx = new CheckWaterMovementPreContext {
                    Params = new CheckWaterMovementParams {
                        Player = player,
                        MoveData = moveDataImpl
                    }
                };

                InvokeCheckWaterPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                {
                    moveDataImpl.Address = 0;
                    _moveDataPool.Return(moveDataImpl);
                    return preCtx.IsReturnSet ? (preCtx.Return ? (byte)1 : (byte)0) : (byte)0;
                }

                var result = next()(movementServices, moveData);

                var postCtx = new CheckWaterMovementPostContext { Params = preCtx.Params, Return = result != 0 };

                InvokeCheckWaterPost(ref postCtx);

                moveDataImpl.Address = 0;
                _moveDataPool.Return(moveDataImpl);
                return postCtx.Return ? (byte)1 : (byte)0;
            };
        });
    }

    internal static Guid UnhookCheckWater()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.CheckWater, out var hookId))
        {
            var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::CheckWater");
            if (ptr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::CheckWater.");

            var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesCheckWater>(ptr);
            unmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeCheckWaterPre( ref CheckWaterMovementPreContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnCheckWaterMovementPreDelegate>.Snapshot))
        {
            subscriber.InvokeCheckWaterPre(ref ctx);
            if (ctx.HookResult is HookResult.Stop or HookResult.Handled) return;
        }
    }

    internal static void InvokeCheckWaterPost( ref CheckWaterMovementPostContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnCheckWaterMovementPostDelegate>.Snapshot))
        {
            subscriber.InvokeCheckWaterPost(ref ctx);
            if (ctx.HookResult is HookResult.Stop or HookResult.Handled) return;
        }
    }
}
