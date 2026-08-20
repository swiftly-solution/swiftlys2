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
    private delegate void CFuncTrainNextDelegate(nint a1);

    private static IUnmanagedFunction<CFuncTrainNextDelegate>? CFuncTrainNextUnmanagedFunction;
    private static Guid CFuncTrainNextHookGuid;

    private static IUnmanagedFunction<CFuncTrainNextDelegate> CFuncTrainNextGetUnmanagedFunction()
    {
        if (CFuncTrainNextUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncTrain", "CFuncTrainNext");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncTrain::CFuncTrainNext.");
            }
            CFuncTrainNextUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncTrainNextDelegate>(address);
        }
        return CFuncTrainNextUnmanagedFunction;
    }

    internal static Guid HookCFuncTrainNext()
    {
        CFuncTrainNextHookGuid = CFuncTrainNextGetUnmanagedFunction().AddHook(next => (a1) => CFuncTrainNextPipeline(a1, () => next()(a1)));
        return CFuncTrainNextHookGuid;
    }

    internal static Guid UnhookCFuncTrainNext()
    {
        CFuncTrainNextGetUnmanagedFunction().RemoveHook(CFuncTrainNextHookGuid);
        return Guid.Empty;
    }

    private static void CFuncTrainNextPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncTrain>(a1);

            var preCtx = new CFuncTrainNextPreContext { SchemaObject = schemaObject };
            InvokeCFuncTrainNextPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncTrainNextPostContext { SchemaObject = schemaObject };
            InvokeCFuncTrainNextPost(ref postCtx);
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

    internal static void InvokeCFuncTrainNext(nint a1)
    {
        CFuncTrainNextGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncTrainNextPre(ref CFuncTrainNextPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncTrainNextPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncTrainNextPost(ref CFuncTrainNextPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncTrainNextPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncTrainNextHook : ICFuncTrainNextHook
{
    private event OnCFuncTrainNextPreDelegate? _Pre;
    private event OnCFuncTrainNextPostDelegate? _Post;

    public event OnCFuncTrainNextPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncTrainNext);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrainNext);
            }
        }
    }

    public event OnCFuncTrainNextPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncTrainNext);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrainNext);
            }
        }
    }

    public void InvokePre(ref CFuncTrainNextPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncTrainNextPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrainNext);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrainNext);
        }
    }

    public void Invoke(CFuncTrain schemaObject) => DatamapHooksPublisher.InvokeCFuncTrainNext(schemaObject.Address);
}