using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate nint CCSPlayerControllerProcessUsercmds( nint controller, nint userCmds, int numCmds, byte paused, float margin );
    private static CCSPlayerControllerImpl _dummyController = new(0);
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
                _dummyController.DangerousSetHandle(controller);
                var player = _dummyController.ToPlayer();
                if (player == null) return next()(controller, userCmds, numCmds, paused, margin);

                var cmdsList = new List<IUserCmd>(numCmds);

                for (var i = 0; i < numCmds; i++)
                    cmdsList.Add(new CUserCmd { Address = userCmds + (i * (144 + CUserCmdPlatformPadding)) });

                var preCtx = new ProcessUsercmdsPreContext {
                    Params = new ProcessUsercmdsParams {
                        Player = player,
                        Usercmds = cmdsList,
                        Paused = paused != 0,
                        Margin = margin
                    }
                };

                InvokeProcessUsercmdsPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal) return 0;

                var result = next()(controller, userCmds, numCmds, paused, margin);

                var postCtx = new ProcessUsercmdsPostContext { Params = preCtx.Params };

                InvokeProcessUsercmdsPost(ref postCtx);
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
