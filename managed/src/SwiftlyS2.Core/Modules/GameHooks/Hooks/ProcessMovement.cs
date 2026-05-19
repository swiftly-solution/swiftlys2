using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate void CCSPlayerMovementServicesProcessMovement( nint movementServices, nint moveData );

    internal static Guid HookProcessMovement()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var ProcessMovementPtr = _core.GameData.GetSignature("CCSPlayer_MovementServices::ProcessMovement");
        if (ProcessMovementPtr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::ProcessMovement.");

        var ProcessMovementUmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesProcessMovement>(ProcessMovementPtr);
        return ProcessMovementUmanagedFunction.AddHook(next =>
        {
            return ( movementServices, moveData ) =>
            {
                _dummyPawnComponent.DangerousSetHandle(movementServices);
                var player = _dummyController.ToPlayer();
                if (player == null) { next()(movementServices, moveData); return; }

                IProcessMovementMovement @event = new ProcessMovementMovementData {
                    Player = player,
                    MoveData = new CMoveDataImpl { Address = moveData },
                    Result = HookResult.Continue
                };

                InvokeProcessMovementPre(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.CancelOriginal) return;

                next()(movementServices, moveData);

                @event.Result = HookResult.Continue;

                InvokeProcessMovementPost(ref @event);
            };
        });
    }

    internal static Guid UnhookProcessMovement()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.ProcessMovement, out var hookId))
        {
            var ProcessMovementPtr = _core.GameData.GetSignature("CCSPlayer_MovementServices::ProcessMovement");
            if (ProcessMovementPtr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::ProcessMovement.");

            var ProcessMovementUmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesProcessMovement>(ProcessMovementPtr);

            ProcessMovementUmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeProcessMovementPre( ref IProcessMovementMovement @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeProcessMovementPre(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeProcessMovementPost( ref IProcessMovementMovement @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeProcessMovementPost(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.Handled) return;
            }
        }
    }
}
