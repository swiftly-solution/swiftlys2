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
    private delegate void CTriggerSoundscapePlayerUpdateThinkDelegate(nint a1);

    private static IUnmanagedFunction<CTriggerSoundscapePlayerUpdateThinkDelegate>? CTriggerSoundscapePlayerUpdateThinkUnmanagedFunction;
    private static Guid CTriggerSoundscapePlayerUpdateThinkHookGuid;

    private static IUnmanagedFunction<CTriggerSoundscapePlayerUpdateThinkDelegate> CTriggerSoundscapePlayerUpdateThinkGetUnmanagedFunction()
    {
        if (CTriggerSoundscapePlayerUpdateThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CTriggerSoundscape", "CTriggerSoundscapePlayerUpdateThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CTriggerSoundscape::CTriggerSoundscapePlayerUpdateThink.");
            }
            CTriggerSoundscapePlayerUpdateThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CTriggerSoundscapePlayerUpdateThinkDelegate>(address);
        }
        return CTriggerSoundscapePlayerUpdateThinkUnmanagedFunction;
    }

    internal static Guid HookCTriggerSoundscapePlayerUpdateThink()
    {
        CTriggerSoundscapePlayerUpdateThinkHookGuid = CTriggerSoundscapePlayerUpdateThinkGetUnmanagedFunction().AddHook(next => (a1) => CTriggerSoundscapePlayerUpdateThinkPipeline(a1, () => next()(a1)));
        return CTriggerSoundscapePlayerUpdateThinkHookGuid;
    }

    internal static Guid UnhookCTriggerSoundscapePlayerUpdateThink()
    {
        CTriggerSoundscapePlayerUpdateThinkGetUnmanagedFunction().RemoveHook(CTriggerSoundscapePlayerUpdateThinkHookGuid);
        return Guid.Empty;
    }

    private static void CTriggerSoundscapePlayerUpdateThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CTriggerSoundscape>(a1);

            var preCtx = new CTriggerSoundscapePlayerUpdateThinkPreContext { SchemaObject = schemaObject };
            InvokeCTriggerSoundscapePlayerUpdateThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CTriggerSoundscapePlayerUpdateThinkPostContext { SchemaObject = schemaObject };
            InvokeCTriggerSoundscapePlayerUpdateThinkPost(ref postCtx);
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

    internal static void InvokeCTriggerSoundscapePlayerUpdateThink(nint a1)
    {
        CTriggerSoundscapePlayerUpdateThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCTriggerSoundscapePlayerUpdateThinkPre(ref CTriggerSoundscapePlayerUpdateThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerSoundscapePlayerUpdateThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCTriggerSoundscapePlayerUpdateThinkPost(ref CTriggerSoundscapePlayerUpdateThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerSoundscapePlayerUpdateThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CTriggerSoundscapePlayerUpdateThinkHook : ICTriggerSoundscapePlayerUpdateThinkHook
{
    private event OnCTriggerSoundscapePlayerUpdateThinkPreDelegate? _Pre;
    private event OnCTriggerSoundscapePlayerUpdateThinkPostDelegate? _Post;

    public event OnCTriggerSoundscapePlayerUpdateThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerSoundscapePlayerUpdateThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerSoundscapePlayerUpdateThink);
            }
        }
    }

    public event OnCTriggerSoundscapePlayerUpdateThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerSoundscapePlayerUpdateThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerSoundscapePlayerUpdateThink);
            }
        }
    }

    public void InvokePre(ref CTriggerSoundscapePlayerUpdateThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CTriggerSoundscapePlayerUpdateThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerSoundscapePlayerUpdateThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerSoundscapePlayerUpdateThink);
        }
    }

    public void Invoke(CTriggerSoundscape schemaObject) => DatamapHooksPublisher.InvokeCTriggerSoundscapePlayerUpdateThink(schemaObject.Address);
}