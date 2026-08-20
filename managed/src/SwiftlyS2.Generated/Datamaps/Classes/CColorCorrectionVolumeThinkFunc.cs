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
    private delegate void CColorCorrectionVolumeThinkFuncDelegate(nint a1);

    private static IUnmanagedFunction<CColorCorrectionVolumeThinkFuncDelegate>? CColorCorrectionVolumeThinkFuncUnmanagedFunction;
    private static Guid CColorCorrectionVolumeThinkFuncHookGuid;

    private static IUnmanagedFunction<CColorCorrectionVolumeThinkFuncDelegate> CColorCorrectionVolumeThinkFuncGetUnmanagedFunction()
    {
        if (CColorCorrectionVolumeThinkFuncUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CColorCorrectionVolume", "CColorCorrectionVolumeThinkFunc");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CColorCorrectionVolume::CColorCorrectionVolumeThinkFunc.");
            }
            CColorCorrectionVolumeThinkFuncUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CColorCorrectionVolumeThinkFuncDelegate>(address);
        }
        return CColorCorrectionVolumeThinkFuncUnmanagedFunction;
    }

    internal static Guid HookCColorCorrectionVolumeThinkFunc()
    {
        CColorCorrectionVolumeThinkFuncHookGuid = CColorCorrectionVolumeThinkFuncGetUnmanagedFunction().AddHook(next => (a1) => CColorCorrectionVolumeThinkFuncPipeline(a1, () => next()(a1)));
        return CColorCorrectionVolumeThinkFuncHookGuid;
    }

    internal static Guid UnhookCColorCorrectionVolumeThinkFunc()
    {
        CColorCorrectionVolumeThinkFuncGetUnmanagedFunction().RemoveHook(CColorCorrectionVolumeThinkFuncHookGuid);
        return Guid.Empty;
    }

    private static void CColorCorrectionVolumeThinkFuncPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CColorCorrectionVolume>(a1);

            var preCtx = new CColorCorrectionVolumeThinkFuncPreContext { SchemaObject = schemaObject };
            InvokeCColorCorrectionVolumeThinkFuncPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CColorCorrectionVolumeThinkFuncPostContext { SchemaObject = schemaObject };
            InvokeCColorCorrectionVolumeThinkFuncPost(ref postCtx);
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

    internal static void InvokeCColorCorrectionVolumeThinkFunc(nint a1)
    {
        CColorCorrectionVolumeThinkFuncGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCColorCorrectionVolumeThinkFuncPre(ref CColorCorrectionVolumeThinkFuncPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCColorCorrectionVolumeThinkFuncPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCColorCorrectionVolumeThinkFuncPost(ref CColorCorrectionVolumeThinkFuncPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCColorCorrectionVolumeThinkFuncPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CColorCorrectionVolumeThinkFuncHook : ICColorCorrectionVolumeThinkFuncHook
{
    private event OnCColorCorrectionVolumeThinkFuncPreDelegate? _Pre;
    private event OnCColorCorrectionVolumeThinkFuncPostDelegate? _Post;

    public event OnCColorCorrectionVolumeThinkFuncPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CColorCorrectionVolumeThinkFunc);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CColorCorrectionVolumeThinkFunc);
            }
        }
    }

    public event OnCColorCorrectionVolumeThinkFuncPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CColorCorrectionVolumeThinkFunc);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CColorCorrectionVolumeThinkFunc);
            }
        }
    }

    public void InvokePre(ref CColorCorrectionVolumeThinkFuncPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CColorCorrectionVolumeThinkFuncPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CColorCorrectionVolumeThinkFunc);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CColorCorrectionVolumeThinkFunc);
        }
    }

    public void Invoke(CColorCorrectionVolume schemaObject) => DatamapHooksPublisher.InvokeCColorCorrectionVolumeThinkFunc(schemaObject.Address);
}