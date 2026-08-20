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
    private delegate void CTriggerLookTimeoutThinkDelegate(nint a1);

    private static IUnmanagedFunction<CTriggerLookTimeoutThinkDelegate>? CTriggerLookTimeoutThinkUnmanagedFunction;
    private static Guid CTriggerLookTimeoutThinkHookGuid;

    private static IUnmanagedFunction<CTriggerLookTimeoutThinkDelegate> CTriggerLookTimeoutThinkGetUnmanagedFunction()
    {
        if (CTriggerLookTimeoutThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CTriggerLook", "CTriggerLookTimeoutThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CTriggerLook::CTriggerLookTimeoutThink.");
            }
            CTriggerLookTimeoutThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CTriggerLookTimeoutThinkDelegate>(address);
        }
        return CTriggerLookTimeoutThinkUnmanagedFunction;
    }

    internal static Guid HookCTriggerLookTimeoutThink()
    {
        CTriggerLookTimeoutThinkHookGuid = CTriggerLookTimeoutThinkGetUnmanagedFunction().AddHook(next => (a1) => CTriggerLookTimeoutThinkPipeline(a1, () => next()(a1)));
        return CTriggerLookTimeoutThinkHookGuid;
    }

    internal static Guid UnhookCTriggerLookTimeoutThink()
    {
        CTriggerLookTimeoutThinkGetUnmanagedFunction().RemoveHook(CTriggerLookTimeoutThinkHookGuid);
        return Guid.Empty;
    }

    private static void CTriggerLookTimeoutThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CTriggerLook>(a1);

            var preCtx = new CTriggerLookTimeoutThinkPreContext { SchemaObject = schemaObject };
            InvokeCTriggerLookTimeoutThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CTriggerLookTimeoutThinkPostContext { SchemaObject = schemaObject };
            InvokeCTriggerLookTimeoutThinkPost(ref postCtx);
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

    internal static void InvokeCTriggerLookTimeoutThink(nint a1)
    {
        CTriggerLookTimeoutThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCTriggerLookTimeoutThinkPre(ref CTriggerLookTimeoutThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerLookTimeoutThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCTriggerLookTimeoutThinkPost(ref CTriggerLookTimeoutThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerLookTimeoutThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CTriggerLookTimeoutThinkHook : ICTriggerLookTimeoutThinkHook
{
    private event OnCTriggerLookTimeoutThinkPreDelegate? _Pre;
    private event OnCTriggerLookTimeoutThinkPostDelegate? _Post;

    public event OnCTriggerLookTimeoutThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerLookTimeoutThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerLookTimeoutThink);
            }
        }
    }

    public event OnCTriggerLookTimeoutThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerLookTimeoutThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerLookTimeoutThink);
            }
        }
    }

    public void InvokePre(ref CTriggerLookTimeoutThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CTriggerLookTimeoutThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerLookTimeoutThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerLookTimeoutThink);
        }
    }

    public void Invoke(CTriggerLook schemaObject) => DatamapHooksPublisher.InvokeCTriggerLookTimeoutThink(schemaObject.Address);
}