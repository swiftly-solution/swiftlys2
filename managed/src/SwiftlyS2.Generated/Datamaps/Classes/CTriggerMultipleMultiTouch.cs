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
    private delegate void CTriggerMultipleMultiTouchDelegate(nint a1);

    private static IUnmanagedFunction<CTriggerMultipleMultiTouchDelegate>? CTriggerMultipleMultiTouchUnmanagedFunction;
    private static Guid CTriggerMultipleMultiTouchHookGuid;

    private static IUnmanagedFunction<CTriggerMultipleMultiTouchDelegate> CTriggerMultipleMultiTouchGetUnmanagedFunction()
    {
        if (CTriggerMultipleMultiTouchUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CTriggerMultiple", "CTriggerMultipleMultiTouch");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CTriggerMultiple::CTriggerMultipleMultiTouch.");
            }
            CTriggerMultipleMultiTouchUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CTriggerMultipleMultiTouchDelegate>(address);
        }
        return CTriggerMultipleMultiTouchUnmanagedFunction;
    }

    internal static Guid HookCTriggerMultipleMultiTouch()
    {
        CTriggerMultipleMultiTouchHookGuid = CTriggerMultipleMultiTouchGetUnmanagedFunction().AddHook(next => (a1) => CTriggerMultipleMultiTouchPipeline(a1, () => next()(a1)));
        return CTriggerMultipleMultiTouchHookGuid;
    }

    internal static Guid UnhookCTriggerMultipleMultiTouch()
    {
        CTriggerMultipleMultiTouchGetUnmanagedFunction().RemoveHook(CTriggerMultipleMultiTouchHookGuid);
        return Guid.Empty;
    }

    private static void CTriggerMultipleMultiTouchPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CTriggerMultiple>(a1);

            var preCtx = new CTriggerMultipleMultiTouchPreContext { SchemaObject = schemaObject };
            InvokeCTriggerMultipleMultiTouchPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CTriggerMultipleMultiTouchPostContext { SchemaObject = schemaObject };
            InvokeCTriggerMultipleMultiTouchPost(ref postCtx);
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

    internal static void InvokeCTriggerMultipleMultiTouch(nint a1)
    {
        CTriggerMultipleMultiTouchGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCTriggerMultipleMultiTouchPre(ref CTriggerMultipleMultiTouchPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerMultipleMultiTouchPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCTriggerMultipleMultiTouchPost(ref CTriggerMultipleMultiTouchPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCTriggerMultipleMultiTouchPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CTriggerMultipleMultiTouchHook : ICTriggerMultipleMultiTouchHook
{
    private event OnCTriggerMultipleMultiTouchPreDelegate? _Pre;
    private event OnCTriggerMultipleMultiTouchPostDelegate? _Post;

    public event OnCTriggerMultipleMultiTouchPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerMultipleMultiTouch);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerMultipleMultiTouch);
            }
        }
    }

    public event OnCTriggerMultipleMultiTouchPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerMultipleMultiTouch);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerMultipleMultiTouch);
            }
        }
    }

    public void InvokePre(ref CTriggerMultipleMultiTouchPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CTriggerMultipleMultiTouchPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerMultipleMultiTouch);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerMultipleMultiTouch);
        }
    }

    public void Invoke(CTriggerMultiple schemaObject) => DatamapHooksPublisher.InvokeCTriggerMultipleMultiTouch(schemaObject.Address);
}