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
    private delegate void CTriggerLerpObjectAttachedEntityThinkDelegate(nint a1);

    private static IUnmanagedFunction<CTriggerLerpObjectAttachedEntityThinkDelegate>? CTriggerLerpObjectAttachedEntityThinkUnmanagedFunction;
    private static Guid CTriggerLerpObjectAttachedEntityThinkHookGuid;

    private static IUnmanagedFunction<CTriggerLerpObjectAttachedEntityThinkDelegate> CTriggerLerpObjectAttachedEntityThinkGetUnmanagedFunction()
    {
        if (CTriggerLerpObjectAttachedEntityThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CTriggerLerpObject", "CTriggerLerpObjectAttachedEntityThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CTriggerLerpObject::CTriggerLerpObjectAttachedEntityThink.");
            }
            CTriggerLerpObjectAttachedEntityThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CTriggerLerpObjectAttachedEntityThinkDelegate>(address);
        }
        return CTriggerLerpObjectAttachedEntityThinkUnmanagedFunction;
    }

    internal static Guid HookCTriggerLerpObjectAttachedEntityThink()
    {
        CTriggerLerpObjectAttachedEntityThinkHookGuid = CTriggerLerpObjectAttachedEntityThinkGetUnmanagedFunction().AddHook(next => (a1) => CTriggerLerpObjectAttachedEntityThinkPipeline(a1, () => next()(a1)));
        return CTriggerLerpObjectAttachedEntityThinkHookGuid;
    }

    internal static Guid UnhookCTriggerLerpObjectAttachedEntityThink()
    {
        CTriggerLerpObjectAttachedEntityThinkGetUnmanagedFunction().RemoveHook(CTriggerLerpObjectAttachedEntityThinkHookGuid);
        return Guid.Empty;
    }

    private static void CTriggerLerpObjectAttachedEntityThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CTriggerLerpObject>(a1);

            var preCtx = new CTriggerLerpObjectAttachedEntityThinkPreContext { SchemaObject = schemaObject };
            InvokeCTriggerLerpObjectAttachedEntityThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CTriggerLerpObjectAttachedEntityThinkPostContext { SchemaObject = schemaObject };
            InvokeCTriggerLerpObjectAttachedEntityThinkPost(ref postCtx);
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

    internal static void InvokeCTriggerLerpObjectAttachedEntityThink(nint a1)
    {
        CTriggerLerpObjectAttachedEntityThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCTriggerLerpObjectAttachedEntityThinkPre(ref CTriggerLerpObjectAttachedEntityThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerLerpObjectAttachedEntityThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCTriggerLerpObjectAttachedEntityThinkPost(ref CTriggerLerpObjectAttachedEntityThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerLerpObjectAttachedEntityThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CTriggerLerpObjectAttachedEntityThinkHook : ICTriggerLerpObjectAttachedEntityThinkHook
{
    private event OnCTriggerLerpObjectAttachedEntityThinkPreDelegate? _Pre;
    private event OnCTriggerLerpObjectAttachedEntityThinkPostDelegate? _Post;

    public event OnCTriggerLerpObjectAttachedEntityThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerLerpObjectAttachedEntityThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerLerpObjectAttachedEntityThink);
            }
        }
    }

    public event OnCTriggerLerpObjectAttachedEntityThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerLerpObjectAttachedEntityThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerLerpObjectAttachedEntityThink);
            }
        }
    }

    public void InvokePre(ref CTriggerLerpObjectAttachedEntityThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CTriggerLerpObjectAttachedEntityThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerLerpObjectAttachedEntityThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerLerpObjectAttachedEntityThink);
        }
    }

    public void Invoke(CTriggerLerpObject schemaObject) => DatamapHooksPublisher.InvokeCTriggerLerpObjectAttachedEntityThink(schemaObject.Address);
}