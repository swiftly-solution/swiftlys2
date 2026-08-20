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
    private delegate void CSoundOpvarSetPathCornerEntitySetOpvarThinkDelegate(nint a1);

    private static IUnmanagedFunction<CSoundOpvarSetPathCornerEntitySetOpvarThinkDelegate>? CSoundOpvarSetPathCornerEntitySetOpvarThinkUnmanagedFunction;
    private static Guid CSoundOpvarSetPathCornerEntitySetOpvarThinkHookGuid;

    private static IUnmanagedFunction<CSoundOpvarSetPathCornerEntitySetOpvarThinkDelegate> CSoundOpvarSetPathCornerEntitySetOpvarThinkGetUnmanagedFunction()
    {
        if (CSoundOpvarSetPathCornerEntitySetOpvarThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CSoundOpvarSetPathCornerEntity", "CSoundOpvarSetPathCornerEntitySetOpvarThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CSoundOpvarSetPathCornerEntity::CSoundOpvarSetPathCornerEntitySetOpvarThink.");
            }
            CSoundOpvarSetPathCornerEntitySetOpvarThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CSoundOpvarSetPathCornerEntitySetOpvarThinkDelegate>(address);
        }
        return CSoundOpvarSetPathCornerEntitySetOpvarThinkUnmanagedFunction;
    }

    internal static Guid HookCSoundOpvarSetPathCornerEntitySetOpvarThink()
    {
        CSoundOpvarSetPathCornerEntitySetOpvarThinkHookGuid = CSoundOpvarSetPathCornerEntitySetOpvarThinkGetUnmanagedFunction().AddHook(next => (a1) => CSoundOpvarSetPathCornerEntitySetOpvarThinkPipeline(a1, () => next()(a1)));
        return CSoundOpvarSetPathCornerEntitySetOpvarThinkHookGuid;
    }

    internal static Guid UnhookCSoundOpvarSetPathCornerEntitySetOpvarThink()
    {
        CSoundOpvarSetPathCornerEntitySetOpvarThinkGetUnmanagedFunction().RemoveHook(CSoundOpvarSetPathCornerEntitySetOpvarThinkHookGuid);
        return Guid.Empty;
    }

    private static void CSoundOpvarSetPathCornerEntitySetOpvarThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CSoundOpvarSetPathCornerEntity>(a1);

            var preCtx = new CSoundOpvarSetPathCornerEntitySetOpvarThinkPreContext { SchemaObject = schemaObject };
            InvokeCSoundOpvarSetPathCornerEntitySetOpvarThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CSoundOpvarSetPathCornerEntitySetOpvarThinkPostContext { SchemaObject = schemaObject };
            InvokeCSoundOpvarSetPathCornerEntitySetOpvarThinkPost(ref postCtx);
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

    internal static void InvokeCSoundOpvarSetPathCornerEntitySetOpvarThink(nint a1)
    {
        CSoundOpvarSetPathCornerEntitySetOpvarThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCSoundOpvarSetPathCornerEntitySetOpvarThinkPre(ref CSoundOpvarSetPathCornerEntitySetOpvarThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundOpvarSetPathCornerEntitySetOpvarThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCSoundOpvarSetPathCornerEntitySetOpvarThinkPost(ref CSoundOpvarSetPathCornerEntitySetOpvarThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCSoundOpvarSetPathCornerEntitySetOpvarThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CSoundOpvarSetPathCornerEntitySetOpvarThinkHook : ICSoundOpvarSetPathCornerEntitySetOpvarThinkHook
{
    private event OnCSoundOpvarSetPathCornerEntitySetOpvarThinkPreDelegate? _Pre;
    private event OnCSoundOpvarSetPathCornerEntitySetOpvarThinkPostDelegate? _Post;

    public event OnCSoundOpvarSetPathCornerEntitySetOpvarThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundOpvarSetPathCornerEntitySetOpvarThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetPathCornerEntitySetOpvarThink);
            }
        }
    }

    public event OnCSoundOpvarSetPathCornerEntitySetOpvarThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CSoundOpvarSetPathCornerEntitySetOpvarThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetPathCornerEntitySetOpvarThink);
            }
        }
    }

    public void InvokePre(ref CSoundOpvarSetPathCornerEntitySetOpvarThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CSoundOpvarSetPathCornerEntitySetOpvarThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetPathCornerEntitySetOpvarThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CSoundOpvarSetPathCornerEntitySetOpvarThink);
        }
    }

    public void Invoke(CSoundOpvarSetPathCornerEntity schemaObject) => DatamapHooksPublisher.InvokeCSoundOpvarSetPathCornerEntitySetOpvarThink(schemaObject.Address);
}