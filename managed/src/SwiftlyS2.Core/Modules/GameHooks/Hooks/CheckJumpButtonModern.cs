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
                unsafe
                {
                    _dummyPawnComponent.DangerousSetHandle(*(nint*)(ccsPlayerModernJump + 8));
                }
                var player = _dummyPawnComponent.ToPlayer();
                if (player == null) { next()(ccsPlayerModernJump, moveData); return; }

                var preCtx = new CheckJumpButtonModernMovementPreContext {
                    Params = new CheckJumpButtonModernMovementParams {
                        Player = player,
                        MoveData = new CMoveDataImpl { Address = moveData }
                    }
                };

                InvokeCheckJumpButtonModernPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal) return;

                next()(ccsPlayerModernJump, moveData);

                var postCtx = new CheckJumpButtonModernMovementPostContext { Params = preCtx.Params };

                InvokeCheckJumpButtonModernPost(ref postCtx);
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
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCheckJumpButtonModernPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeCheckJumpButtonModernPost( ref CheckJumpButtonModernMovementPostContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCheckJumpButtonModernPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }
}
