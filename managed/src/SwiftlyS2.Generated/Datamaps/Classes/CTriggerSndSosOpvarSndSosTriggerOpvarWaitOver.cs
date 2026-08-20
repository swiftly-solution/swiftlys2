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
    private delegate void CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverDelegate(nint a1);

    private static IUnmanagedFunction<CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverDelegate>? CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverUnmanagedFunction;
    private static Guid CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHookGuid;

    private static IUnmanagedFunction<CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverDelegate> CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverGetUnmanagedFunction()
    {
        if (CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CTriggerSndSosOpvar", "CTriggerSndSosOpvarSndSosTriggerOpvarWaitOver");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CTriggerSndSosOpvar::CTriggerSndSosOpvarSndSosTriggerOpvarWaitOver.");
            }
            CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverDelegate>(address);
        }
        return CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverUnmanagedFunction;
    }

    internal static Guid HookCTriggerSndSosOpvarSndSosTriggerOpvarWaitOver()
    {
        CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHookGuid = CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverGetUnmanagedFunction().AddHook(next => (a1) => CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPipeline(a1, () => next()(a1)));
        return CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHookGuid;
    }

    internal static Guid UnhookCTriggerSndSosOpvarSndSosTriggerOpvarWaitOver()
    {
        CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverGetUnmanagedFunction().RemoveHook(CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHookGuid);
        return Guid.Empty;
    }

    private static void CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CTriggerSndSosOpvar>(a1);

            var preCtx = new CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPreContext { SchemaObject = schemaObject };
            InvokeCTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPostContext { SchemaObject = schemaObject };
            InvokeCTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPost(ref postCtx);
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

    internal static void InvokeCTriggerSndSosOpvarSndSosTriggerOpvarWaitOver(nint a1)
    {
        CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPre(ref CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPost(ref CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHook : ICTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHook
{
    private event OnCTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPreDelegate? _Pre;
    private event OnCTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPostDelegate? _Post;

    public event OnCTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerSndSosOpvarSndSosTriggerOpvarWaitOver);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerSndSosOpvarSndSosTriggerOpvarWaitOver);
            }
        }
    }

    public event OnCTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerSndSosOpvarSndSosTriggerOpvarWaitOver);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerSndSosOpvarSndSosTriggerOpvarWaitOver);
            }
        }
    }

    public void InvokePre(ref CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerSndSosOpvarSndSosTriggerOpvarWaitOver);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerSndSosOpvarSndSosTriggerOpvarWaitOver);
        }
    }

    public void Invoke(CTriggerSndSosOpvar schemaObject) => DatamapHooksPublisher.InvokeCTriggerSndSosOpvarSndSosTriggerOpvarWaitOver(schemaObject.Address);
}