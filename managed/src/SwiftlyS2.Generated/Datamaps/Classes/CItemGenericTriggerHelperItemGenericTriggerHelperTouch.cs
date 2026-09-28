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
    private delegate void CItemGenericTriggerHelperItemGenericTriggerHelperTouchDelegate(nint a1);

    private static IUnmanagedFunction<CItemGenericTriggerHelperItemGenericTriggerHelperTouchDelegate>? CItemGenericTriggerHelperItemGenericTriggerHelperTouchUnmanagedFunction;
    private static Guid CItemGenericTriggerHelperItemGenericTriggerHelperTouchHookGuid;

    private static IUnmanagedFunction<CItemGenericTriggerHelperItemGenericTriggerHelperTouchDelegate> CItemGenericTriggerHelperItemGenericTriggerHelperTouchGetUnmanagedFunction()
    {
        if (CItemGenericTriggerHelperItemGenericTriggerHelperTouchUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CItemGenericTriggerHelper", "CItemGenericTriggerHelperItemGenericTriggerHelperTouch");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CItemGenericTriggerHelper::CItemGenericTriggerHelperItemGenericTriggerHelperTouch.");
            }
            CItemGenericTriggerHelperItemGenericTriggerHelperTouchUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CItemGenericTriggerHelperItemGenericTriggerHelperTouchDelegate>(address);
        }
        return CItemGenericTriggerHelperItemGenericTriggerHelperTouchUnmanagedFunction;
    }

    internal static Guid HookCItemGenericTriggerHelperItemGenericTriggerHelperTouch()
    {
        CItemGenericTriggerHelperItemGenericTriggerHelperTouchHookGuid = CItemGenericTriggerHelperItemGenericTriggerHelperTouchGetUnmanagedFunction().AddHook(next => (a1) => CItemGenericTriggerHelperItemGenericTriggerHelperTouchPipeline(a1, () => next()(a1)));
        return CItemGenericTriggerHelperItemGenericTriggerHelperTouchHookGuid;
    }

    internal static Guid UnhookCItemGenericTriggerHelperItemGenericTriggerHelperTouch()
    {
        CItemGenericTriggerHelperItemGenericTriggerHelperTouchGetUnmanagedFunction().RemoveHook(CItemGenericTriggerHelperItemGenericTriggerHelperTouchHookGuid);
        return Guid.Empty;
    }

    private static void CItemGenericTriggerHelperItemGenericTriggerHelperTouchPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CItemGenericTriggerHelper>(a1);

            var preCtx = new CItemGenericTriggerHelperItemGenericTriggerHelperTouchPreContext { SchemaObject = schemaObject };
            InvokeCItemGenericTriggerHelperItemGenericTriggerHelperTouchPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CItemGenericTriggerHelperItemGenericTriggerHelperTouchPostContext { SchemaObject = schemaObject };
            InvokeCItemGenericTriggerHelperItemGenericTriggerHelperTouchPost(ref postCtx);
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

    internal static void InvokeCItemGenericTriggerHelperItemGenericTriggerHelperTouch(nint a1)
    {
        CItemGenericTriggerHelperItemGenericTriggerHelperTouchGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCItemGenericTriggerHelperItemGenericTriggerHelperTouchPre(ref CItemGenericTriggerHelperItemGenericTriggerHelperTouchPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCItemGenericTriggerHelperItemGenericTriggerHelperTouchPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCItemGenericTriggerHelperItemGenericTriggerHelperTouchPost(ref CItemGenericTriggerHelperItemGenericTriggerHelperTouchPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCItemGenericTriggerHelperItemGenericTriggerHelperTouchPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CItemGenericTriggerHelperItemGenericTriggerHelperTouchHook : ICItemGenericTriggerHelperItemGenericTriggerHelperTouchHook
{
    private event OnCItemGenericTriggerHelperItemGenericTriggerHelperTouchPreDelegate? _Pre;
    private event OnCItemGenericTriggerHelperItemGenericTriggerHelperTouchPostDelegate? _Post;

    public event OnCItemGenericTriggerHelperItemGenericTriggerHelperTouchPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CItemGenericTriggerHelperItemGenericTriggerHelperTouch);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemGenericTriggerHelperItemGenericTriggerHelperTouch);
            }
        }
    }

    public event OnCItemGenericTriggerHelperItemGenericTriggerHelperTouchPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CItemGenericTriggerHelperItemGenericTriggerHelperTouch);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemGenericTriggerHelperItemGenericTriggerHelperTouch);
            }
        }
    }

    public void InvokePre(ref CItemGenericTriggerHelperItemGenericTriggerHelperTouchPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CItemGenericTriggerHelperItemGenericTriggerHelperTouchPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemGenericTriggerHelperItemGenericTriggerHelperTouch);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemGenericTriggerHelperItemGenericTriggerHelperTouch);
        }
    }

    public void Invoke(CItemGenericTriggerHelper schemaObject) => DatamapHooksPublisher.InvokeCItemGenericTriggerHelperItemGenericTriggerHelperTouch(schemaObject.Address);
}