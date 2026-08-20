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
    private delegate void CBaseButtonButtonSparkDelegate(nint a1);

    private static IUnmanagedFunction<CBaseButtonButtonSparkDelegate>? CBaseButtonButtonSparkUnmanagedFunction;
    private static Guid CBaseButtonButtonSparkHookGuid;

    private static IUnmanagedFunction<CBaseButtonButtonSparkDelegate> CBaseButtonButtonSparkGetUnmanagedFunction()
    {
        if (CBaseButtonButtonSparkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseButton", "CBaseButtonButtonSpark");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseButton::CBaseButtonButtonSpark.");
            }
            CBaseButtonButtonSparkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseButtonButtonSparkDelegate>(address);
        }
        return CBaseButtonButtonSparkUnmanagedFunction;
    }

    internal static Guid HookCBaseButtonButtonSpark()
    {
        CBaseButtonButtonSparkHookGuid = CBaseButtonButtonSparkGetUnmanagedFunction().AddHook(next => (a1) => CBaseButtonButtonSparkPipeline(a1, () => next()(a1)));
        return CBaseButtonButtonSparkHookGuid;
    }

    internal static Guid UnhookCBaseButtonButtonSpark()
    {
        CBaseButtonButtonSparkGetUnmanagedFunction().RemoveHook(CBaseButtonButtonSparkHookGuid);
        return Guid.Empty;
    }

    private static void CBaseButtonButtonSparkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseButton>(a1);

            var preCtx = new CBaseButtonButtonSparkPreContext { SchemaObject = schemaObject };
            InvokeCBaseButtonButtonSparkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseButtonButtonSparkPostContext { SchemaObject = schemaObject };
            InvokeCBaseButtonButtonSparkPost(ref postCtx);
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

    internal static void InvokeCBaseButtonButtonSpark(nint a1)
    {
        CBaseButtonButtonSparkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseButtonButtonSparkPre(ref CBaseButtonButtonSparkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseButtonButtonSparkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseButtonButtonSparkPost(ref CBaseButtonButtonSparkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseButtonButtonSparkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseButtonButtonSparkHook : ICBaseButtonButtonSparkHook
{
    private event OnCBaseButtonButtonSparkPreDelegate? _Pre;
    private event OnCBaseButtonButtonSparkPostDelegate? _Post;

    public event OnCBaseButtonButtonSparkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseButtonButtonSpark);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonButtonSpark);
            }
        }
    }

    public event OnCBaseButtonButtonSparkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseButtonButtonSpark);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonButtonSpark);
            }
        }
    }

    public void InvokePre(ref CBaseButtonButtonSparkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseButtonButtonSparkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonButtonSpark);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonButtonSpark);
        }
    }

    public void Invoke(CBaseButton schemaObject) => DatamapHooksPublisher.InvokeCBaseButtonButtonSpark(schemaObject.Address);
}