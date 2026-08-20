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
    private delegate void CTriggerHurtNavThinkDelegate(nint a1);

    private static IUnmanagedFunction<CTriggerHurtNavThinkDelegate>? CTriggerHurtNavThinkUnmanagedFunction;
    private static Guid CTriggerHurtNavThinkHookGuid;

    private static IUnmanagedFunction<CTriggerHurtNavThinkDelegate> CTriggerHurtNavThinkGetUnmanagedFunction()
    {
        if (CTriggerHurtNavThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CTriggerHurt", "CTriggerHurtNavThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CTriggerHurt::CTriggerHurtNavThink.");
            }
            CTriggerHurtNavThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CTriggerHurtNavThinkDelegate>(address);
        }
        return CTriggerHurtNavThinkUnmanagedFunction;
    }

    internal static Guid HookCTriggerHurtNavThink()
    {
        CTriggerHurtNavThinkHookGuid = CTriggerHurtNavThinkGetUnmanagedFunction().AddHook(next => (a1) => CTriggerHurtNavThinkPipeline(a1, () => next()(a1)));
        return CTriggerHurtNavThinkHookGuid;
    }

    internal static Guid UnhookCTriggerHurtNavThink()
    {
        CTriggerHurtNavThinkGetUnmanagedFunction().RemoveHook(CTriggerHurtNavThinkHookGuid);
        return Guid.Empty;
    }

    private static void CTriggerHurtNavThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CTriggerHurt>(a1);

            var preCtx = new CTriggerHurtNavThinkPreContext { SchemaObject = schemaObject };
            InvokeCTriggerHurtNavThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CTriggerHurtNavThinkPostContext { SchemaObject = schemaObject };
            InvokeCTriggerHurtNavThinkPost(ref postCtx);
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

    internal static void InvokeCTriggerHurtNavThink(nint a1)
    {
        CTriggerHurtNavThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCTriggerHurtNavThinkPre(ref CTriggerHurtNavThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerHurtNavThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCTriggerHurtNavThinkPost(ref CTriggerHurtNavThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerHurtNavThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CTriggerHurtNavThinkHook : ICTriggerHurtNavThinkHook
{
    private event OnCTriggerHurtNavThinkPreDelegate? _Pre;
    private event OnCTriggerHurtNavThinkPostDelegate? _Post;

    public event OnCTriggerHurtNavThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerHurtNavThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerHurtNavThink);
            }
        }
    }

    public event OnCTriggerHurtNavThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerHurtNavThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerHurtNavThink);
            }
        }
    }

    public void InvokePre(ref CTriggerHurtNavThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CTriggerHurtNavThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerHurtNavThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerHurtNavThink);
        }
    }

    public void Invoke(CTriggerHurt schemaObject) => DatamapHooksPublisher.InvokeCTriggerHurtNavThink(schemaObject.Address);
}