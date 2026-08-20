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
    private delegate void CTriggerProximityMeasureThinkDelegate(nint a1);

    private static IUnmanagedFunction<CTriggerProximityMeasureThinkDelegate>? CTriggerProximityMeasureThinkUnmanagedFunction;
    private static Guid CTriggerProximityMeasureThinkHookGuid;

    private static IUnmanagedFunction<CTriggerProximityMeasureThinkDelegate> CTriggerProximityMeasureThinkGetUnmanagedFunction()
    {
        if (CTriggerProximityMeasureThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CTriggerProximity", "CTriggerProximityMeasureThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CTriggerProximity::CTriggerProximityMeasureThink.");
            }
            CTriggerProximityMeasureThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CTriggerProximityMeasureThinkDelegate>(address);
        }
        return CTriggerProximityMeasureThinkUnmanagedFunction;
    }

    internal static Guid HookCTriggerProximityMeasureThink()
    {
        CTriggerProximityMeasureThinkHookGuid = CTriggerProximityMeasureThinkGetUnmanagedFunction().AddHook(next => (a1) => CTriggerProximityMeasureThinkPipeline(a1, () => next()(a1)));
        return CTriggerProximityMeasureThinkHookGuid;
    }

    internal static Guid UnhookCTriggerProximityMeasureThink()
    {
        CTriggerProximityMeasureThinkGetUnmanagedFunction().RemoveHook(CTriggerProximityMeasureThinkHookGuid);
        return Guid.Empty;
    }

    private static void CTriggerProximityMeasureThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CTriggerProximity>(a1);

            var preCtx = new CTriggerProximityMeasureThinkPreContext { SchemaObject = schemaObject };
            InvokeCTriggerProximityMeasureThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CTriggerProximityMeasureThinkPostContext { SchemaObject = schemaObject };
            InvokeCTriggerProximityMeasureThinkPost(ref postCtx);
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

    internal static void InvokeCTriggerProximityMeasureThink(nint a1)
    {
        CTriggerProximityMeasureThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCTriggerProximityMeasureThinkPre(ref CTriggerProximityMeasureThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerProximityMeasureThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCTriggerProximityMeasureThinkPost(ref CTriggerProximityMeasureThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerProximityMeasureThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CTriggerProximityMeasureThinkHook : ICTriggerProximityMeasureThinkHook
{
    private event OnCTriggerProximityMeasureThinkPreDelegate? _Pre;
    private event OnCTriggerProximityMeasureThinkPostDelegate? _Post;

    public event OnCTriggerProximityMeasureThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerProximityMeasureThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerProximityMeasureThink);
            }
        }
    }

    public event OnCTriggerProximityMeasureThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerProximityMeasureThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerProximityMeasureThink);
            }
        }
    }

    public void InvokePre(ref CTriggerProximityMeasureThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CTriggerProximityMeasureThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerProximityMeasureThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerProximityMeasureThink);
        }
    }

    public void Invoke(CTriggerProximity schemaObject) => DatamapHooksPublisher.InvokeCTriggerProximityMeasureThink(schemaObject.Address);
}