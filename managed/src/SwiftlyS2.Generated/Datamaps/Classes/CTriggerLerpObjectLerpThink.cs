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
    private delegate void CTriggerLerpObjectLerpThinkDelegate(nint a1);

    private static IUnmanagedFunction<CTriggerLerpObjectLerpThinkDelegate>? CTriggerLerpObjectLerpThinkUnmanagedFunction;
    private static Guid CTriggerLerpObjectLerpThinkHookGuid;

    private static IUnmanagedFunction<CTriggerLerpObjectLerpThinkDelegate> CTriggerLerpObjectLerpThinkGetUnmanagedFunction()
    {
        if (CTriggerLerpObjectLerpThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CTriggerLerpObject", "CTriggerLerpObjectLerpThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CTriggerLerpObject::CTriggerLerpObjectLerpThink.");
            }
            CTriggerLerpObjectLerpThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CTriggerLerpObjectLerpThinkDelegate>(address);
        }
        return CTriggerLerpObjectLerpThinkUnmanagedFunction;
    }

    internal static Guid HookCTriggerLerpObjectLerpThink()
    {
        CTriggerLerpObjectLerpThinkHookGuid = CTriggerLerpObjectLerpThinkGetUnmanagedFunction().AddHook(next => (a1) => CTriggerLerpObjectLerpThinkPipeline(a1, () => next()(a1)));
        return CTriggerLerpObjectLerpThinkHookGuid;
    }

    internal static Guid UnhookCTriggerLerpObjectLerpThink()
    {
        CTriggerLerpObjectLerpThinkGetUnmanagedFunction().RemoveHook(CTriggerLerpObjectLerpThinkHookGuid);
        return Guid.Empty;
    }

    private static void CTriggerLerpObjectLerpThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CTriggerLerpObject>(a1);

            var preCtx = new CTriggerLerpObjectLerpThinkPreContext { SchemaObject = schemaObject };
            InvokeCTriggerLerpObjectLerpThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CTriggerLerpObjectLerpThinkPostContext { SchemaObject = schemaObject };
            InvokeCTriggerLerpObjectLerpThinkPost(ref postCtx);
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

    internal static void InvokeCTriggerLerpObjectLerpThink(nint a1)
    {
        CTriggerLerpObjectLerpThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCTriggerLerpObjectLerpThinkPre(ref CTriggerLerpObjectLerpThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerLerpObjectLerpThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCTriggerLerpObjectLerpThinkPost(ref CTriggerLerpObjectLerpThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerLerpObjectLerpThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CTriggerLerpObjectLerpThinkHook : ICTriggerLerpObjectLerpThinkHook
{
    private event OnCTriggerLerpObjectLerpThinkPreDelegate? _Pre;
    private event OnCTriggerLerpObjectLerpThinkPostDelegate? _Post;

    public event OnCTriggerLerpObjectLerpThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerLerpObjectLerpThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerLerpObjectLerpThink);
            }
        }
    }

    public event OnCTriggerLerpObjectLerpThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerLerpObjectLerpThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerLerpObjectLerpThink);
            }
        }
    }

    public void InvokePre(ref CTriggerLerpObjectLerpThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CTriggerLerpObjectLerpThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerLerpObjectLerpThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerLerpObjectLerpThink);
        }
    }

    public void Invoke(CTriggerLerpObject schemaObject) => DatamapHooksPublisher.InvokeCTriggerLerpObjectLerpThink(schemaObject.Address);
}