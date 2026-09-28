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
    private delegate void CFuncTrainControlsFindDelegate(nint a1);

    private static IUnmanagedFunction<CFuncTrainControlsFindDelegate>? CFuncTrainControlsFindUnmanagedFunction;
    private static Guid CFuncTrainControlsFindHookGuid;

    private static IUnmanagedFunction<CFuncTrainControlsFindDelegate> CFuncTrainControlsFindGetUnmanagedFunction()
    {
        if (CFuncTrainControlsFindUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncTrainControls", "CFuncTrainControlsFind");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncTrainControls::CFuncTrainControlsFind.");
            }
            CFuncTrainControlsFindUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncTrainControlsFindDelegate>(address);
        }
        return CFuncTrainControlsFindUnmanagedFunction;
    }

    internal static Guid HookCFuncTrainControlsFind()
    {
        CFuncTrainControlsFindHookGuid = CFuncTrainControlsFindGetUnmanagedFunction().AddHook(next => (a1) => CFuncTrainControlsFindPipeline(a1, () => next()(a1)));
        return CFuncTrainControlsFindHookGuid;
    }

    internal static Guid UnhookCFuncTrainControlsFind()
    {
        CFuncTrainControlsFindGetUnmanagedFunction().RemoveHook(CFuncTrainControlsFindHookGuid);
        return Guid.Empty;
    }

    private static void CFuncTrainControlsFindPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncTrainControls>(a1);

            var preCtx = new CFuncTrainControlsFindPreContext { SchemaObject = schemaObject };
            InvokeCFuncTrainControlsFindPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncTrainControlsFindPostContext { SchemaObject = schemaObject };
            InvokeCFuncTrainControlsFindPost(ref postCtx);
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

    internal static void InvokeCFuncTrainControlsFind(nint a1)
    {
        CFuncTrainControlsFindGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncTrainControlsFindPre(ref CFuncTrainControlsFindPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncTrainControlsFindPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncTrainControlsFindPost(ref CFuncTrainControlsFindPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncTrainControlsFindPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncTrainControlsFindHook : ICFuncTrainControlsFindHook
{
    private event OnCFuncTrainControlsFindPreDelegate? _Pre;
    private event OnCFuncTrainControlsFindPostDelegate? _Post;

    public event OnCFuncTrainControlsFindPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncTrainControlsFind);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrainControlsFind);
            }
        }
    }

    public event OnCFuncTrainControlsFindPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncTrainControlsFind);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrainControlsFind);
            }
        }
    }

    public void InvokePre(ref CFuncTrainControlsFindPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncTrainControlsFindPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrainControlsFind);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrainControlsFind);
        }
    }

    public void Invoke(CFuncTrainControls schemaObject) => DatamapHooksPublisher.InvokeCFuncTrainControlsFind(schemaObject.Address);
}