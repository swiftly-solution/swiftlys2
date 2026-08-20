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
    private delegate void CSoundOpvarSetPointBaseSetOpvarThinkDelegate(nint a1);

    private static IUnmanagedFunction<CSoundOpvarSetPointBaseSetOpvarThinkDelegate>? CSoundOpvarSetPointBaseSetOpvarThinkUnmanagedFunction;
    private static Guid CSoundOpvarSetPointBaseSetOpvarThinkHookGuid;

    private static IUnmanagedFunction<CSoundOpvarSetPointBaseSetOpvarThinkDelegate> CSoundOpvarSetPointBaseSetOpvarThinkGetUnmanagedFunction()
    {
        if (CSoundOpvarSetPointBaseSetOpvarThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSoundOpvarSetPointBase", "CSoundOpvarSetPointBaseSetOpvarThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSoundOpvarSetPointBase::CSoundOpvarSetPointBaseSetOpvarThink.");
            }
            CSoundOpvarSetPointBaseSetOpvarThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSoundOpvarSetPointBaseSetOpvarThinkDelegate>(address);
        }
        return CSoundOpvarSetPointBaseSetOpvarThinkUnmanagedFunction;
    }

    internal static Guid HookCSoundOpvarSetPointBaseSetOpvarThink()
    {
        CSoundOpvarSetPointBaseSetOpvarThinkHookGuid = CSoundOpvarSetPointBaseSetOpvarThinkGetUnmanagedFunction().AddHook(next => (a1) => CSoundOpvarSetPointBaseSetOpvarThinkPipeline(a1, () => next()(a1)));
        return CSoundOpvarSetPointBaseSetOpvarThinkHookGuid;
    }

    internal static Guid UnhookCSoundOpvarSetPointBaseSetOpvarThink()
    {
        CSoundOpvarSetPointBaseSetOpvarThinkGetUnmanagedFunction().RemoveHook(CSoundOpvarSetPointBaseSetOpvarThinkHookGuid);
        return Guid.Empty;
    }

    private static void CSoundOpvarSetPointBaseSetOpvarThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSoundOpvarSetPointBase>(a1);

            var preCtx = new CSoundOpvarSetPointBaseSetOpvarThinkPreContext { SchemaObject = schemaObject };
            InvokeCSoundOpvarSetPointBaseSetOpvarThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSoundOpvarSetPointBaseSetOpvarThinkPostContext { SchemaObject = schemaObject };
            InvokeCSoundOpvarSetPointBaseSetOpvarThinkPost(ref postCtx);
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

    internal static void InvokeCSoundOpvarSetPointBaseSetOpvarThink(nint a1)
    {
        CSoundOpvarSetPointBaseSetOpvarThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSoundOpvarSetPointBaseSetOpvarThinkPre(ref CSoundOpvarSetPointBaseSetOpvarThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundOpvarSetPointBaseSetOpvarThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSoundOpvarSetPointBaseSetOpvarThinkPost(ref CSoundOpvarSetPointBaseSetOpvarThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundOpvarSetPointBaseSetOpvarThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSoundOpvarSetPointBaseSetOpvarThinkHook : ICSoundOpvarSetPointBaseSetOpvarThinkHook
{
    private event OnCSoundOpvarSetPointBaseSetOpvarThinkPreDelegate? _Pre;
    private event OnCSoundOpvarSetPointBaseSetOpvarThinkPostDelegate? _Post;

    public event OnCSoundOpvarSetPointBaseSetOpvarThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundOpvarSetPointBaseSetOpvarThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetPointBaseSetOpvarThink);
            }
        }
    }

    public event OnCSoundOpvarSetPointBaseSetOpvarThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundOpvarSetPointBaseSetOpvarThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetPointBaseSetOpvarThink);
            }
        }
    }

    public void InvokePre(ref CSoundOpvarSetPointBaseSetOpvarThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSoundOpvarSetPointBaseSetOpvarThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetPointBaseSetOpvarThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetPointBaseSetOpvarThink);
        }
    }

    public void Invoke(CSoundOpvarSetPointBase schemaObject) => DatamapHooksPublisher.InvokeCSoundOpvarSetPointBaseSetOpvarThink(schemaObject.Address);
}