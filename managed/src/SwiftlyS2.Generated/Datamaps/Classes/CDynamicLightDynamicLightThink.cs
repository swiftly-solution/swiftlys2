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
    private delegate void CDynamicLightDynamicLightThinkDelegate(nint a1);

    private static IUnmanagedFunction<CDynamicLightDynamicLightThinkDelegate>? CDynamicLightDynamicLightThinkUnmanagedFunction;
    private static Guid CDynamicLightDynamicLightThinkHookGuid;

    private static IUnmanagedFunction<CDynamicLightDynamicLightThinkDelegate> CDynamicLightDynamicLightThinkGetUnmanagedFunction()
    {
        if (CDynamicLightDynamicLightThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CDynamicLight", "CDynamicLightDynamicLightThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CDynamicLight::CDynamicLightDynamicLightThink.");
            }
            CDynamicLightDynamicLightThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CDynamicLightDynamicLightThinkDelegate>(address);
        }
        return CDynamicLightDynamicLightThinkUnmanagedFunction;
    }

    internal static Guid HookCDynamicLightDynamicLightThink()
    {
        CDynamicLightDynamicLightThinkHookGuid = CDynamicLightDynamicLightThinkGetUnmanagedFunction().AddHook(next => (a1) => CDynamicLightDynamicLightThinkPipeline(a1, () => next()(a1)));
        return CDynamicLightDynamicLightThinkHookGuid;
    }

    internal static Guid UnhookCDynamicLightDynamicLightThink()
    {
        CDynamicLightDynamicLightThinkGetUnmanagedFunction().RemoveHook(CDynamicLightDynamicLightThinkHookGuid);
        return Guid.Empty;
    }

    private static void CDynamicLightDynamicLightThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CDynamicLight>(a1);

            var preCtx = new CDynamicLightDynamicLightThinkPreContext { SchemaObject = schemaObject };
            InvokeCDynamicLightDynamicLightThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CDynamicLightDynamicLightThinkPostContext { SchemaObject = schemaObject };
            InvokeCDynamicLightDynamicLightThinkPost(ref postCtx);
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

    internal static void InvokeCDynamicLightDynamicLightThink(nint a1)
    {
        CDynamicLightDynamicLightThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCDynamicLightDynamicLightThinkPre(ref CDynamicLightDynamicLightThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCDynamicLightDynamicLightThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCDynamicLightDynamicLightThinkPost(ref CDynamicLightDynamicLightThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCDynamicLightDynamicLightThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CDynamicLightDynamicLightThinkHook : ICDynamicLightDynamicLightThinkHook
{
    private event OnCDynamicLightDynamicLightThinkPreDelegate? _Pre;
    private event OnCDynamicLightDynamicLightThinkPostDelegate? _Post;

    public event OnCDynamicLightDynamicLightThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CDynamicLightDynamicLightThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CDynamicLightDynamicLightThink);
            }
        }
    }

    public event OnCDynamicLightDynamicLightThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CDynamicLightDynamicLightThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CDynamicLightDynamicLightThink);
            }
        }
    }

    public void InvokePre(ref CDynamicLightDynamicLightThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CDynamicLightDynamicLightThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CDynamicLightDynamicLightThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CDynamicLightDynamicLightThink);
        }
    }

    public void Invoke(CDynamicLight schemaObject) => DatamapHooksPublisher.InvokeCDynamicLightDynamicLightThink(schemaObject.Address);
}