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
                var dummy = _pawnComponentPool.Rent();
                dummy.DangerousSetHandle(movementServices);
                var player = dummy.ToPlayer();
                _pawnComponentPool.Return(dummy);
                if (player == null) { next()(movementServices, userCmd, moveData); return; }

                var moveDataImpl = _moveDataPool.Rent();
                var userCmdImpl = _userCmdPool.Rent();
                userCmdImpl.Address = userCmd;
                moveDataImpl.Address = moveData;

                var preCtx = new SetupMoveMovementPreContext {
                    Params = new SetupMoveMovementParams {
                        Player = player,
                        UserCmd = userCmdImpl,
                        MoveData = moveDataImpl
                    }
                };

                InvokeSetupMovePre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                {
                    moveDataImpl.Address = 0;
                    userCmdImpl.Address = 0;
                    _userCmdPool.Return(userCmdImpl);
                    _moveDataPool.Return(moveDataImpl);
                    return;
                }

                next()(movementServices, userCmd, moveData);

                var postCtx = new SetupMoveMovementPostContext { Params = preCtx.Params };

                InvokeSetupMovePost(ref postCtx);

                moveDataImpl.Address = 0;
                userCmdImpl.Address = 0;
                _userCmdPool.Return(userCmdImpl);
                _moveDataPool.Return(moveDataImpl);
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

    internal static void InvokeSetupMovePre( ref SetupMoveMovementPreContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeSetupMovePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeSetupMovePost( ref SetupMoveMovementPostContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeSetupMovePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }
}
