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
    private delegate void CTriggerHurtRadiationThinkDelegate(nint a1);

    private static IUnmanagedFunction<CTriggerHurtRadiationThinkDelegate>? CTriggerHurtRadiationThinkUnmanagedFunction;
    private static Guid CTriggerHurtRadiationThinkHookGuid;

    private static IUnmanagedFunction<CTriggerHurtRadiationThinkDelegate> CTriggerHurtRadiationThinkGetUnmanagedFunction()
    {
        if (CTriggerHurtRadiationThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CTriggerHurt", "CTriggerHurtRadiationThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CTriggerHurt::CTriggerHurtRadiationThink.");
            }
            CTriggerHurtRadiationThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CTriggerHurtRadiationThinkDelegate>(address);
        }
        return CTriggerHurtRadiationThinkUnmanagedFunction;
    }

    internal static Guid HookCTriggerHurtRadiationThink()
    {
        CTriggerHurtRadiationThinkHookGuid = CTriggerHurtRadiationThinkGetUnmanagedFunction().AddHook(next => (a1) => CTriggerHurtRadiationThinkPipeline(a1, () => next()(a1)));
        return CTriggerHurtRadiationThinkHookGuid;
    }

    internal static Guid UnhookCTriggerHurtRadiationThink()
    {
        CTriggerHurtRadiationThinkGetUnmanagedFunction().RemoveHook(CTriggerHurtRadiationThinkHookGuid);
        return Guid.Empty;
    }

    private static void CTriggerHurtRadiationThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CTriggerHurt>(a1);

            var preCtx = new CTriggerHurtRadiationThinkPreContext { SchemaObject = schemaObject };
            InvokeCTriggerHurtRadiationThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CTriggerHurtRadiationThinkPostContext { SchemaObject = schemaObject };
            InvokeCTriggerHurtRadiationThinkPost(ref postCtx);
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

    internal static void InvokeCTriggerHurtRadiationThink(nint a1)
    {
        CTriggerHurtRadiationThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCTriggerHurtRadiationThinkPre(ref CTriggerHurtRadiationThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerHurtRadiationThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCTriggerHurtRadiationThinkPost(ref CTriggerHurtRadiationThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerHurtRadiationThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CTriggerHurtRadiationThinkHook : ICTriggerHurtRadiationThinkHook
{
    private event OnCTriggerHurtRadiationThinkPreDelegate? _Pre;
    private event OnCTriggerHurtRadiationThinkPostDelegate? _Post;

    public event OnCTriggerHurtRadiationThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerHurtRadiationThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerHurtRadiationThink);
            }
        }
    }

    public event OnCTriggerHurtRadiationThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerHurtRadiationThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerHurtRadiationThink);
            }
        }
    }

    public void InvokePre(ref CTriggerHurtRadiationThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CTriggerHurtRadiationThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerHurtRadiationThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerHurtRadiationThink);
        }
    }

    public void Invoke(CTriggerHurt schemaObject) => DatamapHooksPublisher.InvokeCTriggerHurtRadiationThink(schemaObject.Address);
}