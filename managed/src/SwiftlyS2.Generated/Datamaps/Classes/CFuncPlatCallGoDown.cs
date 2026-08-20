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
    private delegate void CFuncPlatCallGoDownDelegate(nint a1);

    private static IUnmanagedFunction<CFuncPlatCallGoDownDelegate>? CFuncPlatCallGoDownUnmanagedFunction;
    private static Guid CFuncPlatCallGoDownHookGuid;

    private static IUnmanagedFunction<CFuncPlatCallGoDownDelegate> CFuncPlatCallGoDownGetUnmanagedFunction()
    {
        if (CFuncPlatCallGoDownUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncPlat", "CFuncPlatCallGoDown");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncPlat::CFuncPlatCallGoDown.");
            }
            CFuncPlatCallGoDownUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncPlatCallGoDownDelegate>(address);
        }
        return CFuncPlatCallGoDownUnmanagedFunction;
    }

    internal static Guid HookCFuncPlatCallGoDown()
    {
        CFuncPlatCallGoDownHookGuid = CFuncPlatCallGoDownGetUnmanagedFunction().AddHook(next => (a1) => CFuncPlatCallGoDownPipeline(a1, () => next()(a1)));
        return CFuncPlatCallGoDownHookGuid;
    }

    internal static Guid UnhookCFuncPlatCallGoDown()
    {
        CFuncPlatCallGoDownGetUnmanagedFunction().RemoveHook(CFuncPlatCallGoDownHookGuid);
        return Guid.Empty;
    }

    private static void CFuncPlatCallGoDownPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncPlat>(a1);

            var preCtx = new CFuncPlatCallGoDownPreContext { SchemaObject = schemaObject };
            InvokeCFuncPlatCallGoDownPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncPlatCallGoDownPostContext { SchemaObject = schemaObject };
            InvokeCFuncPlatCallGoDownPost(ref postCtx);
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

    internal static void InvokeCFuncPlatCallGoDown(nint a1)
    {
        CFuncPlatCallGoDownGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncPlatCallGoDownPre(ref CFuncPlatCallGoDownPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncPlatCallGoDownPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncPlatCallGoDownPost(ref CFuncPlatCallGoDownPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncPlatCallGoDownPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncPlatCallGoDownHook : ICFuncPlatCallGoDownHook
{
    private event OnCFuncPlatCallGoDownPreDelegate? _Pre;
    private event OnCFuncPlatCallGoDownPostDelegate? _Post;

    public event OnCFuncPlatCallGoDownPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncPlatCallGoDown);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncPlatCallGoDown);
            }
        }
    }

    public event OnCFuncPlatCallGoDownPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncPlatCallGoDown);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncPlatCallGoDown);
            }
        }
    }

    public void InvokePre(ref CFuncPlatCallGoDownPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncPlatCallGoDownPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncPlatCallGoDown);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncPlatCallGoDown);
        }
    }

    public void Invoke(CFuncPlat schemaObject) => DatamapHooksPublisher.InvokeCFuncPlatCallGoDown(schemaObject.Address);
}