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
                unsafe
                {
                    var movementServices = *(nint*)(ccsPlayerModernJump + 8);
                    _dummyPawnComponent.DangerousSetHandle(movementServices);
                }
                var player = _dummyPawnComponent.ToPlayer();
                if (player == null) { next()(ccsPlayerModernJump, moveData); return; }

                var preCtx = new OnJumpModernMovementPreContext {
                    Params = new OnJumpModernMovementParams {
                        Player = player,
                        MoveData = new CMoveDataImpl { Address = moveData }
                    }
                };

                InvokeOnJumpModernPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal) return;

                next()(ccsPlayerModernJump, moveData);

                var postCtx = new OnJumpModernMovementPostContext { Params = preCtx.Params };

                InvokeOnJumpModernPost(ref postCtx);
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
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnJumpModernPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeOnJumpModernPost( ref OnJumpModernMovementPostContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeOnJumpModernPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }
}
