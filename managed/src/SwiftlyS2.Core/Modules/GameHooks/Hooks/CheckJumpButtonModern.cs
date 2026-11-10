using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate void CCSPlayerModernJumpCheckJumpButton( nint ccsPlayerModernJump, nint moveData );

    internal static Guid HookCheckJumpButtonModern()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::CheckJumpButtonModern");
        if (ptr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::CheckJumpButtonModern.");

        var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerModernJumpCheckJumpButton>(ptr);
        return unmanagedFunction.AddHook(next =>
        {
            return ( ccsPlayerModernJump, moveData ) =>
            {
                var dummy = _pawnComponentPool.Rent();
                unsafe
                {
                    dummy.DangerousSetHandle(*(nint*)(ccsPlayerModernJump + 8));
                }
                var player = dummy.ToPlayer();
                _pawnComponentPool.Return(dummy);
                if (player == null) { next()(ccsPlayerModernJump, moveData); return; }

                var moveDataImpl = _moveDataPool.Rent();
                moveDataImpl.Address = moveData;

                var preCtx = new CheckJumpButtonModernMovementPreContext {
                    Params = new CheckJumpButtonModernMovementParams {
                        Player = player,
                        MoveData = moveDataImpl
                    }
                };

                InvokeCheckJumpButtonModernPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                {
                    moveDataImpl.Address = 0;
                    _moveDataPool.Return(moveDataImpl);
                    return;
                }

                next()(ccsPlayerModernJump, moveData);

                var postCtx = new CheckJumpButtonModernMovementPostContext { Params = preCtx.Params };

                InvokeCheckJumpButtonModernPost(ref postCtx);

                moveDataImpl.Address = 0;
                _moveDataPool.Return(moveDataImpl);
            };
        });
    }

    internal static Guid UnhookCheckJumpButtonModern()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.CheckJumpButtonModern, out var hookId))
        {
            var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::CheckJumpButtonModern");
            if (ptr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::CheckJumpButtonModern.");

            var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerModernJumpCheckJumpButton>(ptr);
            unmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeCheckJumpButtonModernPre( ref CheckJumpButtonModernMovementPreContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnCheckJumpButtonModernMovementPreDelegate>.Snapshot))
        {
            subscriber.InvokeCheckJumpButtonModernPre(ref ctx);
            if (ctx.HookResult is HookResult.Stop or HookResult.Handled) return;
        }
    }

    internal static void InvokeCheckJumpButtonModernPost( ref CheckJumpButtonModernMovementPostContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnCheckJumpButtonModernMovementPostDelegate>.Snapshot))
        {
            subscriber.InvokeCheckJumpButtonModernPost(ref ctx);
            if (ctx.HookResult is HookResult.Stop or HookResult.Handled) return;
        }
    }
}
