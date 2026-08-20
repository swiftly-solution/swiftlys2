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
    private delegate void CColorCorrectionFadeInThinkDelegate(nint a1);

    private static IUnmanagedFunction<CColorCorrectionFadeInThinkDelegate>? CColorCorrectionFadeInThinkUnmanagedFunction;
    private static Guid CColorCorrectionFadeInThinkHookGuid;

    private static IUnmanagedFunction<CColorCorrectionFadeInThinkDelegate> CColorCorrectionFadeInThinkGetUnmanagedFunction()
    {
        if (CColorCorrectionFadeInThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CColorCorrection", "CColorCorrectionFadeInThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CColorCorrection::CColorCorrectionFadeInThink.");
            }
            CColorCorrectionFadeInThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CColorCorrectionFadeInThinkDelegate>(address);
        }
        return CColorCorrectionFadeInThinkUnmanagedFunction;
    }

    internal static Guid HookCColorCorrectionFadeInThink()
    {
        CColorCorrectionFadeInThinkHookGuid = CColorCorrectionFadeInThinkGetUnmanagedFunction().AddHook(next => (a1) => CColorCorrectionFadeInThinkPipeline(a1, () => next()(a1)));
        return CColorCorrectionFadeInThinkHookGuid;
    }

    internal static Guid UnhookCColorCorrectionFadeInThink()
    {
        CColorCorrectionFadeInThinkGetUnmanagedFunction().RemoveHook(CColorCorrectionFadeInThinkHookGuid);
        return Guid.Empty;
    }

    private static void CColorCorrectionFadeInThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CColorCorrection>(a1);

            var preCtx = new CColorCorrectionFadeInThinkPreContext { SchemaObject = schemaObject };
            InvokeCColorCorrectionFadeInThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CColorCorrectionFadeInThinkPostContext { SchemaObject = schemaObject };
            InvokeCColorCorrectionFadeInThinkPost(ref postCtx);
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

    internal static void InvokeCColorCorrectionFadeInThink(nint a1)
    {
        CColorCorrectionFadeInThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCColorCorrectionFadeInThinkPre(ref CColorCorrectionFadeInThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCColorCorrectionFadeInThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCColorCorrectionFadeInThinkPost(ref CColorCorrectionFadeInThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCColorCorrectionFadeInThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CColorCorrectionFadeInThinkHook : ICColorCorrectionFadeInThinkHook
{
    private event OnCColorCorrectionFadeInThinkPreDelegate? _Pre;
    private event OnCColorCorrectionFadeInThinkPostDelegate? _Post;

    public event OnCColorCorrectionFadeInThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CColorCorrectionFadeInThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CColorCorrectionFadeInThink);
            }
        }
    }

    public event OnCColorCorrectionFadeInThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CColorCorrectionFadeInThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CColorCorrectionFadeInThink);
            }
        }
    }

    public void InvokePre(ref CColorCorrectionFadeInThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CColorCorrectionFadeInThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CColorCorrectionFadeInThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CColorCorrectionFadeInThink);
        }
    }

    public void Invoke(CColorCorrection schemaObject) => DatamapHooksPublisher.InvokeCColorCorrectionFadeInThink(schemaObject.Address);
}