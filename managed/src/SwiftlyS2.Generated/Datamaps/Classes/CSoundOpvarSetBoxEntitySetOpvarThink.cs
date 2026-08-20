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
    private delegate void CSoundOpvarSetBoxEntitySetOpvarThinkDelegate(nint a1);

    private static IUnmanagedFunction<CSoundOpvarSetBoxEntitySetOpvarThinkDelegate>? CSoundOpvarSetBoxEntitySetOpvarThinkUnmanagedFunction;
    private static Guid CSoundOpvarSetBoxEntitySetOpvarThinkHookGuid;

    private static IUnmanagedFunction<CSoundOpvarSetBoxEntitySetOpvarThinkDelegate> CSoundOpvarSetBoxEntitySetOpvarThinkGetUnmanagedFunction()
    {
        if (CSoundOpvarSetBoxEntitySetOpvarThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSoundOpvarSetBoxEntity", "CSoundOpvarSetBoxEntitySetOpvarThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSoundOpvarSetBoxEntity::CSoundOpvarSetBoxEntitySetOpvarThink.");
            }
            CSoundOpvarSetBoxEntitySetOpvarThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSoundOpvarSetBoxEntitySetOpvarThinkDelegate>(address);
        }
        return CSoundOpvarSetBoxEntitySetOpvarThinkUnmanagedFunction;
    }

    internal static Guid HookCSoundOpvarSetBoxEntitySetOpvarThink()
    {
        CSoundOpvarSetBoxEntitySetOpvarThinkHookGuid = CSoundOpvarSetBoxEntitySetOpvarThinkGetUnmanagedFunction().AddHook(next => (a1) => CSoundOpvarSetBoxEntitySetOpvarThinkPipeline(a1, () => next()(a1)));
        return CSoundOpvarSetBoxEntitySetOpvarThinkHookGuid;
    }

    internal static Guid UnhookCSoundOpvarSetBoxEntitySetOpvarThink()
    {
        CSoundOpvarSetBoxEntitySetOpvarThinkGetUnmanagedFunction().RemoveHook(CSoundOpvarSetBoxEntitySetOpvarThinkHookGuid);
        return Guid.Empty;
    }

    private static void CSoundOpvarSetBoxEntitySetOpvarThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSoundOpvarSetBoxEntity>(a1);

            var preCtx = new CSoundOpvarSetBoxEntitySetOpvarThinkPreContext { SchemaObject = schemaObject };
            InvokeCSoundOpvarSetBoxEntitySetOpvarThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSoundOpvarSetBoxEntitySetOpvarThinkPostContext { SchemaObject = schemaObject };
            InvokeCSoundOpvarSetBoxEntitySetOpvarThinkPost(ref postCtx);
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

    internal static void InvokeCSoundOpvarSetBoxEntitySetOpvarThink(nint a1)
    {
        CSoundOpvarSetBoxEntitySetOpvarThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSoundOpvarSetBoxEntitySetOpvarThinkPre(ref CSoundOpvarSetBoxEntitySetOpvarThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundOpvarSetBoxEntitySetOpvarThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSoundOpvarSetBoxEntitySetOpvarThinkPost(ref CSoundOpvarSetBoxEntitySetOpvarThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundOpvarSetBoxEntitySetOpvarThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSoundOpvarSetBoxEntitySetOpvarThinkHook : ICSoundOpvarSetBoxEntitySetOpvarThinkHook
{
    private event OnCSoundOpvarSetBoxEntitySetOpvarThinkPreDelegate? _Pre;
    private event OnCSoundOpvarSetBoxEntitySetOpvarThinkPostDelegate? _Post;

    public event OnCSoundOpvarSetBoxEntitySetOpvarThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundOpvarSetBoxEntitySetOpvarThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetBoxEntitySetOpvarThink);
            }
        }
    }

    public event OnCSoundOpvarSetBoxEntitySetOpvarThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundOpvarSetBoxEntitySetOpvarThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetBoxEntitySetOpvarThink);
            }
        }
    }

    public void InvokePre(ref CSoundOpvarSetBoxEntitySetOpvarThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSoundOpvarSetBoxEntitySetOpvarThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetBoxEntitySetOpvarThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetBoxEntitySetOpvarThink);
        }
    }

    public void Invoke(CSoundOpvarSetBoxEntity schemaObject) => DatamapHooksPublisher.InvokeCSoundOpvarSetBoxEntitySetOpvarThink(schemaObject.Address);
}