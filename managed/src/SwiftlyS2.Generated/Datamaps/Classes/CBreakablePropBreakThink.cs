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
    private delegate void CBreakablePropBreakThinkDelegate(nint a1);

    private static IUnmanagedFunction<CBreakablePropBreakThinkDelegate>? CBreakablePropBreakThinkUnmanagedFunction;
    private static Guid CBreakablePropBreakThinkHookGuid;

    private static IUnmanagedFunction<CBreakablePropBreakThinkDelegate> CBreakablePropBreakThinkGetUnmanagedFunction()
    {
        if (CBreakablePropBreakThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBreakableProp", "CBreakablePropBreakThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBreakableProp::CBreakablePropBreakThink.");
            }
            CBreakablePropBreakThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBreakablePropBreakThinkDelegate>(address);
        }
        return CBreakablePropBreakThinkUnmanagedFunction;
    }

    internal static Guid HookCBreakablePropBreakThink()
    {
        CBreakablePropBreakThinkHookGuid = CBreakablePropBreakThinkGetUnmanagedFunction().AddHook(next => (a1) => CBreakablePropBreakThinkPipeline(a1, () => next()(a1)));
        return CBreakablePropBreakThinkHookGuid;
    }

    internal static Guid UnhookCBreakablePropBreakThink()
    {
        CBreakablePropBreakThinkGetUnmanagedFunction().RemoveHook(CBreakablePropBreakThinkHookGuid);
        return Guid.Empty;
    }

    private static void CBreakablePropBreakThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBreakableProp>(a1);

            var preCtx = new CBreakablePropBreakThinkPreContext { SchemaObject = schemaObject };
            InvokeCBreakablePropBreakThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBreakablePropBreakThinkPostContext { SchemaObject = schemaObject };
            InvokeCBreakablePropBreakThinkPost(ref postCtx);
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

    internal static void InvokeCBreakablePropBreakThink(nint a1)
    {
        CBreakablePropBreakThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBreakablePropBreakThinkPre(ref CBreakablePropBreakThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBreakablePropBreakThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBreakablePropBreakThinkPost(ref CBreakablePropBreakThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBreakablePropBreakThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBreakablePropBreakThinkHook : ICBreakablePropBreakThinkHook
{
    private event OnCBreakablePropBreakThinkPreDelegate? _Pre;
    private event OnCBreakablePropBreakThinkPostDelegate? _Post;

    public event OnCBreakablePropBreakThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBreakablePropBreakThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBreakablePropBreakThink);
            }
        }
    }

    public event OnCBreakablePropBreakThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBreakablePropBreakThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBreakablePropBreakThink);
            }
        }
    }

    public void InvokePre(ref CBreakablePropBreakThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBreakablePropBreakThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBreakablePropBreakThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBreakablePropBreakThink);
        }
    }

    public void Invoke(CBreakableProp schemaObject) => DatamapHooksPublisher.InvokeCBreakablePropBreakThink(schemaObject.Address);
}