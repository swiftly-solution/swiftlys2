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
    private delegate void CItemItemTouchDelegate(nint a1);

    private static IUnmanagedFunction<CItemItemTouchDelegate>? CItemItemTouchUnmanagedFunction;
    private static Guid CItemItemTouchHookGuid;

    private static IUnmanagedFunction<CItemItemTouchDelegate> CItemItemTouchGetUnmanagedFunction()
    {
        if (CItemItemTouchUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CItem", "CItemItemTouch");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CItem::CItemItemTouch.");
            }
            CItemItemTouchUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CItemItemTouchDelegate>(address);
        }
        return CItemItemTouchUnmanagedFunction;
    }

    internal static Guid HookCItemItemTouch()
    {
        CItemItemTouchHookGuid = CItemItemTouchGetUnmanagedFunction().AddHook(next => (a1) => CItemItemTouchPipeline(a1, () => next()(a1)));
        return CItemItemTouchHookGuid;
    }

    internal static Guid UnhookCItemItemTouch()
    {
        CItemItemTouchGetUnmanagedFunction().RemoveHook(CItemItemTouchHookGuid);
        return Guid.Empty;
    }

    private static void CItemItemTouchPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CItem>(a1);

            var preCtx = new CItemItemTouchPreContext { SchemaObject = schemaObject };
            InvokeCItemItemTouchPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CItemItemTouchPostContext { SchemaObject = schemaObject };
            InvokeCItemItemTouchPost(ref postCtx);
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

    internal static void InvokeCItemItemTouch(nint a1)
    {
        CItemItemTouchGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCItemItemTouchPre(ref CItemItemTouchPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCItemItemTouchPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCItemItemTouchPost(ref CItemItemTouchPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCItemItemTouchPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CItemItemTouchHook : ICItemItemTouchHook
{
    private event OnCItemItemTouchPreDelegate? _Pre;
    private event OnCItemItemTouchPostDelegate? _Post;

    public event OnCItemItemTouchPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CItemItemTouch);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemItemTouch);
            }
        }
    }

    public event OnCItemItemTouchPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CItemItemTouch);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemItemTouch);
            }
        }
    }

    public void InvokePre(ref CItemItemTouchPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CItemItemTouchPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemItemTouch);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemItemTouch);
        }
    }

    public void Invoke(CItem schemaObject) => DatamapHooksPublisher.InvokeCItemItemTouch(schemaObject.Address);
}