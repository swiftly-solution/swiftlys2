using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate void CCSPlayerMovementServicesSetupMove( nint movementServices, nint userCmd, nint moveData );

    internal static Guid HookSetupMove()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var setupMovePtr = _core.GameData.GetSignature("CCSPlayer_MovementServices::SetupMove");
        if (setupMovePtr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::SetupMove.");

        var setupMoveUmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesSetupMove>(setupMovePtr);
        return setupMoveUmanagedFunction.AddHook(next =>
        {
            return ( movementServices, userCmd, moveData ) =>
            {
                _dummyPawnComponent.DangerousSetHandle(movementServices);
                var player = _dummyController.ToPlayer();
                if (player == null) { next()(movementServices, userCmd, moveData); return; }

                ISetupMoveMovement @event = new SetupMoveMovementData {
                    Player = player,
                    UserCmd = new CUserCmd { Address = userCmd },
                    MoveData = new CMoveDataImpl { Address = moveData },
                    Result = HookResult.Continue
                };

                InvokeSetupMovePre(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.CancelOriginal) return;

                next()(movementServices, userCmd, moveData);

                @event.Result = HookResult.Continue;

                InvokeSetupMovePost(ref @event);
            };
        });
    }

    internal static Guid UnhookSetupMove()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.SetupMove, out var hookId))
        {
            var setupMovePtr = _core.GameData.GetSignature("CCSPlayer_MovementServices::SetupMove");
            if (setupMovePtr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::SetupMove.");

            var setupMoveUmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesSetupMove>(setupMovePtr);

            setupMoveUmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeSetupMovePre( ref ISetupMoveMovement @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeSetupMovePre(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeSetupMovePost( ref ISetupMoveMovement @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeSetupMovePost(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.Handled) return;
            }
        }
    }
}
