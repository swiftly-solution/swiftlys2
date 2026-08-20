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
    private delegate void CFuncRotatingRotatingUseDelegate(nint a1);

    private static IUnmanagedFunction<CFuncRotatingRotatingUseDelegate>? CFuncRotatingRotatingUseUnmanagedFunction;
    private static Guid CFuncRotatingRotatingUseHookGuid;

    private static IUnmanagedFunction<CFuncRotatingRotatingUseDelegate> CFuncRotatingRotatingUseGetUnmanagedFunction()
    {
        if (CFuncRotatingRotatingUseUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncRotating", "CFuncRotatingRotatingUse");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncRotating::CFuncRotatingRotatingUse.");
            }
            CFuncRotatingRotatingUseUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncRotatingRotatingUseDelegate>(address);
        }
        return CFuncRotatingRotatingUseUnmanagedFunction;
    }

    internal static Guid HookCFuncRotatingRotatingUse()
    {
        CFuncRotatingRotatingUseHookGuid = CFuncRotatingRotatingUseGetUnmanagedFunction().AddHook(next => (a1) => CFuncRotatingRotatingUsePipeline(a1, () => next()(a1)));
        return CFuncRotatingRotatingUseHookGuid;
    }

    internal static Guid UnhookCFuncRotatingRotatingUse()
    {
        CFuncRotatingRotatingUseGetUnmanagedFunction().RemoveHook(CFuncRotatingRotatingUseHookGuid);
        return Guid.Empty;
    }

    private static void CFuncRotatingRotatingUsePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncRotating>(a1);

            var preCtx = new CFuncRotatingRotatingUsePreContext { SchemaObject = schemaObject };
            InvokeCFuncRotatingRotatingUsePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncRotatingRotatingUsePostContext { SchemaObject = schemaObject };
            InvokeCFuncRotatingRotatingUsePost(ref postCtx);
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

    internal static void InvokeCFuncRotatingRotatingUse(nint a1)
    {
        CFuncRotatingRotatingUseGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncRotatingRotatingUsePre(ref CFuncRotatingRotatingUsePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncRotatingRotatingUsePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncRotatingRotatingUsePost(ref CFuncRotatingRotatingUsePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncRotatingRotatingUsePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncRotatingRotatingUseHook : ICFuncRotatingRotatingUseHook
{
    private event OnCFuncRotatingRotatingUsePreDelegate? _Pre;
    private event OnCFuncRotatingRotatingUsePostDelegate? _Post;

    public event OnCFuncRotatingRotatingUsePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncRotatingRotatingUse);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingRotatingUse);
            }
        }
    }

    public event OnCFuncRotatingRotatingUsePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncRotatingRotatingUse);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingRotatingUse);
            }
        }
    }

    public void InvokePre(ref CFuncRotatingRotatingUsePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncRotatingRotatingUsePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingRotatingUse);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingRotatingUse);
        }
    }

    public void Invoke(CFuncRotating schemaObject) => DatamapHooksPublisher.InvokeCFuncRotatingRotatingUse(schemaObject.Address);
}