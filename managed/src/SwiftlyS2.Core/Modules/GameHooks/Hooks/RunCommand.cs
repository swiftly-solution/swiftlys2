using SwiftlyS2.Core.Events;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate nint CPlayerMovementServicesRunCommand( nint pMovementServices, nint pUserCmd );

    internal static Guid HookRunCommand()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var offset = _core.GameData.GetOffset("CPlayer_MovementServices::RunCommand");
        if (offset < 0)
            throw new InvalidOperationException("Failed to find offset for CPlayer_MovementServices::RunCommand.");

        var runCommand = _core.Memory.GetUnmanagedFunctionByVTable<CPlayerMovementServicesRunCommand>(_core.Memory.GetVTableAddress(Library.Server, "CPlayer_MovementServices")!.Value, offset);
        return runCommand.AddHook(next =>
        {
            return ( pMovementServices, pUserCmd ) =>
            {

                var dummy = _pawnComponentPool.Rent();
                dummy.DangerousSetHandle(pMovementServices);
                var player = dummy.ToPlayer();
                _pawnComponentPool.Return(dummy);
                if (player == null) return next()(pMovementServices, pUserCmd);

                var userCmdImpl = _userCmdPool.Rent();
                userCmdImpl.Address = pUserCmd;

                var preCtx = new RunCommandMovementPreContext {
                    Params = new RunCommandMovementParams {
                        Player = player,
                        UserCmd = userCmdImpl
                    }
                };

                if (EventPublisher.ListensToRunCommand)
                {
                    using var ev = new OnMovementServicesRunCommandHookEvent {
                        MovementServices = preCtx.Params.Player.PlayerPawn!.MovementServices!,
                        ButtonState = preCtx.Params.UserCmd.ButtonState,
                        UserCmdPB = preCtx.Params.UserCmd.CSGOUserCmd
                    };
                    EventPublisher.InvokeOnMovementServicesRunCommandHook(ev);
                }

                InvokeRunCommandPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                {
                    userCmdImpl.Address = 0;
                    _userCmdPool.Return(userCmdImpl);
                    return 0;
                }

                var result = next()(pMovementServices, pUserCmd);

                var postCtx = new RunCommandMovementPostContext { Params = preCtx.Params };

                InvokeRunCommandPost(ref postCtx);

                userCmdImpl.Address = 0;
                _userCmdPool.Return(userCmdImpl);

                return result;
            };
        });
    }

    internal static Guid UnhookRunCommand()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.RunCommand, out var hookId))
        {
            var offset = _core.GameData.GetOffset("CPlayer_MovementServices::RunCommand");
            if (offset < 0)
                throw new InvalidOperationException("Failed to find offset for CPlayer_MovementServices::RunCommand.");

            var runCommand = _core.Memory.GetUnmanagedFunctionByVTable<CPlayerMovementServicesRunCommand>(_core.Memory.GetVTableAddress(Library.Server, "CPlayer_MovementServices")!.Value, offset);
            runCommand.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeRunCommandPre( ref RunCommandMovementPreContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeRunCommandPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeRunCommandPost( ref RunCommandMovementPostContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeRunCommandPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }
}
