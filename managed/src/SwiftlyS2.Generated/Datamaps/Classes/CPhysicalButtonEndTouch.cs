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
    private delegate void CPhysicalButtonEndTouchDelegate(nint a1);

    private static IUnmanagedFunction<CPhysicalButtonEndTouchDelegate>? CPhysicalButtonEndTouchUnmanagedFunction;
    private static Guid CPhysicalButtonEndTouchHookGuid;

    private static IUnmanagedFunction<CPhysicalButtonEndTouchDelegate> CPhysicalButtonEndTouchGetUnmanagedFunction()
    {
        if (CPhysicalButtonEndTouchUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPhysicalButton", "CPhysicalButtonEndTouch");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPhysicalButton::CPhysicalButtonEndTouch.");
            }
            CPhysicalButtonEndTouchUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPhysicalButtonEndTouchDelegate>(address);
        }
        return CPhysicalButtonEndTouchUnmanagedFunction;
    }

    internal static Guid HookCPhysicalButtonEndTouch()
    {
        CPhysicalButtonEndTouchHookGuid = CPhysicalButtonEndTouchGetUnmanagedFunction().AddHook(next => (a1) => CPhysicalButtonEndTouchPipeline(a1, () => next()(a1)));
        return CPhysicalButtonEndTouchHookGuid;
    }

    internal static Guid UnhookCPhysicalButtonEndTouch()
    {
        CPhysicalButtonEndTouchGetUnmanagedFunction().RemoveHook(CPhysicalButtonEndTouchHookGuid);
        return Guid.Empty;
    }

    private static void CPhysicalButtonEndTouchPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPhysicalButton>(a1);

            var preCtx = new CPhysicalButtonEndTouchPreContext { SchemaObject = schemaObject };
            InvokeCPhysicalButtonEndTouchPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPhysicalButtonEndTouchPostContext { SchemaObject = schemaObject };
            InvokeCPhysicalButtonEndTouchPost(ref postCtx);
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

    internal static void InvokeCPhysicalButtonEndTouch(nint a1)
    {
        CPhysicalButtonEndTouchGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPhysicalButtonEndTouchPre(ref CPhysicalButtonEndTouchPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicalButtonEndTouchPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPhysicalButtonEndTouchPost(ref CPhysicalButtonEndTouchPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicalButtonEndTouchPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPhysicalButtonEndTouchHook : ICPhysicalButtonEndTouchHook
{
    private event OnCPhysicalButtonEndTouchPreDelegate? _Pre;
    private event OnCPhysicalButtonEndTouchPostDelegate? _Post;

    public event OnCPhysicalButtonEndTouchPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicalButtonEndTouch);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonEndTouch);
            }
        }
    }

    public event OnCPhysicalButtonEndTouchPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicalButtonEndTouch);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonEndTouch);
            }
        }
    }

    public void InvokePre(ref CPhysicalButtonEndTouchPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPhysicalButtonEndTouchPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonEndTouch);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonEndTouch);
        }
    }

    public void Invoke(CPhysicalButton schemaObject) => DatamapHooksPublisher.InvokeCPhysicalButtonEndTouch(schemaObject.Address);
}