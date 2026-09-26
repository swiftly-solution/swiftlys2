using Spectre.Console;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class DatamapHooksPublisher
{
    private delegate void CVoteControllerVoteControllerThinkDelegate(nint a1);

    private static IUnmanagedFunction<CVoteControllerVoteControllerThinkDelegate>? CVoteControllerVoteControllerThinkUnmanagedFunction;
    private static Guid CVoteControllerVoteControllerThinkHookGuid;

    private static IUnmanagedFunction<CVoteControllerVoteControllerThinkDelegate> CVoteControllerVoteControllerThinkGetUnmanagedFunction()
    {
        if (CVoteControllerVoteControllerThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CVoteController", "CVoteControllerVoteControllerThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CVoteController::CVoteControllerVoteControllerThink.");
            }
            CVoteControllerVoteControllerThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CVoteControllerVoteControllerThinkDelegate>(address);
        }
        return CVoteControllerVoteControllerThinkUnmanagedFunction;
    }

    internal static Guid HookCVoteControllerVoteControllerThink()
    {
        CVoteControllerVoteControllerThinkHookGuid = CVoteControllerVoteControllerThinkGetUnmanagedFunction().AddHook(next => (a1) => CVoteControllerVoteControllerThinkPipeline(a1, () => next()(a1)));
        return CVoteControllerVoteControllerThinkHookGuid;
    }

    internal static Guid UnhookCVoteControllerVoteControllerThink()
    {
        CVoteControllerVoteControllerThinkGetUnmanagedFunction().RemoveHook(CVoteControllerVoteControllerThinkHookGuid);
        return Guid.Empty;
    }

    private static void CVoteControllerVoteControllerThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CVoteController>(a1);

            var preCtx = new CVoteControllerVoteControllerThinkPreContext { SchemaObject = schemaObject };
            InvokeCVoteControllerVoteControllerThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CVoteControllerVoteControllerThinkPostContext { SchemaObject = schemaObject };
            InvokeCVoteControllerVoteControllerThinkPost(ref postCtx);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    internal static void InvokeCVoteControllerVoteControllerThink(nint a1)
    {
        CVoteControllerVoteControllerThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCVoteControllerVoteControllerThinkPre(ref CVoteControllerVoteControllerThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCVoteControllerVoteControllerThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCVoteControllerVoteControllerThinkPost(ref CVoteControllerVoteControllerThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCVoteControllerVoteControllerThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CVoteControllerVoteControllerThinkHook : ICVoteControllerVoteControllerThinkHook
{
    private event OnCVoteControllerVoteControllerThinkPreDelegate? _Pre;
    private event OnCVoteControllerVoteControllerThinkPostDelegate? _Post;

    public event OnCVoteControllerVoteControllerThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CVoteControllerVoteControllerThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CVoteControllerVoteControllerThink);
            }
        }
    }

    public event OnCVoteControllerVoteControllerThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CVoteControllerVoteControllerThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CVoteControllerVoteControllerThink);
            }
        }
    }

    public void InvokePre(ref CVoteControllerVoteControllerThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CVoteControllerVoteControllerThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CVoteControllerVoteControllerThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CVoteControllerVoteControllerThink);
        }
    }

    public void Invoke(CVoteController schemaObject) => DatamapHooksPublisher.InvokeCVoteControllerVoteControllerThink(schemaObject.Address);
}