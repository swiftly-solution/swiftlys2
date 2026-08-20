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
    private delegate void CSoundEventConeEntitySoundEventConeThinkDelegate(nint a1);

    private static IUnmanagedFunction<CSoundEventConeEntitySoundEventConeThinkDelegate>? CSoundEventConeEntitySoundEventConeThinkUnmanagedFunction;
    private static Guid CSoundEventConeEntitySoundEventConeThinkHookGuid;

    private static IUnmanagedFunction<CSoundEventConeEntitySoundEventConeThinkDelegate> CSoundEventConeEntitySoundEventConeThinkGetUnmanagedFunction()
    {
        if (CSoundEventConeEntitySoundEventConeThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSoundEventConeEntity", "CSoundEventConeEntitySoundEventConeThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSoundEventConeEntity::CSoundEventConeEntitySoundEventConeThink.");
            }
            CSoundEventConeEntitySoundEventConeThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSoundEventConeEntitySoundEventConeThinkDelegate>(address);
        }
        return CSoundEventConeEntitySoundEventConeThinkUnmanagedFunction;
    }

    internal static Guid HookCSoundEventConeEntitySoundEventConeThink()
    {
        CSoundEventConeEntitySoundEventConeThinkHookGuid = CSoundEventConeEntitySoundEventConeThinkGetUnmanagedFunction().AddHook(next => (a1) => CSoundEventConeEntitySoundEventConeThinkPipeline(a1, () => next()(a1)));
        return CSoundEventConeEntitySoundEventConeThinkHookGuid;
    }

    internal static Guid UnhookCSoundEventConeEntitySoundEventConeThink()
    {
        CSoundEventConeEntitySoundEventConeThinkGetUnmanagedFunction().RemoveHook(CSoundEventConeEntitySoundEventConeThinkHookGuid);
        return Guid.Empty;
    }

    private static void CSoundEventConeEntitySoundEventConeThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSoundEventConeEntity>(a1);

            var preCtx = new CSoundEventConeEntitySoundEventConeThinkPreContext { SchemaObject = schemaObject };
            InvokeCSoundEventConeEntitySoundEventConeThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSoundEventConeEntitySoundEventConeThinkPostContext { SchemaObject = schemaObject };
            InvokeCSoundEventConeEntitySoundEventConeThinkPost(ref postCtx);
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

    internal static void InvokeCSoundEventConeEntitySoundEventConeThink(nint a1)
    {
        CSoundEventConeEntitySoundEventConeThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSoundEventConeEntitySoundEventConeThinkPre(ref CSoundEventConeEntitySoundEventConeThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundEventConeEntitySoundEventConeThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSoundEventConeEntitySoundEventConeThinkPost(ref CSoundEventConeEntitySoundEventConeThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundEventConeEntitySoundEventConeThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSoundEventConeEntitySoundEventConeThinkHook : ICSoundEventConeEntitySoundEventConeThinkHook
{
    private event OnCSoundEventConeEntitySoundEventConeThinkPreDelegate? _Pre;
    private event OnCSoundEventConeEntitySoundEventConeThinkPostDelegate? _Post;

    public event OnCSoundEventConeEntitySoundEventConeThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundEventConeEntitySoundEventConeThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventConeEntitySoundEventConeThink);
            }
        }
    }

    public event OnCSoundEventConeEntitySoundEventConeThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundEventConeEntitySoundEventConeThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventConeEntitySoundEventConeThink);
            }
        }
    }

    public void InvokePre(ref CSoundEventConeEntitySoundEventConeThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSoundEventConeEntitySoundEventConeThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventConeEntitySoundEventConeThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventConeEntitySoundEventConeThink);
        }
    }

    public void Invoke(CSoundEventConeEntity schemaObject) => DatamapHooksPublisher.InvokeCSoundEventConeEntitySoundEventConeThink(schemaObject.Address);
}