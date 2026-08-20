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
    private delegate void CFuncRotatingReverseMoveDelegate(nint a1);

    private static IUnmanagedFunction<CFuncRotatingReverseMoveDelegate>? CFuncRotatingReverseMoveUnmanagedFunction;
    private static Guid CFuncRotatingReverseMoveHookGuid;

    private static IUnmanagedFunction<CFuncRotatingReverseMoveDelegate> CFuncRotatingReverseMoveGetUnmanagedFunction()
    {
        if (CFuncRotatingReverseMoveUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncRotating", "CFuncRotatingReverseMove");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncRotating::CFuncRotatingReverseMove.");
            }
            CFuncRotatingReverseMoveUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncRotatingReverseMoveDelegate>(address);
        }
        return CFuncRotatingReverseMoveUnmanagedFunction;
    }

    internal static Guid HookCFuncRotatingReverseMove()
    {
        CFuncRotatingReverseMoveHookGuid = CFuncRotatingReverseMoveGetUnmanagedFunction().AddHook(next => (a1) => CFuncRotatingReverseMovePipeline(a1, () => next()(a1)));
        return CFuncRotatingReverseMoveHookGuid;
    }

    internal static Guid UnhookCFuncRotatingReverseMove()
    {
        CFuncRotatingReverseMoveGetUnmanagedFunction().RemoveHook(CFuncRotatingReverseMoveHookGuid);
        return Guid.Empty;
    }

    private static void CFuncRotatingReverseMovePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncRotating>(a1);

            var preCtx = new CFuncRotatingReverseMovePreContext { SchemaObject = schemaObject };
            InvokeCFuncRotatingReverseMovePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncRotatingReverseMovePostContext { SchemaObject = schemaObject };
            InvokeCFuncRotatingReverseMovePost(ref postCtx);
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

    internal static void InvokeCFuncRotatingReverseMove(nint a1)
    {
        CFuncRotatingReverseMoveGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncRotatingReverseMovePre(ref CFuncRotatingReverseMovePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncRotatingReverseMovePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncRotatingReverseMovePost(ref CFuncRotatingReverseMovePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncRotatingReverseMovePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncRotatingReverseMoveHook : ICFuncRotatingReverseMoveHook
{
    private event OnCFuncRotatingReverseMovePreDelegate? _Pre;
    private event OnCFuncRotatingReverseMovePostDelegate? _Post;

    public event OnCFuncRotatingReverseMovePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncRotatingReverseMove);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingReverseMove);
            }
        }
    }

    public event OnCFuncRotatingReverseMovePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncRotatingReverseMove);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingReverseMove);
            }
        }
    }

    public void InvokePre(ref CFuncRotatingReverseMovePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncRotatingReverseMovePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingReverseMove);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingReverseMove);
        }
    }

    public void Invoke(CFuncRotating schemaObject) => DatamapHooksPublisher.InvokeCFuncRotatingReverseMove(schemaObject.Address);
}