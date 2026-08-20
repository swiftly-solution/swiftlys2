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
    private delegate void CGunTargetWaitDelegate(nint a1);

    private static IUnmanagedFunction<CGunTargetWaitDelegate>? CGunTargetWaitUnmanagedFunction;
    private static Guid CGunTargetWaitHookGuid;

    private static IUnmanagedFunction<CGunTargetWaitDelegate> CGunTargetWaitGetUnmanagedFunction()
    {
        if (CGunTargetWaitUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CGunTarget", "CGunTargetWait");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CGunTarget::CGunTargetWait.");
            }
            CGunTargetWaitUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CGunTargetWaitDelegate>(address);
        }
        return CGunTargetWaitUnmanagedFunction;
    }

    internal static Guid HookCGunTargetWait()
    {
        CGunTargetWaitHookGuid = CGunTargetWaitGetUnmanagedFunction().AddHook(next => (a1) => CGunTargetWaitPipeline(a1, () => next()(a1)));
        return CGunTargetWaitHookGuid;
    }

    internal static Guid UnhookCGunTargetWait()
    {
        CGunTargetWaitGetUnmanagedFunction().RemoveHook(CGunTargetWaitHookGuid);
        return Guid.Empty;
    }

    private static void CGunTargetWaitPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CGunTarget>(a1);

            var preCtx = new CGunTargetWaitPreContext { SchemaObject = schemaObject };
            InvokeCGunTargetWaitPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CGunTargetWaitPostContext { SchemaObject = schemaObject };
            InvokeCGunTargetWaitPost(ref postCtx);
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

    internal static void InvokeCGunTargetWait(nint a1)
    {
        CGunTargetWaitGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCGunTargetWaitPre(ref CGunTargetWaitPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCGunTargetWaitPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCGunTargetWaitPost(ref CGunTargetWaitPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCGunTargetWaitPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CGunTargetWaitHook : ICGunTargetWaitHook
{
    private event OnCGunTargetWaitPreDelegate? _Pre;
    private event OnCGunTargetWaitPostDelegate? _Post;

    public event OnCGunTargetWaitPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CGunTargetWait);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CGunTargetWait);
            }
        }
    }

    public event OnCGunTargetWaitPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CGunTargetWait);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CGunTargetWait);
            }
        }
    }

    public void InvokePre(ref CGunTargetWaitPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CGunTargetWaitPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CGunTargetWait);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CGunTargetWait);
        }
    }

    public void Invoke(CGunTarget schemaObject) => DatamapHooksPublisher.InvokeCGunTargetWait(schemaObject.Address);
}