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
    private delegate void CTriggerSaveRetriggerWaitOverDelegate(nint a1);

    private static IUnmanagedFunction<CTriggerSaveRetriggerWaitOverDelegate>? CTriggerSaveRetriggerWaitOverUnmanagedFunction;
    private static Guid CTriggerSaveRetriggerWaitOverHookGuid;

    private static IUnmanagedFunction<CTriggerSaveRetriggerWaitOverDelegate> CTriggerSaveRetriggerWaitOverGetUnmanagedFunction()
    {
        if (CTriggerSaveRetriggerWaitOverUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CTriggerSave", "CTriggerSaveRetriggerWaitOver");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CTriggerSave::CTriggerSaveRetriggerWaitOver.");
            }
            CTriggerSaveRetriggerWaitOverUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CTriggerSaveRetriggerWaitOverDelegate>(address);
        }
        return CTriggerSaveRetriggerWaitOverUnmanagedFunction;
    }

    internal static Guid HookCTriggerSaveRetriggerWaitOver()
    {
        CTriggerSaveRetriggerWaitOverHookGuid = CTriggerSaveRetriggerWaitOverGetUnmanagedFunction().AddHook(next => (a1) => CTriggerSaveRetriggerWaitOverPipeline(a1, () => next()(a1)));
        return CTriggerSaveRetriggerWaitOverHookGuid;
    }

    internal static Guid UnhookCTriggerSaveRetriggerWaitOver()
    {
        CTriggerSaveRetriggerWaitOverGetUnmanagedFunction().RemoveHook(CTriggerSaveRetriggerWaitOverHookGuid);
        return Guid.Empty;
    }

    private static void CTriggerSaveRetriggerWaitOverPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CTriggerSave>(a1);

            var preCtx = new CTriggerSaveRetriggerWaitOverPreContext { SchemaObject = schemaObject };
            InvokeCTriggerSaveRetriggerWaitOverPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CTriggerSaveRetriggerWaitOverPostContext { SchemaObject = schemaObject };
            InvokeCTriggerSaveRetriggerWaitOverPost(ref postCtx);
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

    internal static void InvokeCTriggerSaveRetriggerWaitOver(nint a1)
    {
        CTriggerSaveRetriggerWaitOverGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCTriggerSaveRetriggerWaitOverPre(ref CTriggerSaveRetriggerWaitOverPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerSaveRetriggerWaitOverPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCTriggerSaveRetriggerWaitOverPost(ref CTriggerSaveRetriggerWaitOverPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerSaveRetriggerWaitOverPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CTriggerSaveRetriggerWaitOverHook : ICTriggerSaveRetriggerWaitOverHook
{
    private event OnCTriggerSaveRetriggerWaitOverPreDelegate? _Pre;
    private event OnCTriggerSaveRetriggerWaitOverPostDelegate? _Post;

    public event OnCTriggerSaveRetriggerWaitOverPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerSaveRetriggerWaitOver);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerSaveRetriggerWaitOver);
            }
        }
    }

    public event OnCTriggerSaveRetriggerWaitOverPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerSaveRetriggerWaitOver);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerSaveRetriggerWaitOver);
            }
        }
    }

    public void InvokePre(ref CTriggerSaveRetriggerWaitOverPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CTriggerSaveRetriggerWaitOverPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerSaveRetriggerWaitOver);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerSaveRetriggerWaitOver);
        }
    }

    public void Invoke(CTriggerSave schemaObject) => DatamapHooksPublisher.InvokeCTriggerSaveRetriggerWaitOver(schemaObject.Address);
}