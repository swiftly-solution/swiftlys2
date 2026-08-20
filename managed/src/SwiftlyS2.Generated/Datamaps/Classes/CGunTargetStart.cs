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
    private delegate void CGunTargetStartDelegate(nint a1);

    private static IUnmanagedFunction<CGunTargetStartDelegate>? CGunTargetStartUnmanagedFunction;
    private static Guid CGunTargetStartHookGuid;

    private static IUnmanagedFunction<CGunTargetStartDelegate> CGunTargetStartGetUnmanagedFunction()
    {
        if (CGunTargetStartUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CGunTarget", "CGunTargetStart");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CGunTarget::CGunTargetStart.");
            }
            CGunTargetStartUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CGunTargetStartDelegate>(address);
        }
        return CGunTargetStartUnmanagedFunction;
    }

    internal static Guid HookCGunTargetStart()
    {
        CGunTargetStartHookGuid = CGunTargetStartGetUnmanagedFunction().AddHook(next => (a1) => CGunTargetStartPipeline(a1, () => next()(a1)));
        return CGunTargetStartHookGuid;
    }

    internal static Guid UnhookCGunTargetStart()
    {
        CGunTargetStartGetUnmanagedFunction().RemoveHook(CGunTargetStartHookGuid);
        return Guid.Empty;
    }

    private static void CGunTargetStartPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CGunTarget>(a1);

            var preCtx = new CGunTargetStartPreContext { SchemaObject = schemaObject };
            InvokeCGunTargetStartPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CGunTargetStartPostContext { SchemaObject = schemaObject };
            InvokeCGunTargetStartPost(ref postCtx);
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

    internal static void InvokeCGunTargetStart(nint a1)
    {
        CGunTargetStartGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCGunTargetStartPre(ref CGunTargetStartPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCGunTargetStartPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCGunTargetStartPost(ref CGunTargetStartPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCGunTargetStartPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CGunTargetStartHook : ICGunTargetStartHook
{
    private event OnCGunTargetStartPreDelegate? _Pre;
    private event OnCGunTargetStartPostDelegate? _Post;

    public event OnCGunTargetStartPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CGunTargetStart);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CGunTargetStart);
            }
        }
    }

    public event OnCGunTargetStartPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CGunTargetStart);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CGunTargetStart);
            }
        }
    }

    public void InvokePre(ref CGunTargetStartPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CGunTargetStartPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CGunTargetStart);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CGunTargetStart);
        }
    }

    public void Invoke(CGunTarget schemaObject) => DatamapHooksPublisher.InvokeCGunTargetStart(schemaObject.Address);
}