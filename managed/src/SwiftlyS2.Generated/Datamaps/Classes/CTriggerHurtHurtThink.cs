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
    private delegate void CTriggerHurtHurtThinkDelegate(nint a1);

    private static IUnmanagedFunction<CTriggerHurtHurtThinkDelegate>? CTriggerHurtHurtThinkUnmanagedFunction;
    private static Guid CTriggerHurtHurtThinkHookGuid;

    private static IUnmanagedFunction<CTriggerHurtHurtThinkDelegate> CTriggerHurtHurtThinkGetUnmanagedFunction()
    {
        if (CTriggerHurtHurtThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CTriggerHurt", "CTriggerHurtHurtThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CTriggerHurt::CTriggerHurtHurtThink.");
            }
            CTriggerHurtHurtThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CTriggerHurtHurtThinkDelegate>(address);
        }
        return CTriggerHurtHurtThinkUnmanagedFunction;
    }

    internal static Guid HookCTriggerHurtHurtThink()
    {
        CTriggerHurtHurtThinkHookGuid = CTriggerHurtHurtThinkGetUnmanagedFunction().AddHook(next => (a1) => CTriggerHurtHurtThinkPipeline(a1, () => next()(a1)));
        return CTriggerHurtHurtThinkHookGuid;
    }

    internal static Guid UnhookCTriggerHurtHurtThink()
    {
        CTriggerHurtHurtThinkGetUnmanagedFunction().RemoveHook(CTriggerHurtHurtThinkHookGuid);
        return Guid.Empty;
    }

    private static void CTriggerHurtHurtThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CTriggerHurt>(a1);

            var preCtx = new CTriggerHurtHurtThinkPreContext { SchemaObject = schemaObject };
            InvokeCTriggerHurtHurtThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CTriggerHurtHurtThinkPostContext { SchemaObject = schemaObject };
            InvokeCTriggerHurtHurtThinkPost(ref postCtx);
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

    internal static void InvokeCTriggerHurtHurtThink(nint a1)
    {
        CTriggerHurtHurtThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCTriggerHurtHurtThinkPre(ref CTriggerHurtHurtThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerHurtHurtThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCTriggerHurtHurtThinkPost(ref CTriggerHurtHurtThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerHurtHurtThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CTriggerHurtHurtThinkHook : ICTriggerHurtHurtThinkHook
{
    private event OnCTriggerHurtHurtThinkPreDelegate? _Pre;
    private event OnCTriggerHurtHurtThinkPostDelegate? _Post;

    public event OnCTriggerHurtHurtThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerHurtHurtThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerHurtHurtThink);
            }
        }
    }

    public event OnCTriggerHurtHurtThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerHurtHurtThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerHurtHurtThink);
            }
        }
    }

    public void InvokePre(ref CTriggerHurtHurtThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CTriggerHurtHurtThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerHurtHurtThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerHurtHurtThink);
        }
    }

    public void Invoke(CTriggerHurt schemaObject) => DatamapHooksPublisher.InvokeCTriggerHurtHurtThink(schemaObject.Address);
}