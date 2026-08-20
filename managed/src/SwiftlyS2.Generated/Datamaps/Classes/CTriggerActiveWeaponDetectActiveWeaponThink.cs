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
    private delegate void CTriggerActiveWeaponDetectActiveWeaponThinkDelegate(nint a1);

    private static IUnmanagedFunction<CTriggerActiveWeaponDetectActiveWeaponThinkDelegate>? CTriggerActiveWeaponDetectActiveWeaponThinkUnmanagedFunction;
    private static Guid CTriggerActiveWeaponDetectActiveWeaponThinkHookGuid;

    private static IUnmanagedFunction<CTriggerActiveWeaponDetectActiveWeaponThinkDelegate> CTriggerActiveWeaponDetectActiveWeaponThinkGetUnmanagedFunction()
    {
        if (CTriggerActiveWeaponDetectActiveWeaponThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CTriggerActiveWeaponDetect", "CTriggerActiveWeaponDetectActiveWeaponThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CTriggerActiveWeaponDetect::CTriggerActiveWeaponDetectActiveWeaponThink.");
            }
            CTriggerActiveWeaponDetectActiveWeaponThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CTriggerActiveWeaponDetectActiveWeaponThinkDelegate>(address);
        }
        return CTriggerActiveWeaponDetectActiveWeaponThinkUnmanagedFunction;
    }

    internal static Guid HookCTriggerActiveWeaponDetectActiveWeaponThink()
    {
        CTriggerActiveWeaponDetectActiveWeaponThinkHookGuid = CTriggerActiveWeaponDetectActiveWeaponThinkGetUnmanagedFunction().AddHook(next => (a1) => CTriggerActiveWeaponDetectActiveWeaponThinkPipeline(a1, () => next()(a1)));
        return CTriggerActiveWeaponDetectActiveWeaponThinkHookGuid;
    }

    internal static Guid UnhookCTriggerActiveWeaponDetectActiveWeaponThink()
    {
        CTriggerActiveWeaponDetectActiveWeaponThinkGetUnmanagedFunction().RemoveHook(CTriggerActiveWeaponDetectActiveWeaponThinkHookGuid);
        return Guid.Empty;
    }

    private static void CTriggerActiveWeaponDetectActiveWeaponThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CTriggerActiveWeaponDetect>(a1);

            var preCtx = new CTriggerActiveWeaponDetectActiveWeaponThinkPreContext { SchemaObject = schemaObject };
            InvokeCTriggerActiveWeaponDetectActiveWeaponThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CTriggerActiveWeaponDetectActiveWeaponThinkPostContext { SchemaObject = schemaObject };
            InvokeCTriggerActiveWeaponDetectActiveWeaponThinkPost(ref postCtx);
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

    internal static void InvokeCTriggerActiveWeaponDetectActiveWeaponThink(nint a1)
    {
        CTriggerActiveWeaponDetectActiveWeaponThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCTriggerActiveWeaponDetectActiveWeaponThinkPre(ref CTriggerActiveWeaponDetectActiveWeaponThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerActiveWeaponDetectActiveWeaponThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCTriggerActiveWeaponDetectActiveWeaponThinkPost(ref CTriggerActiveWeaponDetectActiveWeaponThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerActiveWeaponDetectActiveWeaponThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CTriggerActiveWeaponDetectActiveWeaponThinkHook : ICTriggerActiveWeaponDetectActiveWeaponThinkHook
{
    private event OnCTriggerActiveWeaponDetectActiveWeaponThinkPreDelegate? _Pre;
    private event OnCTriggerActiveWeaponDetectActiveWeaponThinkPostDelegate? _Post;

    public event OnCTriggerActiveWeaponDetectActiveWeaponThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerActiveWeaponDetectActiveWeaponThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerActiveWeaponDetectActiveWeaponThink);
            }
        }
    }

    public event OnCTriggerActiveWeaponDetectActiveWeaponThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerActiveWeaponDetectActiveWeaponThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerActiveWeaponDetectActiveWeaponThink);
            }
        }
    }

    public void InvokePre(ref CTriggerActiveWeaponDetectActiveWeaponThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CTriggerActiveWeaponDetectActiveWeaponThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerActiveWeaponDetectActiveWeaponThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerActiveWeaponDetectActiveWeaponThink);
        }
    }

    public void Invoke(CTriggerActiveWeaponDetect schemaObject) => DatamapHooksPublisher.InvokeCTriggerActiveWeaponDetectActiveWeaponThink(schemaObject.Address);
}