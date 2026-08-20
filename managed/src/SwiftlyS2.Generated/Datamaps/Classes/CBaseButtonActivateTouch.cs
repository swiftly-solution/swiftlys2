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
    private delegate void CBaseButtonActivateTouchDelegate(nint a1);

    private static IUnmanagedFunction<CBaseButtonActivateTouchDelegate>? CBaseButtonActivateTouchUnmanagedFunction;
    private static Guid CBaseButtonActivateTouchHookGuid;

    private static IUnmanagedFunction<CBaseButtonActivateTouchDelegate> CBaseButtonActivateTouchGetUnmanagedFunction()
    {
        if (CBaseButtonActivateTouchUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseButton", "CBaseButtonActivateTouch");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseButton::CBaseButtonActivateTouch.");
            }
            CBaseButtonActivateTouchUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseButtonActivateTouchDelegate>(address);
        }
        return CBaseButtonActivateTouchUnmanagedFunction;
    }

    internal static Guid HookCBaseButtonActivateTouch()
    {
        CBaseButtonActivateTouchHookGuid = CBaseButtonActivateTouchGetUnmanagedFunction().AddHook(next => (a1) => CBaseButtonActivateTouchPipeline(a1, () => next()(a1)));
        return CBaseButtonActivateTouchHookGuid;
    }

    internal static Guid UnhookCBaseButtonActivateTouch()
    {
        CBaseButtonActivateTouchGetUnmanagedFunction().RemoveHook(CBaseButtonActivateTouchHookGuid);
        return Guid.Empty;
    }

    private static void CBaseButtonActivateTouchPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseButton>(a1);

            var preCtx = new CBaseButtonActivateTouchPreContext { SchemaObject = schemaObject };
            InvokeCBaseButtonActivateTouchPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseButtonActivateTouchPostContext { SchemaObject = schemaObject };
            InvokeCBaseButtonActivateTouchPost(ref postCtx);
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

    internal static void InvokeCBaseButtonActivateTouch(nint a1)
    {
        CBaseButtonActivateTouchGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseButtonActivateTouchPre(ref CBaseButtonActivateTouchPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseButtonActivateTouchPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseButtonActivateTouchPost(ref CBaseButtonActivateTouchPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseButtonActivateTouchPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseButtonActivateTouchHook : ICBaseButtonActivateTouchHook
{
    private event OnCBaseButtonActivateTouchPreDelegate? _Pre;
    private event OnCBaseButtonActivateTouchPostDelegate? _Post;

    public event OnCBaseButtonActivateTouchPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseButtonActivateTouch);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonActivateTouch);
            }
        }
    }

    public event OnCBaseButtonActivateTouchPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseButtonActivateTouch);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonActivateTouch);
            }
        }
    }

    public void InvokePre(ref CBaseButtonActivateTouchPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseButtonActivateTouchPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonActivateTouch);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonActivateTouch);
        }
    }

    public void Invoke(CBaseButton schemaObject) => DatamapHooksPublisher.InvokeCBaseButtonActivateTouch(schemaObject.Address);
}