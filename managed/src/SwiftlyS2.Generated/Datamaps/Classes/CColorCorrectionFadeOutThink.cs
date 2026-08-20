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
    private delegate void CColorCorrectionFadeOutThinkDelegate(nint a1);

    private static IUnmanagedFunction<CColorCorrectionFadeOutThinkDelegate>? CColorCorrectionFadeOutThinkUnmanagedFunction;
    private static Guid CColorCorrectionFadeOutThinkHookGuid;

    private static IUnmanagedFunction<CColorCorrectionFadeOutThinkDelegate> CColorCorrectionFadeOutThinkGetUnmanagedFunction()
    {
        if (CColorCorrectionFadeOutThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CColorCorrection", "CColorCorrectionFadeOutThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CColorCorrection::CColorCorrectionFadeOutThink.");
            }
            CColorCorrectionFadeOutThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CColorCorrectionFadeOutThinkDelegate>(address);
        }
        return CColorCorrectionFadeOutThinkUnmanagedFunction;
    }

    internal static Guid HookCColorCorrectionFadeOutThink()
    {
        CColorCorrectionFadeOutThinkHookGuid = CColorCorrectionFadeOutThinkGetUnmanagedFunction().AddHook(next => (a1) => CColorCorrectionFadeOutThinkPipeline(a1, () => next()(a1)));
        return CColorCorrectionFadeOutThinkHookGuid;
    }

    internal static Guid UnhookCColorCorrectionFadeOutThink()
    {
        CColorCorrectionFadeOutThinkGetUnmanagedFunction().RemoveHook(CColorCorrectionFadeOutThinkHookGuid);
        return Guid.Empty;
    }

    private static void CColorCorrectionFadeOutThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CColorCorrection>(a1);

            var preCtx = new CColorCorrectionFadeOutThinkPreContext { SchemaObject = schemaObject };
            InvokeCColorCorrectionFadeOutThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CColorCorrectionFadeOutThinkPostContext { SchemaObject = schemaObject };
            InvokeCColorCorrectionFadeOutThinkPost(ref postCtx);
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

    internal static void InvokeCColorCorrectionFadeOutThink(nint a1)
    {
        CColorCorrectionFadeOutThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCColorCorrectionFadeOutThinkPre(ref CColorCorrectionFadeOutThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCColorCorrectionFadeOutThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCColorCorrectionFadeOutThinkPost(ref CColorCorrectionFadeOutThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCColorCorrectionFadeOutThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CColorCorrectionFadeOutThinkHook : ICColorCorrectionFadeOutThinkHook
{
    private event OnCColorCorrectionFadeOutThinkPreDelegate? _Pre;
    private event OnCColorCorrectionFadeOutThinkPostDelegate? _Post;

    public event OnCColorCorrectionFadeOutThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CColorCorrectionFadeOutThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CColorCorrectionFadeOutThink);
            }
        }
    }

    public event OnCColorCorrectionFadeOutThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CColorCorrectionFadeOutThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CColorCorrectionFadeOutThink);
            }
        }
    }

    public void InvokePre(ref CColorCorrectionFadeOutThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CColorCorrectionFadeOutThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CColorCorrectionFadeOutThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CColorCorrectionFadeOutThink);
        }
    }

    public void Invoke(CColorCorrection schemaObject) => DatamapHooksPublisher.InvokeCColorCorrectionFadeOutThink(schemaObject.Address);
}