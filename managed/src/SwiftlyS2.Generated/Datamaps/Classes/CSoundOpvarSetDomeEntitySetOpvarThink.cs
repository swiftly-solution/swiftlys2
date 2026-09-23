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
    private delegate void CSoundOpvarSetDomeEntitySetOpvarThinkDelegate(nint a1);

    private static IUnmanagedFunction<CSoundOpvarSetDomeEntitySetOpvarThinkDelegate>? CSoundOpvarSetDomeEntitySetOpvarThinkUnmanagedFunction;
    private static Guid CSoundOpvarSetDomeEntitySetOpvarThinkHookGuid;

    private static IUnmanagedFunction<CSoundOpvarSetDomeEntitySetOpvarThinkDelegate> CSoundOpvarSetDomeEntitySetOpvarThinkGetUnmanagedFunction()
    {
        if (CSoundOpvarSetDomeEntitySetOpvarThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSoundOpvarSetDomeEntity", "CSoundOpvarSetDomeEntitySetOpvarThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSoundOpvarSetDomeEntity::CSoundOpvarSetDomeEntitySetOpvarThink.");
            }
            CSoundOpvarSetDomeEntitySetOpvarThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSoundOpvarSetDomeEntitySetOpvarThinkDelegate>(address);
        }
        return CSoundOpvarSetDomeEntitySetOpvarThinkUnmanagedFunction;
    }

    internal static Guid HookCSoundOpvarSetDomeEntitySetOpvarThink()
    {
        CSoundOpvarSetDomeEntitySetOpvarThinkHookGuid = CSoundOpvarSetDomeEntitySetOpvarThinkGetUnmanagedFunction().AddHook(next => (a1) => CSoundOpvarSetDomeEntitySetOpvarThinkPipeline(a1, () => next()(a1)));
        return CSoundOpvarSetDomeEntitySetOpvarThinkHookGuid;
    }

    internal static Guid UnhookCSoundOpvarSetDomeEntitySetOpvarThink()
    {
        CSoundOpvarSetDomeEntitySetOpvarThinkGetUnmanagedFunction().RemoveHook(CSoundOpvarSetDomeEntitySetOpvarThinkHookGuid);
        return Guid.Empty;
    }

    private static void CSoundOpvarSetDomeEntitySetOpvarThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSoundOpvarSetDomeEntity>(a1);

            var preCtx = new CSoundOpvarSetDomeEntitySetOpvarThinkPreContext { SchemaObject = schemaObject };
            InvokeCSoundOpvarSetDomeEntitySetOpvarThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSoundOpvarSetDomeEntitySetOpvarThinkPostContext { SchemaObject = schemaObject };
            InvokeCSoundOpvarSetDomeEntitySetOpvarThinkPost(ref postCtx);
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

    internal static void InvokeCSoundOpvarSetDomeEntitySetOpvarThink(nint a1)
    {
        CSoundOpvarSetDomeEntitySetOpvarThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSoundOpvarSetDomeEntitySetOpvarThinkPre(ref CSoundOpvarSetDomeEntitySetOpvarThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundOpvarSetDomeEntitySetOpvarThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSoundOpvarSetDomeEntitySetOpvarThinkPost(ref CSoundOpvarSetDomeEntitySetOpvarThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundOpvarSetDomeEntitySetOpvarThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSoundOpvarSetDomeEntitySetOpvarThinkHook : ICSoundOpvarSetDomeEntitySetOpvarThinkHook
{
    private event OnCSoundOpvarSetDomeEntitySetOpvarThinkPreDelegate? _Pre;
    private event OnCSoundOpvarSetDomeEntitySetOpvarThinkPostDelegate? _Post;

    public event OnCSoundOpvarSetDomeEntitySetOpvarThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundOpvarSetDomeEntitySetOpvarThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetDomeEntitySetOpvarThink);
            }
        }
    }

    public event OnCSoundOpvarSetDomeEntitySetOpvarThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundOpvarSetDomeEntitySetOpvarThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetDomeEntitySetOpvarThink);
            }
        }
    }

    public void InvokePre(ref CSoundOpvarSetDomeEntitySetOpvarThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSoundOpvarSetDomeEntitySetOpvarThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetDomeEntitySetOpvarThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetDomeEntitySetOpvarThink);
        }
    }

    public void Invoke(CSoundOpvarSetDomeEntity schemaObject) => DatamapHooksPublisher.InvokeCSoundOpvarSetDomeEntitySetOpvarThink(schemaObject.Address);
}