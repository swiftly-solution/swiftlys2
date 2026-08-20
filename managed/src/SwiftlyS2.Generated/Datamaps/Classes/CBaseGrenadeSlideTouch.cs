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
    private delegate void CBaseGrenadeSlideTouchDelegate(nint a1);

    private static IUnmanagedFunction<CBaseGrenadeSlideTouchDelegate>? CBaseGrenadeSlideTouchUnmanagedFunction;
    private static Guid CBaseGrenadeSlideTouchHookGuid;

    private static IUnmanagedFunction<CBaseGrenadeSlideTouchDelegate> CBaseGrenadeSlideTouchGetUnmanagedFunction()
    {
        if (CBaseGrenadeSlideTouchUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseGrenade", "CBaseGrenadeSlideTouch");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseGrenade::CBaseGrenadeSlideTouch.");
            }
            CBaseGrenadeSlideTouchUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseGrenadeSlideTouchDelegate>(address);
        }
        return CBaseGrenadeSlideTouchUnmanagedFunction;
    }

    internal static Guid HookCBaseGrenadeSlideTouch()
    {
        CBaseGrenadeSlideTouchHookGuid = CBaseGrenadeSlideTouchGetUnmanagedFunction().AddHook(next => (a1) => CBaseGrenadeSlideTouchPipeline(a1, () => next()(a1)));
        return CBaseGrenadeSlideTouchHookGuid;
    }

    internal static Guid UnhookCBaseGrenadeSlideTouch()
    {
        CBaseGrenadeSlideTouchGetUnmanagedFunction().RemoveHook(CBaseGrenadeSlideTouchHookGuid);
        return Guid.Empty;
    }

    private static void CBaseGrenadeSlideTouchPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseGrenade>(a1);

            var preCtx = new CBaseGrenadeSlideTouchPreContext { SchemaObject = schemaObject };
            InvokeCBaseGrenadeSlideTouchPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseGrenadeSlideTouchPostContext { SchemaObject = schemaObject };
            InvokeCBaseGrenadeSlideTouchPost(ref postCtx);
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

    internal static void InvokeCBaseGrenadeSlideTouch(nint a1)
    {
        CBaseGrenadeSlideTouchGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseGrenadeSlideTouchPre(ref CBaseGrenadeSlideTouchPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseGrenadeSlideTouchPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseGrenadeSlideTouchPost(ref CBaseGrenadeSlideTouchPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseGrenadeSlideTouchPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseGrenadeSlideTouchHook : ICBaseGrenadeSlideTouchHook
{
    private event OnCBaseGrenadeSlideTouchPreDelegate? _Pre;
    private event OnCBaseGrenadeSlideTouchPostDelegate? _Post;

    public event OnCBaseGrenadeSlideTouchPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseGrenadeSlideTouch);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeSlideTouch);
            }
        }
    }

    public event OnCBaseGrenadeSlideTouchPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseGrenadeSlideTouch);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeSlideTouch);
            }
        }
    }

    public void InvokePre(ref CBaseGrenadeSlideTouchPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseGrenadeSlideTouchPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeSlideTouch);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeSlideTouch);
        }
    }

    public void Invoke(CBaseGrenade schemaObject) => DatamapHooksPublisher.InvokeCBaseGrenadeSlideTouch(schemaObject.Address);
}