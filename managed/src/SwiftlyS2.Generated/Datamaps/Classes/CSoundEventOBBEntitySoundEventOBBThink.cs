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
    private delegate void CSoundEventOBBEntitySoundEventOBBThinkDelegate(nint a1);

    private static IUnmanagedFunction<CSoundEventOBBEntitySoundEventOBBThinkDelegate>? CSoundEventOBBEntitySoundEventOBBThinkUnmanagedFunction;
    private static Guid CSoundEventOBBEntitySoundEventOBBThinkHookGuid;

    private static IUnmanagedFunction<CSoundEventOBBEntitySoundEventOBBThinkDelegate> CSoundEventOBBEntitySoundEventOBBThinkGetUnmanagedFunction()
    {
        if (CSoundEventOBBEntitySoundEventOBBThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSoundEventOBBEntity", "CSoundEventOBBEntitySoundEventOBBThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSoundEventOBBEntity::CSoundEventOBBEntitySoundEventOBBThink.");
            }
            CSoundEventOBBEntitySoundEventOBBThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSoundEventOBBEntitySoundEventOBBThinkDelegate>(address);
        }
        return CSoundEventOBBEntitySoundEventOBBThinkUnmanagedFunction;
    }

    internal static Guid HookCSoundEventOBBEntitySoundEventOBBThink()
    {
        CSoundEventOBBEntitySoundEventOBBThinkHookGuid = CSoundEventOBBEntitySoundEventOBBThinkGetUnmanagedFunction().AddHook(next => (a1) => CSoundEventOBBEntitySoundEventOBBThinkPipeline(a1, () => next()(a1)));
        return CSoundEventOBBEntitySoundEventOBBThinkHookGuid;
    }

    internal static Guid UnhookCSoundEventOBBEntitySoundEventOBBThink()
    {
        CSoundEventOBBEntitySoundEventOBBThinkGetUnmanagedFunction().RemoveHook(CSoundEventOBBEntitySoundEventOBBThinkHookGuid);
        return Guid.Empty;
    }

    private static void CSoundEventOBBEntitySoundEventOBBThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSoundEventOBBEntity>(a1);

            var preCtx = new CSoundEventOBBEntitySoundEventOBBThinkPreContext { SchemaObject = schemaObject };
            InvokeCSoundEventOBBEntitySoundEventOBBThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSoundEventOBBEntitySoundEventOBBThinkPostContext { SchemaObject = schemaObject };
            InvokeCSoundEventOBBEntitySoundEventOBBThinkPost(ref postCtx);
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

    internal static void InvokeCSoundEventOBBEntitySoundEventOBBThink(nint a1)
    {
        CSoundEventOBBEntitySoundEventOBBThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSoundEventOBBEntitySoundEventOBBThinkPre(ref CSoundEventOBBEntitySoundEventOBBThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundEventOBBEntitySoundEventOBBThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSoundEventOBBEntitySoundEventOBBThinkPost(ref CSoundEventOBBEntitySoundEventOBBThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundEventOBBEntitySoundEventOBBThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSoundEventOBBEntitySoundEventOBBThinkHook : ICSoundEventOBBEntitySoundEventOBBThinkHook
{
    private event OnCSoundEventOBBEntitySoundEventOBBThinkPreDelegate? _Pre;
    private event OnCSoundEventOBBEntitySoundEventOBBThinkPostDelegate? _Post;

    public event OnCSoundEventOBBEntitySoundEventOBBThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundEventOBBEntitySoundEventOBBThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventOBBEntitySoundEventOBBThink);
            }
        }
    }

    public event OnCSoundEventOBBEntitySoundEventOBBThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundEventOBBEntitySoundEventOBBThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventOBBEntitySoundEventOBBThink);
            }
        }
    }

    public void InvokePre(ref CSoundEventOBBEntitySoundEventOBBThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSoundEventOBBEntitySoundEventOBBThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventOBBEntitySoundEventOBBThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventOBBEntitySoundEventOBBThink);
        }
    }

    public void Invoke(CSoundEventOBBEntity schemaObject) => DatamapHooksPublisher.InvokeCSoundEventOBBEntitySoundEventOBBThink(schemaObject.Address);
}