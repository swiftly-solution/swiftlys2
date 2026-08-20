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
    private delegate void CSoundEventSphereEntitySoundEventSphereThinkDelegate(nint a1);

    private static IUnmanagedFunction<CSoundEventSphereEntitySoundEventSphereThinkDelegate>? CSoundEventSphereEntitySoundEventSphereThinkUnmanagedFunction;
    private static Guid CSoundEventSphereEntitySoundEventSphereThinkHookGuid;

    private static IUnmanagedFunction<CSoundEventSphereEntitySoundEventSphereThinkDelegate> CSoundEventSphereEntitySoundEventSphereThinkGetUnmanagedFunction()
    {
        if (CSoundEventSphereEntitySoundEventSphereThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSoundEventSphereEntity", "CSoundEventSphereEntitySoundEventSphereThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSoundEventSphereEntity::CSoundEventSphereEntitySoundEventSphereThink.");
            }
            CSoundEventSphereEntitySoundEventSphereThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSoundEventSphereEntitySoundEventSphereThinkDelegate>(address);
        }
        return CSoundEventSphereEntitySoundEventSphereThinkUnmanagedFunction;
    }

    internal static Guid HookCSoundEventSphereEntitySoundEventSphereThink()
    {
        CSoundEventSphereEntitySoundEventSphereThinkHookGuid = CSoundEventSphereEntitySoundEventSphereThinkGetUnmanagedFunction().AddHook(next => (a1) => CSoundEventSphereEntitySoundEventSphereThinkPipeline(a1, () => next()(a1)));
        return CSoundEventSphereEntitySoundEventSphereThinkHookGuid;
    }

    internal static Guid UnhookCSoundEventSphereEntitySoundEventSphereThink()
    {
        CSoundEventSphereEntitySoundEventSphereThinkGetUnmanagedFunction().RemoveHook(CSoundEventSphereEntitySoundEventSphereThinkHookGuid);
        return Guid.Empty;
    }

    private static void CSoundEventSphereEntitySoundEventSphereThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSoundEventSphereEntity>(a1);

            var preCtx = new CSoundEventSphereEntitySoundEventSphereThinkPreContext { SchemaObject = schemaObject };
            InvokeCSoundEventSphereEntitySoundEventSphereThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSoundEventSphereEntitySoundEventSphereThinkPostContext { SchemaObject = schemaObject };
            InvokeCSoundEventSphereEntitySoundEventSphereThinkPost(ref postCtx);
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

    internal static void InvokeCSoundEventSphereEntitySoundEventSphereThink(nint a1)
    {
        CSoundEventSphereEntitySoundEventSphereThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSoundEventSphereEntitySoundEventSphereThinkPre(ref CSoundEventSphereEntitySoundEventSphereThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundEventSphereEntitySoundEventSphereThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSoundEventSphereEntitySoundEventSphereThinkPost(ref CSoundEventSphereEntitySoundEventSphereThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundEventSphereEntitySoundEventSphereThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSoundEventSphereEntitySoundEventSphereThinkHook : ICSoundEventSphereEntitySoundEventSphereThinkHook
{
    private event OnCSoundEventSphereEntitySoundEventSphereThinkPreDelegate? _Pre;
    private event OnCSoundEventSphereEntitySoundEventSphereThinkPostDelegate? _Post;

    public event OnCSoundEventSphereEntitySoundEventSphereThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundEventSphereEntitySoundEventSphereThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventSphereEntitySoundEventSphereThink);
            }
        }
    }

    public event OnCSoundEventSphereEntitySoundEventSphereThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundEventSphereEntitySoundEventSphereThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventSphereEntitySoundEventSphereThink);
            }
        }
    }

    public void InvokePre(ref CSoundEventSphereEntitySoundEventSphereThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSoundEventSphereEntitySoundEventSphereThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventSphereEntitySoundEventSphereThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventSphereEntitySoundEventSphereThink);
        }
    }

    public void Invoke(CSoundEventSphereEntity schemaObject) => DatamapHooksPublisher.InvokeCSoundEventSphereEntitySoundEventSphereThink(schemaObject.Address);
}