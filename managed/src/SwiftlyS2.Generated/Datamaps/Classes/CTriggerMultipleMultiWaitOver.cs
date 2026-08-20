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
    private delegate void CTriggerMultipleMultiWaitOverDelegate(nint a1);

    private static IUnmanagedFunction<CTriggerMultipleMultiWaitOverDelegate>? CTriggerMultipleMultiWaitOverUnmanagedFunction;
    private static Guid CTriggerMultipleMultiWaitOverHookGuid;

    private static IUnmanagedFunction<CTriggerMultipleMultiWaitOverDelegate> CTriggerMultipleMultiWaitOverGetUnmanagedFunction()
    {
        if (CTriggerMultipleMultiWaitOverUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CTriggerMultiple", "CTriggerMultipleMultiWaitOver");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CTriggerMultiple::CTriggerMultipleMultiWaitOver.");
            }
            CTriggerMultipleMultiWaitOverUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CTriggerMultipleMultiWaitOverDelegate>(address);
        }
        return CTriggerMultipleMultiWaitOverUnmanagedFunction;
    }

    internal static Guid HookCTriggerMultipleMultiWaitOver()
    {
        CTriggerMultipleMultiWaitOverHookGuid = CTriggerMultipleMultiWaitOverGetUnmanagedFunction().AddHook(next => (a1) => CTriggerMultipleMultiWaitOverPipeline(a1, () => next()(a1)));
        return CTriggerMultipleMultiWaitOverHookGuid;
    }

    internal static Guid UnhookCTriggerMultipleMultiWaitOver()
    {
        CTriggerMultipleMultiWaitOverGetUnmanagedFunction().RemoveHook(CTriggerMultipleMultiWaitOverHookGuid);
        return Guid.Empty;
    }

    private static void CTriggerMultipleMultiWaitOverPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CTriggerMultiple>(a1);

            var preCtx = new CTriggerMultipleMultiWaitOverPreContext { SchemaObject = schemaObject };
            InvokeCTriggerMultipleMultiWaitOverPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CTriggerMultipleMultiWaitOverPostContext { SchemaObject = schemaObject };
            InvokeCTriggerMultipleMultiWaitOverPost(ref postCtx);
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

    internal static void InvokeCTriggerMultipleMultiWaitOver(nint a1)
    {
        CTriggerMultipleMultiWaitOverGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCTriggerMultipleMultiWaitOverPre(ref CTriggerMultipleMultiWaitOverPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerMultipleMultiWaitOverPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCTriggerMultipleMultiWaitOverPost(ref CTriggerMultipleMultiWaitOverPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerMultipleMultiWaitOverPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CTriggerMultipleMultiWaitOverHook : ICTriggerMultipleMultiWaitOverHook
{
    private event OnCTriggerMultipleMultiWaitOverPreDelegate? _Pre;
    private event OnCTriggerMultipleMultiWaitOverPostDelegate? _Post;

    public event OnCTriggerMultipleMultiWaitOverPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerMultipleMultiWaitOver);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerMultipleMultiWaitOver);
            }
        }
    }

    public event OnCTriggerMultipleMultiWaitOverPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerMultipleMultiWaitOver);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerMultipleMultiWaitOver);
            }
        }
    }

    public void InvokePre(ref CTriggerMultipleMultiWaitOverPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CTriggerMultipleMultiWaitOverPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerMultipleMultiWaitOver);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerMultipleMultiWaitOver);
        }
    }

    public void Invoke(CTriggerMultiple schemaObject) => DatamapHooksPublisher.InvokeCTriggerMultipleMultiWaitOver(schemaObject.Address);
}