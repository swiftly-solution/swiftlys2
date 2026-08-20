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
    private delegate void CFuncTrackTrainNearestPathDelegate(nint a1);

    private static IUnmanagedFunction<CFuncTrackTrainNearestPathDelegate>? CFuncTrackTrainNearestPathUnmanagedFunction;
    private static Guid CFuncTrackTrainNearestPathHookGuid;

    private static IUnmanagedFunction<CFuncTrackTrainNearestPathDelegate> CFuncTrackTrainNearestPathGetUnmanagedFunction()
    {
        if (CFuncTrackTrainNearestPathUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncTrackTrain", "CFuncTrackTrainNearestPath");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncTrackTrain::CFuncTrackTrainNearestPath.");
            }
            CFuncTrackTrainNearestPathUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncTrackTrainNearestPathDelegate>(address);
        }
        return CFuncTrackTrainNearestPathUnmanagedFunction;
    }

    internal static Guid HookCFuncTrackTrainNearestPath()
    {
        CFuncTrackTrainNearestPathHookGuid = CFuncTrackTrainNearestPathGetUnmanagedFunction().AddHook(next => (a1) => CFuncTrackTrainNearestPathPipeline(a1, () => next()(a1)));
        return CFuncTrackTrainNearestPathHookGuid;
    }

    internal static Guid UnhookCFuncTrackTrainNearestPath()
    {
        CFuncTrackTrainNearestPathGetUnmanagedFunction().RemoveHook(CFuncTrackTrainNearestPathHookGuid);
        return Guid.Empty;
    }

    private static void CFuncTrackTrainNearestPathPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncTrackTrain>(a1);

            var preCtx = new CFuncTrackTrainNearestPathPreContext { SchemaObject = schemaObject };
            InvokeCFuncTrackTrainNearestPathPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncTrackTrainNearestPathPostContext { SchemaObject = schemaObject };
            InvokeCFuncTrackTrainNearestPathPost(ref postCtx);
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

    internal static void InvokeCFuncTrackTrainNearestPath(nint a1)
    {
        CFuncTrackTrainNearestPathGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncTrackTrainNearestPathPre(ref CFuncTrackTrainNearestPathPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncTrackTrainNearestPathPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncTrackTrainNearestPathPost(ref CFuncTrackTrainNearestPathPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncTrackTrainNearestPathPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncTrackTrainNearestPathHook : ICFuncTrackTrainNearestPathHook
{
    private event OnCFuncTrackTrainNearestPathPreDelegate? _Pre;
    private event OnCFuncTrackTrainNearestPathPostDelegate? _Post;

    public event OnCFuncTrackTrainNearestPathPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncTrackTrainNearestPath);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrackTrainNearestPath);
            }
        }
    }

    public event OnCFuncTrackTrainNearestPathPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncTrackTrainNearestPath);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrackTrainNearestPath);
            }
        }
    }

    public void InvokePre(ref CFuncTrackTrainNearestPathPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncTrackTrainNearestPathPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrackTrainNearestPath);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrackTrainNearestPath);
        }
    }

    public void Invoke(CFuncTrackTrain schemaObject) => DatamapHooksPublisher.InvokeCFuncTrackTrainNearestPath(schemaObject.Address);
}