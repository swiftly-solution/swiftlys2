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
    private delegate void CBaseGrenadeBounceTouchDelegate(nint a1);

    private static IUnmanagedFunction<CBaseGrenadeBounceTouchDelegate>? CBaseGrenadeBounceTouchUnmanagedFunction;
    private static Guid CBaseGrenadeBounceTouchHookGuid;

    private static IUnmanagedFunction<CBaseGrenadeBounceTouchDelegate> CBaseGrenadeBounceTouchGetUnmanagedFunction()
    {
        if (CBaseGrenadeBounceTouchUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseGrenade", "CBaseGrenadeBounceTouch");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseGrenade::CBaseGrenadeBounceTouch.");
            }
            CBaseGrenadeBounceTouchUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseGrenadeBounceTouchDelegate>(address);
        }
        return CBaseGrenadeBounceTouchUnmanagedFunction;
    }

    internal static Guid HookCBaseGrenadeBounceTouch()
    {
        CBaseGrenadeBounceTouchHookGuid = CBaseGrenadeBounceTouchGetUnmanagedFunction().AddHook(next => (a1) => CBaseGrenadeBounceTouchPipeline(a1, () => next()(a1)));
        return CBaseGrenadeBounceTouchHookGuid;
    }

    internal static Guid UnhookCBaseGrenadeBounceTouch()
    {
        CBaseGrenadeBounceTouchGetUnmanagedFunction().RemoveHook(CBaseGrenadeBounceTouchHookGuid);
        return Guid.Empty;
    }

    private static void CBaseGrenadeBounceTouchPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseGrenade>(a1);

            var preCtx = new CBaseGrenadeBounceTouchPreContext { SchemaObject = schemaObject };
            InvokeCBaseGrenadeBounceTouchPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseGrenadeBounceTouchPostContext { SchemaObject = schemaObject };
            InvokeCBaseGrenadeBounceTouchPost(ref postCtx);
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

    internal static void InvokeCBaseGrenadeBounceTouch(nint a1)
    {
        CBaseGrenadeBounceTouchGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseGrenadeBounceTouchPre(ref CBaseGrenadeBounceTouchPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseGrenadeBounceTouchPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseGrenadeBounceTouchPost(ref CBaseGrenadeBounceTouchPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseGrenadeBounceTouchPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseGrenadeBounceTouchHook : ICBaseGrenadeBounceTouchHook
{
    private event OnCBaseGrenadeBounceTouchPreDelegate? _Pre;
    private event OnCBaseGrenadeBounceTouchPostDelegate? _Post;

    public event OnCBaseGrenadeBounceTouchPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseGrenadeBounceTouch);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeBounceTouch);
            }
        }
    }

    public event OnCBaseGrenadeBounceTouchPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseGrenadeBounceTouch);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeBounceTouch);
            }
        }
    }

    public void InvokePre(ref CBaseGrenadeBounceTouchPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseGrenadeBounceTouchPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeBounceTouch);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeBounceTouch);
        }
    }

    public void Invoke(CBaseGrenade schemaObject) => DatamapHooksPublisher.InvokeCBaseGrenadeBounceTouch(schemaObject.Address);
}