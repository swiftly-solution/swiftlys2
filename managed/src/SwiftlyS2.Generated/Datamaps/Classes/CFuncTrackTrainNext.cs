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
    private delegate void CFuncTrackTrainNextDelegate(nint a1);

    private static IUnmanagedFunction<CFuncTrackTrainNextDelegate>? CFuncTrackTrainNextUnmanagedFunction;
    private static Guid CFuncTrackTrainNextHookGuid;

    private static IUnmanagedFunction<CFuncTrackTrainNextDelegate> CFuncTrackTrainNextGetUnmanagedFunction()
    {
        if (CFuncTrackTrainNextUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncTrackTrain", "CFuncTrackTrainNext");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncTrackTrain::CFuncTrackTrainNext.");
            }
            CFuncTrackTrainNextUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncTrackTrainNextDelegate>(address);
        }
        return CFuncTrackTrainNextUnmanagedFunction;
    }

    internal static Guid HookCFuncTrackTrainNext()
    {
        CFuncTrackTrainNextHookGuid = CFuncTrackTrainNextGetUnmanagedFunction().AddHook(next => (a1) => CFuncTrackTrainNextPipeline(a1, () => next()(a1)));
        return CFuncTrackTrainNextHookGuid;
    }

    internal static Guid UnhookCFuncTrackTrainNext()
    {
        CFuncTrackTrainNextGetUnmanagedFunction().RemoveHook(CFuncTrackTrainNextHookGuid);
        return Guid.Empty;
    }

    private static void CFuncTrackTrainNextPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncTrackTrain>(a1);

            var preCtx = new CFuncTrackTrainNextPreContext { SchemaObject = schemaObject };
            InvokeCFuncTrackTrainNextPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncTrackTrainNextPostContext { SchemaObject = schemaObject };
            InvokeCFuncTrackTrainNextPost(ref postCtx);
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

    internal static void InvokeCFuncTrackTrainNext(nint a1)
    {
        CFuncTrackTrainNextGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncTrackTrainNextPre(ref CFuncTrackTrainNextPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncTrackTrainNextPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncTrackTrainNextPost(ref CFuncTrackTrainNextPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncTrackTrainNextPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncTrackTrainNextHook : ICFuncTrackTrainNextHook
{
    private event OnCFuncTrackTrainNextPreDelegate? _Pre;
    private event OnCFuncTrackTrainNextPostDelegate? _Post;

    public event OnCFuncTrackTrainNextPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncTrackTrainNext);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrackTrainNext);
            }
        }
    }

    public event OnCFuncTrackTrainNextPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncTrackTrainNext);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrackTrainNext);
            }
        }
    }

    public void InvokePre(ref CFuncTrackTrainNextPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncTrackTrainNextPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrackTrainNext);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrackTrainNext);
        }
    }

    public void Invoke(CFuncTrackTrain schemaObject) => DatamapHooksPublisher.InvokeCFuncTrackTrainNext(schemaObject.Address);
}