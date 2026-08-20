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
    private delegate void CItemDefuserDefuserTouchDelegate(nint a1);

    private static IUnmanagedFunction<CItemDefuserDefuserTouchDelegate>? CItemDefuserDefuserTouchUnmanagedFunction;
    private static Guid CItemDefuserDefuserTouchHookGuid;

    private static IUnmanagedFunction<CItemDefuserDefuserTouchDelegate> CItemDefuserDefuserTouchGetUnmanagedFunction()
    {
        if (CItemDefuserDefuserTouchUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CItemDefuser", "CItemDefuserDefuserTouch");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CItemDefuser::CItemDefuserDefuserTouch.");
            }
            CItemDefuserDefuserTouchUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CItemDefuserDefuserTouchDelegate>(address);
        }
        return CItemDefuserDefuserTouchUnmanagedFunction;
    }

    internal static Guid HookCItemDefuserDefuserTouch()
    {
        CItemDefuserDefuserTouchHookGuid = CItemDefuserDefuserTouchGetUnmanagedFunction().AddHook(next => (a1) => CItemDefuserDefuserTouchPipeline(a1, () => next()(a1)));
        return CItemDefuserDefuserTouchHookGuid;
    }

    internal static Guid UnhookCItemDefuserDefuserTouch()
    {
        CItemDefuserDefuserTouchGetUnmanagedFunction().RemoveHook(CItemDefuserDefuserTouchHookGuid);
        return Guid.Empty;
    }

    private static void CItemDefuserDefuserTouchPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CItemDefuser>(a1);

            var preCtx = new CItemDefuserDefuserTouchPreContext { SchemaObject = schemaObject };
            InvokeCItemDefuserDefuserTouchPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CItemDefuserDefuserTouchPostContext { SchemaObject = schemaObject };
            InvokeCItemDefuserDefuserTouchPost(ref postCtx);
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

    internal static void InvokeCItemDefuserDefuserTouch(nint a1)
    {
        CItemDefuserDefuserTouchGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCItemDefuserDefuserTouchPre(ref CItemDefuserDefuserTouchPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCItemDefuserDefuserTouchPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCItemDefuserDefuserTouchPost(ref CItemDefuserDefuserTouchPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCItemDefuserDefuserTouchPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CItemDefuserDefuserTouchHook : ICItemDefuserDefuserTouchHook
{
    private event OnCItemDefuserDefuserTouchPreDelegate? _Pre;
    private event OnCItemDefuserDefuserTouchPostDelegate? _Post;

    public event OnCItemDefuserDefuserTouchPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CItemDefuserDefuserTouch);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemDefuserDefuserTouch);
            }
        }
    }

    public event OnCItemDefuserDefuserTouchPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CItemDefuserDefuserTouch);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemDefuserDefuserTouch);
            }
        }
    }

    public void InvokePre(ref CItemDefuserDefuserTouchPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CItemDefuserDefuserTouchPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemDefuserDefuserTouch);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CItemDefuserDefuserTouch);
        }
    }

    public void Invoke(CItemDefuser schemaObject) => DatamapHooksPublisher.InvokeCItemDefuserDefuserTouch(schemaObject.Address);
}