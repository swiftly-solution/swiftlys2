using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate void CCSPlayerLegacyJumpCheckJumpButton( nint ccsPlayerLegacyJump, nint moveData );

    internal static Guid HookCheckJumpButtonLegacy()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::CheckJumpButtonLegacy");
        if (ptr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::CheckJumpButtonLegacy.");

        var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerLegacyJumpCheckJumpButton>(ptr);
        return unmanagedFunction.AddHook(next =>
        {
            return ( ccsPlayerLegacyJump, moveData ) =>
            {
                unsafe
                {
                    _dummyPawnComponent.DangerousSetHandle(*(nint*)(ccsPlayerLegacyJump + 8));
                }
                var player = _dummyPawnComponent.ToPlayer();
                if (player == null) { next()(ccsPlayerLegacyJump, moveData); return; }

                var preCtx = new CheckJumpButtonLegacyMovementPreContext {
                    Params = new CheckJumpButtonLegacyMovementParams {
                        Player = player,
                        MoveData = new CMoveDataImpl { Address = moveData }
                    }
                };

                InvokeCheckJumpButtonLegacyPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal) return;

                next()(ccsPlayerLegacyJump, moveData);

                var postCtx = new CheckJumpButtonLegacyMovementPostContext { Params = preCtx.Params };

                InvokeCheckJumpButtonLegacyPost(ref postCtx);
            };
        });
    }

    internal static Guid UnhookCheckJumpButtonLegacy()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.CheckJumpButtonLegacy, out var hookId))
        {
            var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::CheckJumpButtonLegacy");
            if (ptr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::CheckJumpButtonLegacy.");

            var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerLegacyJumpCheckJumpButton>(ptr);
            unmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeCheckJumpButtonLegacyPre( ref CheckJumpButtonLegacyMovementPreContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCheckJumpButtonLegacyPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeCheckJumpButtonLegacyPost( ref CheckJumpButtonLegacyMovementPostContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCheckJumpButtonLegacyPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }
}
