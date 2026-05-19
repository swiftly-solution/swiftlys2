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

                ICheckJumpButtonModernMovement @event = new CheckJumpButtonModernMovementData {
                    Player = player,
                    MoveData = new CMoveDataImpl { Address = moveData },
                    Result = HookResult.Continue
                };

                InvokeCheckJumpButtonModernPre(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.CancelOriginal) return;

                next()(ccsPlayerModernJump, moveData);

                @event.Result = HookResult.Continue;

                InvokeCheckJumpButtonModernPost(ref @event);
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

    internal static void InvokeCheckJumpButtonModernPre( ref ICheckJumpButtonModernMovement @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCheckJumpButtonModernPre(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeCheckJumpButtonModernPost( ref ICheckJumpButtonModernMovement @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCheckJumpButtonModernPost(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.Handled) return;
            }
        }
    }
}
