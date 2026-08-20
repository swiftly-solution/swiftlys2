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
    private delegate void CBreakablePropRampToDefaultFadeScaleDelegate(nint a1);

    private static IUnmanagedFunction<CBreakablePropRampToDefaultFadeScaleDelegate>? CBreakablePropRampToDefaultFadeScaleUnmanagedFunction;
    private static Guid CBreakablePropRampToDefaultFadeScaleHookGuid;

    private static IUnmanagedFunction<CBreakablePropRampToDefaultFadeScaleDelegate> CBreakablePropRampToDefaultFadeScaleGetUnmanagedFunction()
    {
        if (CBreakablePropRampToDefaultFadeScaleUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBreakableProp", "CBreakablePropRampToDefaultFadeScale");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBreakableProp::CBreakablePropRampToDefaultFadeScale.");
            }
            CBreakablePropRampToDefaultFadeScaleUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBreakablePropRampToDefaultFadeScaleDelegate>(address);
        }
        return CBreakablePropRampToDefaultFadeScaleUnmanagedFunction;
    }

    internal static Guid HookCBreakablePropRampToDefaultFadeScale()
    {
        CBreakablePropRampToDefaultFadeScaleHookGuid = CBreakablePropRampToDefaultFadeScaleGetUnmanagedFunction().AddHook(next => (a1) => CBreakablePropRampToDefaultFadeScalePipeline(a1, () => next()(a1)));
        return CBreakablePropRampToDefaultFadeScaleHookGuid;
    }

    internal static Guid UnhookCBreakablePropRampToDefaultFadeScale()
    {
        CBreakablePropRampToDefaultFadeScaleGetUnmanagedFunction().RemoveHook(CBreakablePropRampToDefaultFadeScaleHookGuid);
        return Guid.Empty;
    }

    private static void CBreakablePropRampToDefaultFadeScalePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBreakableProp>(a1);

            var preCtx = new CBreakablePropRampToDefaultFadeScalePreContext { SchemaObject = schemaObject };
            InvokeCBreakablePropRampToDefaultFadeScalePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBreakablePropRampToDefaultFadeScalePostContext { SchemaObject = schemaObject };
            InvokeCBreakablePropRampToDefaultFadeScalePost(ref postCtx);
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

    internal static void InvokeCBreakablePropRampToDefaultFadeScale(nint a1)
    {
        CBreakablePropRampToDefaultFadeScaleGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBreakablePropRampToDefaultFadeScalePre(ref CBreakablePropRampToDefaultFadeScalePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBreakablePropRampToDefaultFadeScalePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBreakablePropRampToDefaultFadeScalePost(ref CBreakablePropRampToDefaultFadeScalePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBreakablePropRampToDefaultFadeScalePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBreakablePropRampToDefaultFadeScaleHook : ICBreakablePropRampToDefaultFadeScaleHook
{
    private event OnCBreakablePropRampToDefaultFadeScalePreDelegate? _Pre;
    private event OnCBreakablePropRampToDefaultFadeScalePostDelegate? _Post;

    public event OnCBreakablePropRampToDefaultFadeScalePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBreakablePropRampToDefaultFadeScale);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBreakablePropRampToDefaultFadeScale);
            }
        }
    }

    public event OnCBreakablePropRampToDefaultFadeScalePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBreakablePropRampToDefaultFadeScale);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBreakablePropRampToDefaultFadeScale);
            }
        }
    }

    public void InvokePre(ref CBreakablePropRampToDefaultFadeScalePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBreakablePropRampToDefaultFadeScalePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBreakablePropRampToDefaultFadeScale);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBreakablePropRampToDefaultFadeScale);
        }
    }

    public void Invoke(CBreakableProp schemaObject) => DatamapHooksPublisher.InvokeCBreakablePropRampToDefaultFadeScale(schemaObject.Address);
}