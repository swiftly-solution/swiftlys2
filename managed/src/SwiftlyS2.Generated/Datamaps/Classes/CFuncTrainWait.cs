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
    private delegate void CFuncTrainWaitDelegate(nint a1);

    private static IUnmanagedFunction<CFuncTrainWaitDelegate>? CFuncTrainWaitUnmanagedFunction;
    private static Guid CFuncTrainWaitHookGuid;

    private static IUnmanagedFunction<CFuncTrainWaitDelegate> CFuncTrainWaitGetUnmanagedFunction()
    {
        if (CFuncTrainWaitUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncTrain", "CFuncTrainWait");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncTrain::CFuncTrainWait.");
            }
            CFuncTrainWaitUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncTrainWaitDelegate>(address);
        }
        return CFuncTrainWaitUnmanagedFunction;
    }

    internal static Guid HookCFuncTrainWait()
    {
        CFuncTrainWaitHookGuid = CFuncTrainWaitGetUnmanagedFunction().AddHook(next => (a1) => CFuncTrainWaitPipeline(a1, () => next()(a1)));
        return CFuncTrainWaitHookGuid;
    }

    internal static Guid UnhookCFuncTrainWait()
    {
        CFuncTrainWaitGetUnmanagedFunction().RemoveHook(CFuncTrainWaitHookGuid);
        return Guid.Empty;
    }

    private static void CFuncTrainWaitPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncTrain>(a1);

            var preCtx = new CFuncTrainWaitPreContext { SchemaObject = schemaObject };
            InvokeCFuncTrainWaitPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncTrainWaitPostContext { SchemaObject = schemaObject };
            InvokeCFuncTrainWaitPost(ref postCtx);
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

    internal static void InvokeCFuncTrainWait(nint a1)
    {
        CFuncTrainWaitGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncTrainWaitPre(ref CFuncTrainWaitPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncTrainWaitPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncTrainWaitPost(ref CFuncTrainWaitPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncTrainWaitPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncTrainWaitHook : ICFuncTrainWaitHook
{
    private event OnCFuncTrainWaitPreDelegate? _Pre;
    private event OnCFuncTrainWaitPostDelegate? _Post;

    public event OnCFuncTrainWaitPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncTrainWait);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrainWait);
            }
        }
    }

    public event OnCFuncTrainWaitPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncTrainWait);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrainWait);
            }
        }
    }

    public void InvokePre(ref CFuncTrainWaitPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncTrainWaitPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrainWait);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrainWait);
        }
    }

    public void Invoke(CFuncTrain schemaObject) => DatamapHooksPublisher.InvokeCFuncTrainWait(schemaObject.Address);
}