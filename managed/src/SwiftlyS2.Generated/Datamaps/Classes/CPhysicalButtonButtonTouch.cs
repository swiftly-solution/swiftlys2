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
    private delegate void CPhysicalButtonButtonTouchDelegate(nint a1);

    private static IUnmanagedFunction<CPhysicalButtonButtonTouchDelegate>? CPhysicalButtonButtonTouchUnmanagedFunction;
    private static Guid CPhysicalButtonButtonTouchHookGuid;

    private static IUnmanagedFunction<CPhysicalButtonButtonTouchDelegate> CPhysicalButtonButtonTouchGetUnmanagedFunction()
    {
        if (CPhysicalButtonButtonTouchUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPhysicalButton", "CPhysicalButtonButtonTouch");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPhysicalButton::CPhysicalButtonButtonTouch.");
            }
            CPhysicalButtonButtonTouchUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPhysicalButtonButtonTouchDelegate>(address);
        }
        return CPhysicalButtonButtonTouchUnmanagedFunction;
    }

    internal static Guid HookCPhysicalButtonButtonTouch()
    {
        CPhysicalButtonButtonTouchHookGuid = CPhysicalButtonButtonTouchGetUnmanagedFunction().AddHook(next => (a1) => CPhysicalButtonButtonTouchPipeline(a1, () => next()(a1)));
        return CPhysicalButtonButtonTouchHookGuid;
    }

    internal static Guid UnhookCPhysicalButtonButtonTouch()
    {
        CPhysicalButtonButtonTouchGetUnmanagedFunction().RemoveHook(CPhysicalButtonButtonTouchHookGuid);
        return Guid.Empty;
    }

    private static void CPhysicalButtonButtonTouchPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPhysicalButton>(a1);

            var preCtx = new CPhysicalButtonButtonTouchPreContext { SchemaObject = schemaObject };
            InvokeCPhysicalButtonButtonTouchPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPhysicalButtonButtonTouchPostContext { SchemaObject = schemaObject };
            InvokeCPhysicalButtonButtonTouchPost(ref postCtx);
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

    internal static void InvokeCPhysicalButtonButtonTouch(nint a1)
    {
        CPhysicalButtonButtonTouchGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPhysicalButtonButtonTouchPre(ref CPhysicalButtonButtonTouchPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicalButtonButtonTouchPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPhysicalButtonButtonTouchPost(ref CPhysicalButtonButtonTouchPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicalButtonButtonTouchPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPhysicalButtonButtonTouchHook : ICPhysicalButtonButtonTouchHook
{
    private event OnCPhysicalButtonButtonTouchPreDelegate? _Pre;
    private event OnCPhysicalButtonButtonTouchPostDelegate? _Post;

    public event OnCPhysicalButtonButtonTouchPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicalButtonButtonTouch);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonButtonTouch);
            }
        }
    }

    public event OnCPhysicalButtonButtonTouchPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicalButtonButtonTouch);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonButtonTouch);
            }
        }
    }

    public void InvokePre(ref CPhysicalButtonButtonTouchPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPhysicalButtonButtonTouchPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonButtonTouch);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonButtonTouch);
        }
    }

    public void Invoke(CPhysicalButton schemaObject) => DatamapHooksPublisher.InvokeCPhysicalButtonButtonTouch(schemaObject.Address);
}