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
    private delegate void CTriggerFanPushThinkDelegate(nint a1);

    private static IUnmanagedFunction<CTriggerFanPushThinkDelegate>? CTriggerFanPushThinkUnmanagedFunction;
    private static Guid CTriggerFanPushThinkHookGuid;

    private static IUnmanagedFunction<CTriggerFanPushThinkDelegate> CTriggerFanPushThinkGetUnmanagedFunction()
    {
        if (CTriggerFanPushThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CTriggerFan", "CTriggerFanPushThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CTriggerFan::CTriggerFanPushThink.");
            }
            CTriggerFanPushThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CTriggerFanPushThinkDelegate>(address);
        }
        return CTriggerFanPushThinkUnmanagedFunction;
    }

    internal static Guid HookCTriggerFanPushThink()
    {
        CTriggerFanPushThinkHookGuid = CTriggerFanPushThinkGetUnmanagedFunction().AddHook(next => (a1) => CTriggerFanPushThinkPipeline(a1, () => next()(a1)));
        return CTriggerFanPushThinkHookGuid;
    }

    internal static Guid UnhookCTriggerFanPushThink()
    {
        CTriggerFanPushThinkGetUnmanagedFunction().RemoveHook(CTriggerFanPushThinkHookGuid);
        return Guid.Empty;
    }

    private static void CTriggerFanPushThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CTriggerFan>(a1);

            var preCtx = new CTriggerFanPushThinkPreContext { SchemaObject = schemaObject };
            InvokeCTriggerFanPushThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CTriggerFanPushThinkPostContext { SchemaObject = schemaObject };
            InvokeCTriggerFanPushThinkPost(ref postCtx);
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

    internal static void InvokeCTriggerFanPushThink(nint a1)
    {
        CTriggerFanPushThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCTriggerFanPushThinkPre(ref CTriggerFanPushThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerFanPushThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCTriggerFanPushThinkPost(ref CTriggerFanPushThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerFanPushThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CTriggerFanPushThinkHook : ICTriggerFanPushThinkHook
{
    private event OnCTriggerFanPushThinkPreDelegate? _Pre;
    private event OnCTriggerFanPushThinkPostDelegate? _Post;

    public event OnCTriggerFanPushThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerFanPushThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerFanPushThink);
            }
        }
    }

    public event OnCTriggerFanPushThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerFanPushThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerFanPushThink);
            }
        }
    }

    public void InvokePre(ref CTriggerFanPushThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CTriggerFanPushThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerFanPushThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerFanPushThink);
        }
    }

    public void Invoke(CTriggerFan schemaObject) => DatamapHooksPublisher.InvokeCTriggerFanPushThink(schemaObject.Address);
}