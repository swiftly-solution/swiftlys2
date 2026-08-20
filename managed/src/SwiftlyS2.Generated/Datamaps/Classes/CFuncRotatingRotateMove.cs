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
    private delegate void CFuncRotatingRotateMoveDelegate(nint a1);

    private static IUnmanagedFunction<CFuncRotatingRotateMoveDelegate>? CFuncRotatingRotateMoveUnmanagedFunction;
    private static Guid CFuncRotatingRotateMoveHookGuid;

    private static IUnmanagedFunction<CFuncRotatingRotateMoveDelegate> CFuncRotatingRotateMoveGetUnmanagedFunction()
    {
        if (CFuncRotatingRotateMoveUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncRotating", "CFuncRotatingRotateMove");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncRotating::CFuncRotatingRotateMove.");
            }
            CFuncRotatingRotateMoveUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncRotatingRotateMoveDelegate>(address);
        }
        return CFuncRotatingRotateMoveUnmanagedFunction;
    }

    internal static Guid HookCFuncRotatingRotateMove()
    {
        CFuncRotatingRotateMoveHookGuid = CFuncRotatingRotateMoveGetUnmanagedFunction().AddHook(next => (a1) => CFuncRotatingRotateMovePipeline(a1, () => next()(a1)));
        return CFuncRotatingRotateMoveHookGuid;
    }

    internal static Guid UnhookCFuncRotatingRotateMove()
    {
        CFuncRotatingRotateMoveGetUnmanagedFunction().RemoveHook(CFuncRotatingRotateMoveHookGuid);
        return Guid.Empty;
    }

    private static void CFuncRotatingRotateMovePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncRotating>(a1);

            var preCtx = new CFuncRotatingRotateMovePreContext { SchemaObject = schemaObject };
            InvokeCFuncRotatingRotateMovePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncRotatingRotateMovePostContext { SchemaObject = schemaObject };
            InvokeCFuncRotatingRotateMovePost(ref postCtx);
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

    internal static void InvokeCFuncRotatingRotateMove(nint a1)
    {
        CFuncRotatingRotateMoveGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncRotatingRotateMovePre(ref CFuncRotatingRotateMovePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncRotatingRotateMovePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncRotatingRotateMovePost(ref CFuncRotatingRotateMovePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncRotatingRotateMovePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncRotatingRotateMoveHook : ICFuncRotatingRotateMoveHook
{
    private event OnCFuncRotatingRotateMovePreDelegate? _Pre;
    private event OnCFuncRotatingRotateMovePostDelegate? _Post;

    public event OnCFuncRotatingRotateMovePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncRotatingRotateMove);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingRotateMove);
            }
        }
    }

    public event OnCFuncRotatingRotateMovePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncRotatingRotateMove);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingRotateMove);
            }
        }
    }

    public void InvokePre(ref CFuncRotatingRotateMovePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncRotatingRotateMovePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingRotateMove);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingRotateMove);
        }
    }

    public void Invoke(CFuncRotating schemaObject) => DatamapHooksPublisher.InvokeCFuncRotatingRotateMove(schemaObject.Address);
}