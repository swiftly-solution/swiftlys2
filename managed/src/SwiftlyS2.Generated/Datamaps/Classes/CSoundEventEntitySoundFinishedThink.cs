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
    private delegate void CSoundEventEntitySoundFinishedThinkDelegate(nint a1);

    private static IUnmanagedFunction<CSoundEventEntitySoundFinishedThinkDelegate>? CSoundEventEntitySoundFinishedThinkUnmanagedFunction;
    private static Guid CSoundEventEntitySoundFinishedThinkHookGuid;

    private static IUnmanagedFunction<CSoundEventEntitySoundFinishedThinkDelegate> CSoundEventEntitySoundFinishedThinkGetUnmanagedFunction()
    {
        if (CSoundEventEntitySoundFinishedThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSoundEventEntity", "CSoundEventEntitySoundFinishedThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSoundEventEntity::CSoundEventEntitySoundFinishedThink.");
            }
            CSoundEventEntitySoundFinishedThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSoundEventEntitySoundFinishedThinkDelegate>(address);
        }
        return CSoundEventEntitySoundFinishedThinkUnmanagedFunction;
    }

    internal static Guid HookCSoundEventEntitySoundFinishedThink()
    {
        CSoundEventEntitySoundFinishedThinkHookGuid = CSoundEventEntitySoundFinishedThinkGetUnmanagedFunction().AddHook(next => (a1) => CSoundEventEntitySoundFinishedThinkPipeline(a1, () => next()(a1)));
        return CSoundEventEntitySoundFinishedThinkHookGuid;
    }

    internal static Guid UnhookCSoundEventEntitySoundFinishedThink()
    {
        CSoundEventEntitySoundFinishedThinkGetUnmanagedFunction().RemoveHook(CSoundEventEntitySoundFinishedThinkHookGuid);
        return Guid.Empty;
    }

    private static void CSoundEventEntitySoundFinishedThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSoundEventEntity>(a1);

            var preCtx = new CSoundEventEntitySoundFinishedThinkPreContext { SchemaObject = schemaObject };
            InvokeCSoundEventEntitySoundFinishedThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSoundEventEntitySoundFinishedThinkPostContext { SchemaObject = schemaObject };
            InvokeCSoundEventEntitySoundFinishedThinkPost(ref postCtx);
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

    internal static void InvokeCSoundEventEntitySoundFinishedThink(nint a1)
    {
        CSoundEventEntitySoundFinishedThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSoundEventEntitySoundFinishedThinkPre(ref CSoundEventEntitySoundFinishedThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundEventEntitySoundFinishedThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSoundEventEntitySoundFinishedThinkPost(ref CSoundEventEntitySoundFinishedThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundEventEntitySoundFinishedThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSoundEventEntitySoundFinishedThinkHook : ICSoundEventEntitySoundFinishedThinkHook
{
    private event OnCSoundEventEntitySoundFinishedThinkPreDelegate? _Pre;
    private event OnCSoundEventEntitySoundFinishedThinkPostDelegate? _Post;

    public event OnCSoundEventEntitySoundFinishedThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundEventEntitySoundFinishedThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventEntitySoundFinishedThink);
            }
        }
    }

    public event OnCSoundEventEntitySoundFinishedThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundEventEntitySoundFinishedThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventEntitySoundFinishedThink);
            }
        }
    }

    public void InvokePre(ref CSoundEventEntitySoundFinishedThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSoundEventEntitySoundFinishedThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventEntitySoundFinishedThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventEntitySoundFinishedThink);
        }
    }

    public void Invoke(CSoundEventEntity schemaObject) => DatamapHooksPublisher.InvokeCSoundEventEntitySoundFinishedThink(schemaObject.Address);
}