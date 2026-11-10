using SwiftlyS2.Core.Events;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate nint CCSPlayerPawnPostThink( nint pawn );

    internal static Guid HookPostThink()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var postThinkPtr = _core.GameData.GetSignature("CCSPlayerPawn::PostThink");
        if (postThinkPtr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayerPawn::PostThink.");

        var postThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerPawnPostThink>(postThinkPtr);
        return postThinkUnmanagedFunction.AddHook(next =>
        {
            return ( pawn ) =>
            {
                var dummy = _pawnPool.Rent();
                dummy.DangerousSetHandle(pawn);
                var player = dummy.ToPlayer();
                _pawnPool.Return(dummy);
                if (player == null) return next()(pawn);

                var preCtx = new PostThinkPawnPreContext { Params = new PostThinkPawnParams { Player = player } };

                if (EventPublisher.ListensToPostThink)
                {
                    using var ev = new OnPlayerPawnPostThinkHookEvent {
                        PlayerPawn = preCtx.Params.Player.PlayerPawn!
                    };
                    EventPublisher.InvokeOnPlayerPawnPostThinkHook(ev);
                }

                InvokePostThinkPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal) return 0;

                var result = next()(pawn);

                var postCtx = new PostThinkPawnPostContext { Params = preCtx.Params };

                InvokePostThinkPost(ref postCtx);
                return result;
            };
        });
    }

    internal static Guid UnhookPostThink()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.PostThink, out var hookId))
        {
            var postThinkPtr = _core.GameData.GetSignature("CCSPlayerPawn::PostThink");
            if (postThinkPtr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayerPawn::PostThink.");

            var postThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerPawnPostThink>(postThinkPtr);

            postThinkUnmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokePostThinkPre( ref PostThinkPawnPreContext ctx )
    {
        var subs = GetSubscribers(HookListener.PostThink);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokePostThinkPre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }

    internal static void InvokePostThinkPost( ref PostThinkPawnPostContext ctx )
    {
        var subs = GetSubscribers(HookListener.PostThink);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokePostThinkPost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
        }
    }
}
