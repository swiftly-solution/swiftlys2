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
    private delegate void CBaseGrenadeExplodeTouchDelegate(nint a1);

    private static IUnmanagedFunction<CBaseGrenadeExplodeTouchDelegate>? CBaseGrenadeExplodeTouchUnmanagedFunction;
    private static Guid CBaseGrenadeExplodeTouchHookGuid;

    private static IUnmanagedFunction<CBaseGrenadeExplodeTouchDelegate> CBaseGrenadeExplodeTouchGetUnmanagedFunction()
    {
        if (CBaseGrenadeExplodeTouchUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseGrenade", "CBaseGrenadeExplodeTouch");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseGrenade::CBaseGrenadeExplodeTouch.");
            }
            CBaseGrenadeExplodeTouchUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseGrenadeExplodeTouchDelegate>(address);
        }
        return CBaseGrenadeExplodeTouchUnmanagedFunction;
    }

    internal static Guid HookCBaseGrenadeExplodeTouch()
    {
        CBaseGrenadeExplodeTouchHookGuid = CBaseGrenadeExplodeTouchGetUnmanagedFunction().AddHook(next => (a1) => CBaseGrenadeExplodeTouchPipeline(a1, () => next()(a1)));
        return CBaseGrenadeExplodeTouchHookGuid;
    }

    internal static Guid UnhookCBaseGrenadeExplodeTouch()
    {
        CBaseGrenadeExplodeTouchGetUnmanagedFunction().RemoveHook(CBaseGrenadeExplodeTouchHookGuid);
        return Guid.Empty;
    }

    private static void CBaseGrenadeExplodeTouchPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseGrenade>(a1);

            var preCtx = new CBaseGrenadeExplodeTouchPreContext { SchemaObject = schemaObject };
            InvokeCBaseGrenadeExplodeTouchPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseGrenadeExplodeTouchPostContext { SchemaObject = schemaObject };
            InvokeCBaseGrenadeExplodeTouchPost(ref postCtx);
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

    internal static void InvokeCBaseGrenadeExplodeTouch(nint a1)
    {
        CBaseGrenadeExplodeTouchGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseGrenadeExplodeTouchPre(ref CBaseGrenadeExplodeTouchPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseGrenadeExplodeTouchPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseGrenadeExplodeTouchPost(ref CBaseGrenadeExplodeTouchPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseGrenadeExplodeTouchPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseGrenadeExplodeTouchHook : ICBaseGrenadeExplodeTouchHook
{
    private event OnCBaseGrenadeExplodeTouchPreDelegate? _Pre;
    private event OnCBaseGrenadeExplodeTouchPostDelegate? _Post;

    public event OnCBaseGrenadeExplodeTouchPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseGrenadeExplodeTouch);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeExplodeTouch);
            }
        }
    }

    public event OnCBaseGrenadeExplodeTouchPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseGrenadeExplodeTouch);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeExplodeTouch);
            }
        }
    }

    public void InvokePre(ref CBaseGrenadeExplodeTouchPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseGrenadeExplodeTouchPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeExplodeTouch);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeExplodeTouch);
        }
    }

    public void Invoke(CBaseGrenade schemaObject) => DatamapHooksPublisher.InvokeCBaseGrenadeExplodeTouch(schemaObject.Address);
}