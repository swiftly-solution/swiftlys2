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
    private delegate void CFuncTrackTrainDeadEndDelegate(nint a1);

    private static IUnmanagedFunction<CFuncTrackTrainDeadEndDelegate>? CFuncTrackTrainDeadEndUnmanagedFunction;
    private static Guid CFuncTrackTrainDeadEndHookGuid;

    private static IUnmanagedFunction<CFuncTrackTrainDeadEndDelegate> CFuncTrackTrainDeadEndGetUnmanagedFunction()
    {
        if (CFuncTrackTrainDeadEndUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncTrackTrain", "CFuncTrackTrainDeadEnd");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncTrackTrain::CFuncTrackTrainDeadEnd.");
            }
            CFuncTrackTrainDeadEndUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncTrackTrainDeadEndDelegate>(address);
        }
        return CFuncTrackTrainDeadEndUnmanagedFunction;
    }

    internal static Guid HookCFuncTrackTrainDeadEnd()
    {
        CFuncTrackTrainDeadEndHookGuid = CFuncTrackTrainDeadEndGetUnmanagedFunction().AddHook(next => (a1) => CFuncTrackTrainDeadEndPipeline(a1, () => next()(a1)));
        return CFuncTrackTrainDeadEndHookGuid;
    }

    internal static Guid UnhookCFuncTrackTrainDeadEnd()
    {
        CFuncTrackTrainDeadEndGetUnmanagedFunction().RemoveHook(CFuncTrackTrainDeadEndHookGuid);
        return Guid.Empty;
    }

    private static void CFuncTrackTrainDeadEndPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncTrackTrain>(a1);

            var preCtx = new CFuncTrackTrainDeadEndPreContext { SchemaObject = schemaObject };
            InvokeCFuncTrackTrainDeadEndPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncTrackTrainDeadEndPostContext { SchemaObject = schemaObject };
            InvokeCFuncTrackTrainDeadEndPost(ref postCtx);
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

    internal static void InvokeCFuncTrackTrainDeadEnd(nint a1)
    {
        CFuncTrackTrainDeadEndGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncTrackTrainDeadEndPre(ref CFuncTrackTrainDeadEndPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncTrackTrainDeadEndPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncTrackTrainDeadEndPost(ref CFuncTrackTrainDeadEndPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncTrackTrainDeadEndPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncTrackTrainDeadEndHook : ICFuncTrackTrainDeadEndHook
{
    private event OnCFuncTrackTrainDeadEndPreDelegate? _Pre;
    private event OnCFuncTrackTrainDeadEndPostDelegate? _Post;

    public event OnCFuncTrackTrainDeadEndPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncTrackTrainDeadEnd);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrackTrainDeadEnd);
            }
        }
    }

    public event OnCFuncTrackTrainDeadEndPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncTrackTrainDeadEnd);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrackTrainDeadEnd);
            }
        }
    }

    public void InvokePre(ref CFuncTrackTrainDeadEndPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncTrackTrainDeadEndPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrackTrainDeadEnd);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrackTrainDeadEnd);
        }
    }

    public void Invoke(CFuncTrackTrain schemaObject) => DatamapHooksPublisher.InvokeCFuncTrackTrainDeadEnd(schemaObject.Address);
}