using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate void CCSPlayerModernJumpOnJump( nint ccsPlayerModernJump, nint moveData );

    internal static Guid HookOnJumpModern()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::OnJumpModern");
        if (ptr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::OnJumpModern.");

        var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerModernJumpOnJump>(ptr);
        return unmanagedFunction.AddHook(next =>
        {
            return ( ccsPlayerModernJump, moveData ) =>
            {
                var dummy = _pawnComponentPool.Rent();
                unsafe
                {
                    var movementServices = *(nint*)(ccsPlayerModernJump + 8);
                    dummy.DangerousSetHandle(movementServices);
                }
                var player = dummy.ToPlayer();
                _pawnComponentPool.Return(dummy);
                if (player == null) { next()(ccsPlayerModernJump, moveData); return; }

                var moveDataImpl = _moveDataPool.Rent();
                moveDataImpl.Address = moveData;

                var preCtx = new OnJumpModernMovementPreContext {
                    Params = new OnJumpModernMovementParams {
                        Player = player,
                        MoveData = moveDataImpl
                    }
                };

                InvokeOnJumpModernPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                {
                    moveDataImpl.Address = 0;
                    _moveDataPool.Return(moveDataImpl);
                    return;
                }

                next()(ccsPlayerModernJump, moveData);

                var postCtx = new OnJumpModernMovementPostContext { Params = preCtx.Params };

                InvokeOnJumpModernPost(ref postCtx);

                moveDataImpl.Address = 0;
                _moveDataPool.Return(moveDataImpl);
            };
        });
    }

    internal static Guid UnhookOnJumpModern()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.OnJumpModern, out var hookId))
        {
            var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::OnJumpModern");
            if (ptr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::OnJumpModern.");

            var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerModernJumpOnJump>(ptr);
            unmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeOnJumpModernPre( ref OnJumpModernMovementPreContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnOnJumpModernMovementPreDelegate>.Snapshot))
        {
            subscriber.InvokeOnJumpModernPre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }

    internal static void InvokeOnJumpModernPost( ref OnJumpModernMovementPostContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnOnJumpModernMovementPostDelegate>.Snapshot))
        {
            subscriber.InvokeOnJumpModernPost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }
}
