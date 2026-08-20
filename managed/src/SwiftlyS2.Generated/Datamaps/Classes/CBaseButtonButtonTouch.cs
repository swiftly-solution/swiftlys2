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
    private delegate void CBaseButtonButtonTouchDelegate(nint a1);

    private static IUnmanagedFunction<CBaseButtonButtonTouchDelegate>? CBaseButtonButtonTouchUnmanagedFunction;
    private static Guid CBaseButtonButtonTouchHookGuid;

    private static IUnmanagedFunction<CBaseButtonButtonTouchDelegate> CBaseButtonButtonTouchGetUnmanagedFunction()
    {
        if (CBaseButtonButtonTouchUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseButton", "CBaseButtonButtonTouch");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseButton::CBaseButtonButtonTouch.");
            }
            CBaseButtonButtonTouchUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseButtonButtonTouchDelegate>(address);
        }
        return CBaseButtonButtonTouchUnmanagedFunction;
    }

    internal static Guid HookCBaseButtonButtonTouch()
    {
        CBaseButtonButtonTouchHookGuid = CBaseButtonButtonTouchGetUnmanagedFunction().AddHook(next => (a1) => CBaseButtonButtonTouchPipeline(a1, () => next()(a1)));
        return CBaseButtonButtonTouchHookGuid;
    }

    internal static Guid UnhookCBaseButtonButtonTouch()
    {
        CBaseButtonButtonTouchGetUnmanagedFunction().RemoveHook(CBaseButtonButtonTouchHookGuid);
        return Guid.Empty;
    }

    private static void CBaseButtonButtonTouchPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseButton>(a1);

            var preCtx = new CBaseButtonButtonTouchPreContext { SchemaObject = schemaObject };
            InvokeCBaseButtonButtonTouchPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseButtonButtonTouchPostContext { SchemaObject = schemaObject };
            InvokeCBaseButtonButtonTouchPost(ref postCtx);
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

    internal static void InvokeCBaseButtonButtonTouch(nint a1)
    {
        CBaseButtonButtonTouchGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseButtonButtonTouchPre(ref CBaseButtonButtonTouchPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseButtonButtonTouchPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseButtonButtonTouchPost(ref CBaseButtonButtonTouchPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseButtonButtonTouchPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseButtonButtonTouchHook : ICBaseButtonButtonTouchHook
{
    private event OnCBaseButtonButtonTouchPreDelegate? _Pre;
    private event OnCBaseButtonButtonTouchPostDelegate? _Post;

    public event OnCBaseButtonButtonTouchPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseButtonButtonTouch);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonButtonTouch);
            }
        }
    }

    public event OnCBaseButtonButtonTouchPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseButtonButtonTouch);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonButtonTouch);
            }
        }
    }

    public void InvokePre(ref CBaseButtonButtonTouchPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseButtonButtonTouchPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonButtonTouch);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonButtonTouch);
        }
    }

    public void Invoke(CBaseButton schemaObject) => DatamapHooksPublisher.InvokeCBaseButtonButtonTouch(schemaObject.Address);
}