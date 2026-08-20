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
    private delegate void CTriggerImpactDisableThinkDelegate(nint a1);

    private static IUnmanagedFunction<CTriggerImpactDisableThinkDelegate>? CTriggerImpactDisableThinkUnmanagedFunction;
    private static Guid CTriggerImpactDisableThinkHookGuid;

    private static IUnmanagedFunction<CTriggerImpactDisableThinkDelegate> CTriggerImpactDisableThinkGetUnmanagedFunction()
    {
        if (CTriggerImpactDisableThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CTriggerImpact", "CTriggerImpactDisableThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CTriggerImpact::CTriggerImpactDisableThink.");
            }
            CTriggerImpactDisableThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CTriggerImpactDisableThinkDelegate>(address);
        }
        return CTriggerImpactDisableThinkUnmanagedFunction;
    }

    internal static Guid HookCTriggerImpactDisableThink()
    {
        CTriggerImpactDisableThinkHookGuid = CTriggerImpactDisableThinkGetUnmanagedFunction().AddHook(next => (a1) => CTriggerImpactDisableThinkPipeline(a1, () => next()(a1)));
        return CTriggerImpactDisableThinkHookGuid;
    }

    internal static Guid UnhookCTriggerImpactDisableThink()
    {
        CTriggerImpactDisableThinkGetUnmanagedFunction().RemoveHook(CTriggerImpactDisableThinkHookGuid);
        return Guid.Empty;
    }

    private static void CTriggerImpactDisableThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CTriggerImpact>(a1);

            var preCtx = new CTriggerImpactDisableThinkPreContext { SchemaObject = schemaObject };
            InvokeCTriggerImpactDisableThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CTriggerImpactDisableThinkPostContext { SchemaObject = schemaObject };
            InvokeCTriggerImpactDisableThinkPost(ref postCtx);
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

    internal static void InvokeCTriggerImpactDisableThink(nint a1)
    {
        CTriggerImpactDisableThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCTriggerImpactDisableThinkPre(ref CTriggerImpactDisableThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerImpactDisableThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCTriggerImpactDisableThinkPost(ref CTriggerImpactDisableThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerImpactDisableThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CTriggerImpactDisableThinkHook : ICTriggerImpactDisableThinkHook
{
    private event OnCTriggerImpactDisableThinkPreDelegate? _Pre;
    private event OnCTriggerImpactDisableThinkPostDelegate? _Post;

    public event OnCTriggerImpactDisableThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerImpactDisableThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerImpactDisableThink);
            }
        }
    }

    public event OnCTriggerImpactDisableThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerImpactDisableThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerImpactDisableThink);
            }
        }
    }

    public void InvokePre(ref CTriggerImpactDisableThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CTriggerImpactDisableThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerImpactDisableThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerImpactDisableThink);
        }
    }

    public void Invoke(CTriggerImpact schemaObject) => DatamapHooksPublisher.InvokeCTriggerImpactDisableThink(schemaObject.Address);
}