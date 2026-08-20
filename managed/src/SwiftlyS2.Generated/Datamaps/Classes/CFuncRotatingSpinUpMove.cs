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
    private delegate void CFuncRotatingSpinUpMoveDelegate(nint a1);

    private static IUnmanagedFunction<CFuncRotatingSpinUpMoveDelegate>? CFuncRotatingSpinUpMoveUnmanagedFunction;
    private static Guid CFuncRotatingSpinUpMoveHookGuid;

    private static IUnmanagedFunction<CFuncRotatingSpinUpMoveDelegate> CFuncRotatingSpinUpMoveGetUnmanagedFunction()
    {
        if (CFuncRotatingSpinUpMoveUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncRotating", "CFuncRotatingSpinUpMove");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncRotating::CFuncRotatingSpinUpMove.");
            }
            CFuncRotatingSpinUpMoveUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncRotatingSpinUpMoveDelegate>(address);
        }
        return CFuncRotatingSpinUpMoveUnmanagedFunction;
    }

    internal static Guid HookCFuncRotatingSpinUpMove()
    {
        CFuncRotatingSpinUpMoveHookGuid = CFuncRotatingSpinUpMoveGetUnmanagedFunction().AddHook(next => (a1) => CFuncRotatingSpinUpMovePipeline(a1, () => next()(a1)));
        return CFuncRotatingSpinUpMoveHookGuid;
    }

    internal static Guid UnhookCFuncRotatingSpinUpMove()
    {
        CFuncRotatingSpinUpMoveGetUnmanagedFunction().RemoveHook(CFuncRotatingSpinUpMoveHookGuid);
        return Guid.Empty;
    }

    private static void CFuncRotatingSpinUpMovePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncRotating>(a1);

            var preCtx = new CFuncRotatingSpinUpMovePreContext { SchemaObject = schemaObject };
            InvokeCFuncRotatingSpinUpMovePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncRotatingSpinUpMovePostContext { SchemaObject = schemaObject };
            InvokeCFuncRotatingSpinUpMovePost(ref postCtx);
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

    internal static void InvokeCFuncRotatingSpinUpMove(nint a1)
    {
        CFuncRotatingSpinUpMoveGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncRotatingSpinUpMovePre(ref CFuncRotatingSpinUpMovePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncRotatingSpinUpMovePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncRotatingSpinUpMovePost(ref CFuncRotatingSpinUpMovePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncRotatingSpinUpMovePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncRotatingSpinUpMoveHook : ICFuncRotatingSpinUpMoveHook
{
    private event OnCFuncRotatingSpinUpMovePreDelegate? _Pre;
    private event OnCFuncRotatingSpinUpMovePostDelegate? _Post;

    public event OnCFuncRotatingSpinUpMovePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncRotatingSpinUpMove);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingSpinUpMove);
            }
        }
    }

    public event OnCFuncRotatingSpinUpMovePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncRotatingSpinUpMove);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingSpinUpMove);
            }
        }
    }

    public void InvokePre(ref CFuncRotatingSpinUpMovePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncRotatingSpinUpMovePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingSpinUpMove);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingSpinUpMove);
        }
    }

    public void Invoke(CFuncRotating schemaObject) => DatamapHooksPublisher.InvokeCFuncRotatingSpinUpMove(schemaObject.Address);
}