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
    private delegate void CSoundEventPathCornerEntitySoundEventPathCornerThinkDelegate(nint a1);

    private static IUnmanagedFunction<CSoundEventPathCornerEntitySoundEventPathCornerThinkDelegate>? CSoundEventPathCornerEntitySoundEventPathCornerThinkUnmanagedFunction;
    private static Guid CSoundEventPathCornerEntitySoundEventPathCornerThinkHookGuid;

    private static IUnmanagedFunction<CSoundEventPathCornerEntitySoundEventPathCornerThinkDelegate> CSoundEventPathCornerEntitySoundEventPathCornerThinkGetUnmanagedFunction()
    {
        if (CSoundEventPathCornerEntitySoundEventPathCornerThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSoundEventPathCornerEntity", "CSoundEventPathCornerEntitySoundEventPathCornerThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSoundEventPathCornerEntity::CSoundEventPathCornerEntitySoundEventPathCornerThink.");
            }
            CSoundEventPathCornerEntitySoundEventPathCornerThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSoundEventPathCornerEntitySoundEventPathCornerThinkDelegate>(address);
        }
        return CSoundEventPathCornerEntitySoundEventPathCornerThinkUnmanagedFunction;
    }

    internal static Guid HookCSoundEventPathCornerEntitySoundEventPathCornerThink()
    {
        CSoundEventPathCornerEntitySoundEventPathCornerThinkHookGuid = CSoundEventPathCornerEntitySoundEventPathCornerThinkGetUnmanagedFunction().AddHook(next => (a1) => CSoundEventPathCornerEntitySoundEventPathCornerThinkPipeline(a1, () => next()(a1)));
        return CSoundEventPathCornerEntitySoundEventPathCornerThinkHookGuid;
    }

    internal static Guid UnhookCSoundEventPathCornerEntitySoundEventPathCornerThink()
    {
        CSoundEventPathCornerEntitySoundEventPathCornerThinkGetUnmanagedFunction().RemoveHook(CSoundEventPathCornerEntitySoundEventPathCornerThinkHookGuid);
        return Guid.Empty;
    }

    private static void CSoundEventPathCornerEntitySoundEventPathCornerThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSoundEventPathCornerEntity>(a1);

            var preCtx = new CSoundEventPathCornerEntitySoundEventPathCornerThinkPreContext { SchemaObject = schemaObject };
            InvokeCSoundEventPathCornerEntitySoundEventPathCornerThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSoundEventPathCornerEntitySoundEventPathCornerThinkPostContext { SchemaObject = schemaObject };
            InvokeCSoundEventPathCornerEntitySoundEventPathCornerThinkPost(ref postCtx);
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

    internal static void InvokeCSoundEventPathCornerEntitySoundEventPathCornerThink(nint a1)
    {
        CSoundEventPathCornerEntitySoundEventPathCornerThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSoundEventPathCornerEntitySoundEventPathCornerThinkPre(ref CSoundEventPathCornerEntitySoundEventPathCornerThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundEventPathCornerEntitySoundEventPathCornerThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSoundEventPathCornerEntitySoundEventPathCornerThinkPost(ref CSoundEventPathCornerEntitySoundEventPathCornerThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundEventPathCornerEntitySoundEventPathCornerThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSoundEventPathCornerEntitySoundEventPathCornerThinkHook : ICSoundEventPathCornerEntitySoundEventPathCornerThinkHook
{
    private event OnCSoundEventPathCornerEntitySoundEventPathCornerThinkPreDelegate? _Pre;
    private event OnCSoundEventPathCornerEntitySoundEventPathCornerThinkPostDelegate? _Post;

    public event OnCSoundEventPathCornerEntitySoundEventPathCornerThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundEventPathCornerEntitySoundEventPathCornerThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventPathCornerEntitySoundEventPathCornerThink);
            }
        }
    }

    public event OnCSoundEventPathCornerEntitySoundEventPathCornerThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundEventPathCornerEntitySoundEventPathCornerThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventPathCornerEntitySoundEventPathCornerThink);
            }
        }
    }

    public void InvokePre(ref CSoundEventPathCornerEntitySoundEventPathCornerThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSoundEventPathCornerEntitySoundEventPathCornerThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventPathCornerEntitySoundEventPathCornerThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventPathCornerEntitySoundEventPathCornerThink);
        }
    }

    public void Invoke(CSoundEventPathCornerEntity schemaObject) => DatamapHooksPublisher.InvokeCSoundEventPathCornerEntitySoundEventPathCornerThink(schemaObject.Address);
}