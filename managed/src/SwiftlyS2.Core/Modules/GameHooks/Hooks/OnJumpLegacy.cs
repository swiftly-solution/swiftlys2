using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate void CCSPlayerLegacyJumpOnJump( nint ccsPlayerLegacyJump, nint moveData );

    internal static Guid HookOnJumpLegacy()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::OnJumpLegacy");
        if (ptr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::OnJumpLegacy.");

        var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerLegacyJumpOnJump>(ptr);
        return unmanagedFunction.AddHook(next =>
        {
            return ( ccsPlayerLegacyJump, moveData ) =>
            {
                var dummy = _pawnComponentPool.Rent();
                unsafe
                {
                    var movementServices = *(nint*)(ccsPlayerLegacyJump + 8);
                    dummy.DangerousSetHandle(movementServices);
                }
                var player = dummy.ToPlayer();
                _pawnComponentPool.Return(dummy);
                if (player == null) { next()(ccsPlayerLegacyJump, moveData); return; }

                var moveDataImpl = _moveDataPool.Rent();
                moveDataImpl.Address = moveData;

                var preCtx = new OnJumpLegacyMovementPreContext {
                    Params = new OnJumpLegacyMovementParams {
                        Player = player,
                        MoveData = moveDataImpl
                    }
                };

                InvokeOnJumpLegacyPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                {
                    moveDataImpl.Address = 0;
                    _moveDataPool.Return(moveDataImpl);
                    return;
                }

                next()(ccsPlayerLegacyJump, moveData);

                var postCtx = new OnJumpLegacyMovementPostContext { Params = preCtx.Params };

                InvokeOnJumpLegacyPost(ref postCtx);

                moveDataImpl.Address = 0;
                _moveDataPool.Return(moveDataImpl);
            };
        });
    }

    internal static Guid UnhookOnJumpLegacy()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.OnJumpLegacy, out var hookId))
        {
            var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::OnJumpLegacy");
            if (ptr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::OnJumpLegacy.");

            var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerLegacyJumpOnJump>(ptr);
            unmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeOnJumpLegacyPre( ref OnJumpLegacyMovementPreContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnOnJumpLegacyMovementPreDelegate>.Snapshot))
        {
            subscriber.InvokeOnJumpLegacyPre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }

    internal static void InvokeOnJumpLegacyPost( ref OnJumpLegacyMovementPostContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnOnJumpLegacyMovementPostDelegate>.Snapshot))
        {
            subscriber.InvokeOnJumpLegacyPost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }
}
