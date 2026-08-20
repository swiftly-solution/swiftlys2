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
    private delegate void CFuncRotatingSpinDownMoveDelegate(nint a1);

    private static IUnmanagedFunction<CFuncRotatingSpinDownMoveDelegate>? CFuncRotatingSpinDownMoveUnmanagedFunction;
    private static Guid CFuncRotatingSpinDownMoveHookGuid;

    private static IUnmanagedFunction<CFuncRotatingSpinDownMoveDelegate> CFuncRotatingSpinDownMoveGetUnmanagedFunction()
    {
        if (CFuncRotatingSpinDownMoveUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncRotating", "CFuncRotatingSpinDownMove");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncRotating::CFuncRotatingSpinDownMove.");
            }
            CFuncRotatingSpinDownMoveUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncRotatingSpinDownMoveDelegate>(address);
        }
        return CFuncRotatingSpinDownMoveUnmanagedFunction;
    }

    internal static Guid HookCFuncRotatingSpinDownMove()
    {
        CFuncRotatingSpinDownMoveHookGuid = CFuncRotatingSpinDownMoveGetUnmanagedFunction().AddHook(next => (a1) => CFuncRotatingSpinDownMovePipeline(a1, () => next()(a1)));
        return CFuncRotatingSpinDownMoveHookGuid;
    }

    internal static Guid UnhookCFuncRotatingSpinDownMove()
    {
        CFuncRotatingSpinDownMoveGetUnmanagedFunction().RemoveHook(CFuncRotatingSpinDownMoveHookGuid);
        return Guid.Empty;
    }

    private static void CFuncRotatingSpinDownMovePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncRotating>(a1);

            var preCtx = new CFuncRotatingSpinDownMovePreContext { SchemaObject = schemaObject };
            InvokeCFuncRotatingSpinDownMovePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncRotatingSpinDownMovePostContext { SchemaObject = schemaObject };
            InvokeCFuncRotatingSpinDownMovePost(ref postCtx);
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

    internal static void InvokeCFuncRotatingSpinDownMove(nint a1)
    {
        CFuncRotatingSpinDownMoveGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncRotatingSpinDownMovePre(ref CFuncRotatingSpinDownMovePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncRotatingSpinDownMovePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncRotatingSpinDownMovePost(ref CFuncRotatingSpinDownMovePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncRotatingSpinDownMovePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncRotatingSpinDownMoveHook : ICFuncRotatingSpinDownMoveHook
{
    private event OnCFuncRotatingSpinDownMovePreDelegate? _Pre;
    private event OnCFuncRotatingSpinDownMovePostDelegate? _Post;

    public event OnCFuncRotatingSpinDownMovePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncRotatingSpinDownMove);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingSpinDownMove);
            }
        }
    }

    public event OnCFuncRotatingSpinDownMovePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncRotatingSpinDownMove);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingSpinDownMove);
            }
        }
    }

    public void InvokePre(ref CFuncRotatingSpinDownMovePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncRotatingSpinDownMovePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingSpinDownMove);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingSpinDownMove);
        }
    }

    public void Invoke(CFuncRotating schemaObject) => DatamapHooksPublisher.InvokeCFuncRotatingSpinDownMove(schemaObject.Address);
}