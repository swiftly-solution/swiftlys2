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
    private delegate void CItemGenericItemGenericTouchDelegate(nint a1);

    private static IUnmanagedFunction<CItemGenericItemGenericTouchDelegate>? CItemGenericItemGenericTouchUnmanagedFunction;
    private static Guid CItemGenericItemGenericTouchHookGuid;

    private static IUnmanagedFunction<CItemGenericItemGenericTouchDelegate> CItemGenericItemGenericTouchGetUnmanagedFunction()
    {
        if (CItemGenericItemGenericTouchUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CItemGeneric", "CItemGenericItemGenericTouch");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CItemGeneric::CItemGenericItemGenericTouch.");
            }
            CItemGenericItemGenericTouchUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CItemGenericItemGenericTouchDelegate>(address);
        }
        return CItemGenericItemGenericTouchUnmanagedFunction;
    }

    internal static Guid HookCItemGenericItemGenericTouch()
    {
        CItemGenericItemGenericTouchHookGuid = CItemGenericItemGenericTouchGetUnmanagedFunction().AddHook(next => (a1) => CItemGenericItemGenericTouchPipeline(a1, () => next()(a1)));
        return CItemGenericItemGenericTouchHookGuid;
    }

    internal static Guid UnhookCItemGenericItemGenericTouch()
    {
        CItemGenericItemGenericTouchGetUnmanagedFunction().RemoveHook(CItemGenericItemGenericTouchHookGuid);
        return Guid.Empty;
    }

    private static void CItemGenericItemGenericTouchPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CItemGeneric>(a1);

            var preCtx = new CItemGenericItemGenericTouchPreContext { SchemaObject = schemaObject };
            InvokeCItemGenericItemGenericTouchPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CItemGenericItemGenericTouchPostContext { SchemaObject = schemaObject };
            InvokeCItemGenericItemGenericTouchPost(ref postCtx);
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

    internal static void InvokeCItemGenericItemGenericTouch(nint a1)
    {
        CItemGenericItemGenericTouchGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCItemGenericItemGenericTouchPre(ref CItemGenericItemGenericTouchPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCItemGenericItemGenericTouchPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCItemGenericItemGenericTouchPost(ref CItemGenericItemGenericTouchPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCItemGenericItemGenericTouchPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CItemGenericItemGenericTouchHook : ICItemGenericItemGenericTouchHook
{
    private event OnCItemGenericItemGenericTouchPreDelegate? _Pre;
    private event OnCItemGenericItemGenericTouchPostDelegate? _Post;

    public event OnCItemGenericItemGenericTouchPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CItemGenericItemGenericTouch);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemGenericItemGenericTouch);
            }
        }
    }

    public event OnCItemGenericItemGenericTouchPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CItemGenericItemGenericTouch);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemGenericItemGenericTouch);
            }
        }
    }

    public void InvokePre(ref CItemGenericItemGenericTouchPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CItemGenericItemGenericTouchPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemGenericItemGenericTouch);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemGenericItemGenericTouch);
        }
    }

    public void Invoke(CItemGeneric schemaObject) => DatamapHooksPublisher.InvokeCItemGenericItemGenericTouch(schemaObject.Address);
}