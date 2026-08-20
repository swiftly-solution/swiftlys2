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
    private delegate void CDynamicPropAnimThinkDelegate(nint a1);

    private static IUnmanagedFunction<CDynamicPropAnimThinkDelegate>? CDynamicPropAnimThinkUnmanagedFunction;
    private static Guid CDynamicPropAnimThinkHookGuid;

    private static IUnmanagedFunction<CDynamicPropAnimThinkDelegate> CDynamicPropAnimThinkGetUnmanagedFunction()
    {
        if (CDynamicPropAnimThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CDynamicProp", "CDynamicPropAnimThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CDynamicProp::CDynamicPropAnimThink.");
            }
            CDynamicPropAnimThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CDynamicPropAnimThinkDelegate>(address);
        }
        return CDynamicPropAnimThinkUnmanagedFunction;
    }

    internal static Guid HookCDynamicPropAnimThink()
    {
        CDynamicPropAnimThinkHookGuid = CDynamicPropAnimThinkGetUnmanagedFunction().AddHook(next => (a1) => CDynamicPropAnimThinkPipeline(a1, () => next()(a1)));
        return CDynamicPropAnimThinkHookGuid;
    }

    internal static Guid UnhookCDynamicPropAnimThink()
    {
        CDynamicPropAnimThinkGetUnmanagedFunction().RemoveHook(CDynamicPropAnimThinkHookGuid);
        return Guid.Empty;
    }

    private static void CDynamicPropAnimThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CDynamicProp>(a1);

            var preCtx = new CDynamicPropAnimThinkPreContext { SchemaObject = schemaObject };
            InvokeCDynamicPropAnimThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CDynamicPropAnimThinkPostContext { SchemaObject = schemaObject };
            InvokeCDynamicPropAnimThinkPost(ref postCtx);
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

    internal static void InvokeCDynamicPropAnimThink(nint a1)
    {
        CDynamicPropAnimThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCDynamicPropAnimThinkPre(ref CDynamicPropAnimThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCDynamicPropAnimThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCDynamicPropAnimThinkPost(ref CDynamicPropAnimThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCDynamicPropAnimThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CDynamicPropAnimThinkHook : ICDynamicPropAnimThinkHook
{
    private event OnCDynamicPropAnimThinkPreDelegate? _Pre;
    private event OnCDynamicPropAnimThinkPostDelegate? _Post;

    public event OnCDynamicPropAnimThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CDynamicPropAnimThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CDynamicPropAnimThink);
            }
        }
    }

    public event OnCDynamicPropAnimThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CDynamicPropAnimThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CDynamicPropAnimThink);
            }
        }
    }

    public void InvokePre(ref CDynamicPropAnimThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CDynamicPropAnimThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CDynamicPropAnimThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CDynamicPropAnimThink);
        }
    }

    public void Invoke(CDynamicProp schemaObject) => DatamapHooksPublisher.InvokeCDynamicPropAnimThink(schemaObject.Address);
}