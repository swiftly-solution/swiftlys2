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
    private delegate void CFuncPlatCallHitTopDelegate(nint a1);

    private static IUnmanagedFunction<CFuncPlatCallHitTopDelegate>? CFuncPlatCallHitTopUnmanagedFunction;
    private static Guid CFuncPlatCallHitTopHookGuid;

    private static IUnmanagedFunction<CFuncPlatCallHitTopDelegate> CFuncPlatCallHitTopGetUnmanagedFunction()
    {
        if (CFuncPlatCallHitTopUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncPlat", "CFuncPlatCallHitTop");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncPlat::CFuncPlatCallHitTop.");
            }
            CFuncPlatCallHitTopUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncPlatCallHitTopDelegate>(address);
        }
        return CFuncPlatCallHitTopUnmanagedFunction;
    }

    internal static Guid HookCFuncPlatCallHitTop()
    {
        CFuncPlatCallHitTopHookGuid = CFuncPlatCallHitTopGetUnmanagedFunction().AddHook(next => (a1) => CFuncPlatCallHitTopPipeline(a1, () => next()(a1)));
        return CFuncPlatCallHitTopHookGuid;
    }

    internal static Guid UnhookCFuncPlatCallHitTop()
    {
        CFuncPlatCallHitTopGetUnmanagedFunction().RemoveHook(CFuncPlatCallHitTopHookGuid);
        return Guid.Empty;
    }

    private static void CFuncPlatCallHitTopPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncPlat>(a1);

            var preCtx = new CFuncPlatCallHitTopPreContext { SchemaObject = schemaObject };
            InvokeCFuncPlatCallHitTopPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncPlatCallHitTopPostContext { SchemaObject = schemaObject };
            InvokeCFuncPlatCallHitTopPost(ref postCtx);
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

    internal static void InvokeCFuncPlatCallHitTop(nint a1)
    {
        CFuncPlatCallHitTopGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncPlatCallHitTopPre(ref CFuncPlatCallHitTopPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncPlatCallHitTopPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncPlatCallHitTopPost(ref CFuncPlatCallHitTopPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncPlatCallHitTopPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncPlatCallHitTopHook : ICFuncPlatCallHitTopHook
{
    private event OnCFuncPlatCallHitTopPreDelegate? _Pre;
    private event OnCFuncPlatCallHitTopPostDelegate? _Post;

    public event OnCFuncPlatCallHitTopPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncPlatCallHitTop);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncPlatCallHitTop);
            }
        }
    }

    public event OnCFuncPlatCallHitTopPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncPlatCallHitTop);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncPlatCallHitTop);
            }
        }
    }

    public void InvokePre(ref CFuncPlatCallHitTopPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncPlatCallHitTopPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncPlatCallHitTop);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncPlatCallHitTop);
        }
    }

    public void Invoke(CFuncPlat schemaObject) => DatamapHooksPublisher.InvokeCFuncPlatCallHitTop(schemaObject.Address);
}