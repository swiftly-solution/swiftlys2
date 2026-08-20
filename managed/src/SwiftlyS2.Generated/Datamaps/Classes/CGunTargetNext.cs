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
    private delegate void CGunTargetNextDelegate(nint a1);

    private static IUnmanagedFunction<CGunTargetNextDelegate>? CGunTargetNextUnmanagedFunction;
    private static Guid CGunTargetNextHookGuid;

    private static IUnmanagedFunction<CGunTargetNextDelegate> CGunTargetNextGetUnmanagedFunction()
    {
        if (CGunTargetNextUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CGunTarget", "CGunTargetNext");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CGunTarget::CGunTargetNext.");
            }
            CGunTargetNextUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CGunTargetNextDelegate>(address);
        }
        return CGunTargetNextUnmanagedFunction;
    }

    internal static Guid HookCGunTargetNext()
    {
        CGunTargetNextHookGuid = CGunTargetNextGetUnmanagedFunction().AddHook(next => (a1) => CGunTargetNextPipeline(a1, () => next()(a1)));
        return CGunTargetNextHookGuid;
    }

    internal static Guid UnhookCGunTargetNext()
    {
        CGunTargetNextGetUnmanagedFunction().RemoveHook(CGunTargetNextHookGuid);
        return Guid.Empty;
    }

    private static void CGunTargetNextPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CGunTarget>(a1);

            var preCtx = new CGunTargetNextPreContext { SchemaObject = schemaObject };
            InvokeCGunTargetNextPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CGunTargetNextPostContext { SchemaObject = schemaObject };
            InvokeCGunTargetNextPost(ref postCtx);
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

    internal static void InvokeCGunTargetNext(nint a1)
    {
        CGunTargetNextGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCGunTargetNextPre(ref CGunTargetNextPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCGunTargetNextPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCGunTargetNextPost(ref CGunTargetNextPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCGunTargetNextPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CGunTargetNextHook : ICGunTargetNextHook
{
    private event OnCGunTargetNextPreDelegate? _Pre;
    private event OnCGunTargetNextPostDelegate? _Post;

    public event OnCGunTargetNextPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CGunTargetNext);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CGunTargetNext);
            }
        }
    }

    public event OnCGunTargetNextPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CGunTargetNext);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CGunTargetNext);
            }
        }
    }

    public void InvokePre(ref CGunTargetNextPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CGunTargetNextPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CGunTargetNext);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CGunTargetNext);
        }
    }

    public void Invoke(CGunTarget schemaObject) => DatamapHooksPublisher.InvokeCGunTargetNext(schemaObject.Address);
}