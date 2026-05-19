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
                _dummyPawn.DangerousSetHandle(pawn);
                var player = _dummyPawn.ToPlayer();
                if (player == null) return next()(pawn);

                ICanMovePawn @event = new CanMovePawnData {
                    Player = player,
                    OriginalResult = false,
                    Result = HookResult.Continue
                };

                InvokeCanMovePre(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.CancelOriginal)
                    return @event.Intercepted ? (@event.OriginalResult ? (byte)1 : (byte)0) : (byte)0;

                var result = next()(pawn);

                @event.SetResult(result != 0);
                @event.Intercepted = false;

                InvokeCanMovePost(ref @event);

                return @event.Intercepted ? (@event.OriginalResult ? (byte)1 : (byte)0) : result;
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

    internal static void InvokeCanMovePre( ref ICanMovePawn @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCanMovePre(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeCanMovePost( ref ICanMovePawn @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCanMovePost(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.Handled) return;
            }
        }
    }
}
