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
    private delegate void CSoundEventMultiPointEntityMultiPointThinkDelegate(nint a1);

    private static IUnmanagedFunction<CSoundEventMultiPointEntityMultiPointThinkDelegate>? CSoundEventMultiPointEntityMultiPointThinkUnmanagedFunction;
    private static Guid CSoundEventMultiPointEntityMultiPointThinkHookGuid;

    private static IUnmanagedFunction<CSoundEventMultiPointEntityMultiPointThinkDelegate> CSoundEventMultiPointEntityMultiPointThinkGetUnmanagedFunction()
    {
        if (CSoundEventMultiPointEntityMultiPointThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSoundEventMultiPointEntity", "CSoundEventMultiPointEntityMultiPointThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSoundEventMultiPointEntity::CSoundEventMultiPointEntityMultiPointThink.");
            }
            CSoundEventMultiPointEntityMultiPointThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSoundEventMultiPointEntityMultiPointThinkDelegate>(address);
        }
        return CSoundEventMultiPointEntityMultiPointThinkUnmanagedFunction;
    }

    internal static Guid HookCSoundEventMultiPointEntityMultiPointThink()
    {
        CSoundEventMultiPointEntityMultiPointThinkHookGuid = CSoundEventMultiPointEntityMultiPointThinkGetUnmanagedFunction().AddHook(next => (a1) => CSoundEventMultiPointEntityMultiPointThinkPipeline(a1, () => next()(a1)));
        return CSoundEventMultiPointEntityMultiPointThinkHookGuid;
    }

    internal static Guid UnhookCSoundEventMultiPointEntityMultiPointThink()
    {
        CSoundEventMultiPointEntityMultiPointThinkGetUnmanagedFunction().RemoveHook(CSoundEventMultiPointEntityMultiPointThinkHookGuid);
        return Guid.Empty;
    }

    private static void CSoundEventMultiPointEntityMultiPointThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSoundEventMultiPointEntity>(a1);

            var preCtx = new CSoundEventMultiPointEntityMultiPointThinkPreContext { SchemaObject = schemaObject };
            InvokeCSoundEventMultiPointEntityMultiPointThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSoundEventMultiPointEntityMultiPointThinkPostContext { SchemaObject = schemaObject };
            InvokeCSoundEventMultiPointEntityMultiPointThinkPost(ref postCtx);
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

    internal static void InvokeCSoundEventMultiPointEntityMultiPointThink(nint a1)
    {
        CSoundEventMultiPointEntityMultiPointThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSoundEventMultiPointEntityMultiPointThinkPre(ref CSoundEventMultiPointEntityMultiPointThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundEventMultiPointEntityMultiPointThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSoundEventMultiPointEntityMultiPointThinkPost(ref CSoundEventMultiPointEntityMultiPointThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundEventMultiPointEntityMultiPointThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSoundEventMultiPointEntityMultiPointThinkHook : ICSoundEventMultiPointEntityMultiPointThinkHook
{
    private event OnCSoundEventMultiPointEntityMultiPointThinkPreDelegate? _Pre;
    private event OnCSoundEventMultiPointEntityMultiPointThinkPostDelegate? _Post;

    public event OnCSoundEventMultiPointEntityMultiPointThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundEventMultiPointEntityMultiPointThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventMultiPointEntityMultiPointThink);
            }
        }
    }

    public event OnCSoundEventMultiPointEntityMultiPointThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundEventMultiPointEntityMultiPointThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventMultiPointEntityMultiPointThink);
            }
        }
    }

    public void InvokePre(ref CSoundEventMultiPointEntityMultiPointThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSoundEventMultiPointEntityMultiPointThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventMultiPointEntityMultiPointThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundEventMultiPointEntityMultiPointThink);
        }
    }

    public void Invoke(CSoundEventMultiPointEntity schemaObject) => DatamapHooksPublisher.InvokeCSoundEventMultiPointEntityMultiPointThink(schemaObject.Address);
}