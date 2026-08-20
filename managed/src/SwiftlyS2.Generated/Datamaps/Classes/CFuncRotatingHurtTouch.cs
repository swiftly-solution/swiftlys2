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
    private delegate void CFuncRotatingHurtTouchDelegate(nint a1);

    private static IUnmanagedFunction<CFuncRotatingHurtTouchDelegate>? CFuncRotatingHurtTouchUnmanagedFunction;
    private static Guid CFuncRotatingHurtTouchHookGuid;

    private static IUnmanagedFunction<CFuncRotatingHurtTouchDelegate> CFuncRotatingHurtTouchGetUnmanagedFunction()
    {
        if (CFuncRotatingHurtTouchUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncRotating", "CFuncRotatingHurtTouch");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncRotating::CFuncRotatingHurtTouch.");
            }
            CFuncRotatingHurtTouchUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncRotatingHurtTouchDelegate>(address);
        }
        return CFuncRotatingHurtTouchUnmanagedFunction;
    }

    internal static Guid HookCFuncRotatingHurtTouch()
    {
        CFuncRotatingHurtTouchHookGuid = CFuncRotatingHurtTouchGetUnmanagedFunction().AddHook(next => (a1) => CFuncRotatingHurtTouchPipeline(a1, () => next()(a1)));
        return CFuncRotatingHurtTouchHookGuid;
    }

    internal static Guid UnhookCFuncRotatingHurtTouch()
    {
        CFuncRotatingHurtTouchGetUnmanagedFunction().RemoveHook(CFuncRotatingHurtTouchHookGuid);
        return Guid.Empty;
    }

    private static void CFuncRotatingHurtTouchPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncRotating>(a1);

            var preCtx = new CFuncRotatingHurtTouchPreContext { SchemaObject = schemaObject };
            InvokeCFuncRotatingHurtTouchPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncRotatingHurtTouchPostContext { SchemaObject = schemaObject };
            InvokeCFuncRotatingHurtTouchPost(ref postCtx);
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

    internal static void InvokeCFuncRotatingHurtTouch(nint a1)
    {
        CFuncRotatingHurtTouchGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncRotatingHurtTouchPre(ref CFuncRotatingHurtTouchPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncRotatingHurtTouchPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncRotatingHurtTouchPost(ref CFuncRotatingHurtTouchPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncRotatingHurtTouchPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncRotatingHurtTouchHook : ICFuncRotatingHurtTouchHook
{
    private event OnCFuncRotatingHurtTouchPreDelegate? _Pre;
    private event OnCFuncRotatingHurtTouchPostDelegate? _Post;

    public event OnCFuncRotatingHurtTouchPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncRotatingHurtTouch);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingHurtTouch);
            }
        }
    }

    public event OnCFuncRotatingHurtTouchPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncRotatingHurtTouch);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingHurtTouch);
            }
        }
    }

    public void InvokePre(ref CFuncRotatingHurtTouchPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncRotatingHurtTouchPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingHurtTouch);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncRotatingHurtTouch);
        }
    }

    public void Invoke(CFuncRotating schemaObject) => DatamapHooksPublisher.InvokeCFuncRotatingHurtTouch(schemaObject.Address);
}