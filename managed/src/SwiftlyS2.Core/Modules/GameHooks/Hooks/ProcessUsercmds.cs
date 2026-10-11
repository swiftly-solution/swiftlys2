using SwiftlyS2.Core.Events;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate nint CCSPlayerControllerProcessUsercmds( nint controller, nint userCmds, int numCmds, byte paused, float margin );
    private static readonly int CUserCmdPlatformPadding = IsWindows ? 0x8 : 0x0;

    internal static Guid HookProcessUsercmds()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var processUsercmdsPtr = _core.GameData.GetSignature("CCSPlayerController::ProcessUserCmd");
        if (processUsercmdsPtr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayerController::ProcessUserCmd.");

        var processUsercmdsUmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerControllerProcessUsercmds>(processUsercmdsPtr);
        return processUsercmdsUmanagedFunction.AddHook(next =>
        {
            return ( controller, userCmds, numCmds, paused, margin ) =>
            {
                var dummy = _controllerPool.Rent();
                dummy.DangerousSetHandle(controller);
                var player = dummy.ToPlayer();
                _controllerPool.Return(dummy);
                if (player == null) return next()(controller, userCmds, numCmds, paused, margin);

                var cmdsList = new List<IUserCmd>(numCmds);

                for (var i = 0; i < numCmds; i++)
                {
                    var userCmdImpl = _userCmdPool.Rent();
                    userCmdImpl.Address = userCmds + (i * (144 + CUserCmdPlatformPadding));
                    cmdsList.Add(userCmdImpl);
                }

                var preCtx = new ProcessUsercmdsPreContext {
                    Params = new ProcessUsercmdsParams {
                        Player = player,
                        Usercmds = cmdsList,
                        Paused = paused != 0,
                        Margin = margin
                    }
                };

                if (EventPublisher.ListensToProcessUsercmds)
                {
                    var usercmdsPBList = new List<CSGOUserCmdPB>(preCtx.Params.Usercmds.Count);
                    foreach (var usercmd in preCtx.Params.Usercmds)
                        usercmdsPBList.Add(usercmd.CSGOUserCmd);

                    var ev = new OnClientProcessUsercmdsEvent {
                        PlayerId = preCtx.Params.Player.PlayerID,
                        Paused = preCtx.Params.Paused,
                        Margin = preCtx.Params.Margin,
                        Usercmds = usercmdsPBList
                    };
                    EventPublisher.OnClientProcessUsercmds(ref ev);
                }

                InvokeProcessUsercmdsPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                {
                    foreach (var userCmd in preCtx.Params.Usercmds)
                    {
                        var userCmdImpl = (CUserCmd)userCmd;
                        userCmdImpl.Address = 0;
                        _userCmdPool.Return(userCmdImpl);
                    }
                    return 0;
                }

                var result = next()(controller, userCmds, numCmds, paused, margin);

                var postCtx = new ProcessUsercmdsPostContext { Params = preCtx.Params };

                InvokeProcessUsercmdsPost(ref postCtx);
                foreach (var userCmd in preCtx.Params.Usercmds)
                {
                    var userCmdImpl = (CUserCmd)userCmd;
                    userCmdImpl.Address = 0;
                    _userCmdPool.Return(userCmdImpl);
                }
                return result;
            };
        });
    }

    internal static Guid UnhookProcessUsercmds()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.ProcessUsercmds, out var hookId))
        {
            var processUsercmdsPtr = _core.GameData.GetSignature("CCSPlayerController::ProcessUserCmd");
            if (processUsercmdsPtr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayerController::ProcessUserCmd.");

            var processUsercmdsUmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerControllerProcessUsercmds>(processUsercmdsPtr);

            processUsercmdsUmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeProcessUsercmdsPre( ref ProcessUsercmdsPreContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeProcessUsercmdsPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeProcessUsercmdsPost( ref ProcessUsercmdsPostContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeProcessUsercmdsPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }
}
