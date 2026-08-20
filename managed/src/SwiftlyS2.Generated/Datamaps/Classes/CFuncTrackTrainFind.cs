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
    private delegate void CFuncTrackTrainFindDelegate(nint a1);

    private static IUnmanagedFunction<CFuncTrackTrainFindDelegate>? CFuncTrackTrainFindUnmanagedFunction;
    private static Guid CFuncTrackTrainFindHookGuid;

    private static IUnmanagedFunction<CFuncTrackTrainFindDelegate> CFuncTrackTrainFindGetUnmanagedFunction()
    {
        if (CFuncTrackTrainFindUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncTrackTrain", "CFuncTrackTrainFind");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncTrackTrain::CFuncTrackTrainFind.");
            }
            CFuncTrackTrainFindUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncTrackTrainFindDelegate>(address);
        }
        return CFuncTrackTrainFindUnmanagedFunction;
    }

    internal static Guid HookCFuncTrackTrainFind()
    {
        CFuncTrackTrainFindHookGuid = CFuncTrackTrainFindGetUnmanagedFunction().AddHook(next => (a1) => CFuncTrackTrainFindPipeline(a1, () => next()(a1)));
        return CFuncTrackTrainFindHookGuid;
    }

    internal static Guid UnhookCFuncTrackTrainFind()
    {
        CFuncTrackTrainFindGetUnmanagedFunction().RemoveHook(CFuncTrackTrainFindHookGuid);
        return Guid.Empty;
    }

    private static void CFuncTrackTrainFindPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncTrackTrain>(a1);

            var preCtx = new CFuncTrackTrainFindPreContext { SchemaObject = schemaObject };
            InvokeCFuncTrackTrainFindPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncTrackTrainFindPostContext { SchemaObject = schemaObject };
            InvokeCFuncTrackTrainFindPost(ref postCtx);
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

    internal static void InvokeCFuncTrackTrainFind(nint a1)
    {
        CFuncTrackTrainFindGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncTrackTrainFindPre(ref CFuncTrackTrainFindPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncTrackTrainFindPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncTrackTrainFindPost(ref CFuncTrackTrainFindPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncTrackTrainFindPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncTrackTrainFindHook : ICFuncTrackTrainFindHook
{
    private event OnCFuncTrackTrainFindPreDelegate? _Pre;
    private event OnCFuncTrackTrainFindPostDelegate? _Post;

    public event OnCFuncTrackTrainFindPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncTrackTrainFind);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrackTrainFind);
            }
        }
    }

    public event OnCFuncTrackTrainFindPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncTrackTrainFind);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrackTrainFind);
            }
        }
    }

    public void InvokePre(ref CFuncTrackTrainFindPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncTrackTrainFindPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrackTrainFind);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrackTrainFind);
        }
    }

    public void Invoke(CFuncTrackTrain schemaObject) => DatamapHooksPublisher.InvokeCFuncTrackTrainFind(schemaObject.Address);
}