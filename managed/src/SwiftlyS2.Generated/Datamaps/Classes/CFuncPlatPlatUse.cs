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
    private delegate void CFuncPlatPlatUseDelegate(nint a1);

    private static IUnmanagedFunction<CFuncPlatPlatUseDelegate>? CFuncPlatPlatUseUnmanagedFunction;
    private static Guid CFuncPlatPlatUseHookGuid;

    private static IUnmanagedFunction<CFuncPlatPlatUseDelegate> CFuncPlatPlatUseGetUnmanagedFunction()
    {
        if (CFuncPlatPlatUseUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncPlat", "CFuncPlatPlatUse");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncPlat::CFuncPlatPlatUse.");
            }
            CFuncPlatPlatUseUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncPlatPlatUseDelegate>(address);
        }
        return CFuncPlatPlatUseUnmanagedFunction;
    }

    internal static Guid HookCFuncPlatPlatUse()
    {
        CFuncPlatPlatUseHookGuid = CFuncPlatPlatUseGetUnmanagedFunction().AddHook(next => (a1) => CFuncPlatPlatUsePipeline(a1, () => next()(a1)));
        return CFuncPlatPlatUseHookGuid;
    }

    internal static Guid UnhookCFuncPlatPlatUse()
    {
        CFuncPlatPlatUseGetUnmanagedFunction().RemoveHook(CFuncPlatPlatUseHookGuid);
        return Guid.Empty;
    }

    private static void CFuncPlatPlatUsePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncPlat>(a1);

            var preCtx = new CFuncPlatPlatUsePreContext { SchemaObject = schemaObject };
            InvokeCFuncPlatPlatUsePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncPlatPlatUsePostContext { SchemaObject = schemaObject };
            InvokeCFuncPlatPlatUsePost(ref postCtx);
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

    internal static void InvokeCFuncPlatPlatUse(nint a1)
    {
        CFuncPlatPlatUseGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncPlatPlatUsePre(ref CFuncPlatPlatUsePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncPlatPlatUsePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncPlatPlatUsePost(ref CFuncPlatPlatUsePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncPlatPlatUsePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncPlatPlatUseHook : ICFuncPlatPlatUseHook
{
    private event OnCFuncPlatPlatUsePreDelegate? _Pre;
    private event OnCFuncPlatPlatUsePostDelegate? _Post;

    public event OnCFuncPlatPlatUsePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncPlatPlatUse);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncPlatPlatUse);
            }
        }
    }

    public event OnCFuncPlatPlatUsePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncPlatPlatUse);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncPlatPlatUse);
            }
        }
    }

    public void InvokePre(ref CFuncPlatPlatUsePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncPlatPlatUsePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncPlatPlatUse);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncPlatPlatUse);
        }
    }

    public void Invoke(CFuncPlat schemaObject) => DatamapHooksPublisher.InvokeCFuncPlatPlatUse(schemaObject.Address);
}