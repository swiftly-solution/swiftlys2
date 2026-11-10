using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate byte CCSPlayerPawnCanMove( nint pawn );

    internal static Guid HookCanMove()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var canMovePtr = _core.GameData.GetSignature("CCSPlayerPawn::CanMove");
        if (canMovePtr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayerPawn::CanMove.");

        var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerPawnCanMove>(canMovePtr);
        return unmanagedFunction.AddHook(next =>
        {
            return ( pawn ) =>
            {
                var dummy = _pawnPool.Rent();
                dummy.DangerousSetHandle(pawn);
                var player = dummy.ToPlayer();
                _pawnPool.Return(dummy);
                if (player == null) return next()(pawn);

                var preCtx = new CanMovePawnPreContext { Params = new CanMovePawnParams { Player = player } };

                InvokeCanMovePre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                    return preCtx.IsReturnSet ? (preCtx.Return ? (byte)1 : (byte)0) : (byte)0;

                var result = next()(pawn);

                var postCtx = new CanMovePawnPostContext { Params = preCtx.Params, Return = result != 0 };

                InvokeCanMovePost(ref postCtx);

                return postCtx.Return ? (byte)1 : (byte)0;
            };
        });
    }

    internal static Guid UnhookCanMove()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.CanMove, out var hookId))
        {
            var canMovePtr = _core.GameData.GetSignature("CCSPlayerPawn::CanMove");
            if (canMovePtr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayerPawn::CanMove.");

            var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerPawnCanMove>(canMovePtr);
            unmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeCanMovePre( ref CanMovePawnPreContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnCanMovePawnPreDelegate>.Snapshot))
        {
            subscriber.InvokeCanMovePre(ref ctx);
            if (ctx.HookResult is HookResult.Stop or HookResult.Handled) return;
        }
    }

    internal static void InvokeCanMovePost( ref CanMovePawnPostContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnCanMovePawnPostDelegate>.Snapshot))
        {
            subscriber.InvokeCanMovePost(ref ctx);
            if (ctx.HookResult is HookResult.Stop or HookResult.Handled) return;
        }
    }
}
