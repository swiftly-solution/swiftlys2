using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate nint CCSPlayerPawnPostThink( nint pawn );
    private static CCSPlayerPawnImpl _dummyPawn = new(0);

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
                _dummyPawn.DangerousSetHandle(pawn);
                var player = _dummyPawn.ToPlayer();
                if (player == null) return next()(pawn);

                IPostThinkPawn @event = new PostThinkPawnData {
                    Player = player,
                    Result = HookResult.Continue
                };

                InvokePostThinkPre(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.CancelOriginal) return 0;

                var result = next()(pawn);

                @event.Result = HookResult.Continue;

                InvokePostThinkPost(ref @event);
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

    internal static void InvokePostThinkPre( ref IPostThinkPawn @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokePostThinkPre(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokePostThinkPost( ref IPostThinkPawn @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokePostThinkPost(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.Handled) return;
            }
        }
    }
}
