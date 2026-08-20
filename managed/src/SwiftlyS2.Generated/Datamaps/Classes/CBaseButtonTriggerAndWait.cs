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
    private delegate void CBaseButtonTriggerAndWaitDelegate(nint a1);

    private static IUnmanagedFunction<CBaseButtonTriggerAndWaitDelegate>? CBaseButtonTriggerAndWaitUnmanagedFunction;
    private static Guid CBaseButtonTriggerAndWaitHookGuid;

    private static IUnmanagedFunction<CBaseButtonTriggerAndWaitDelegate> CBaseButtonTriggerAndWaitGetUnmanagedFunction()
    {
        if (CBaseButtonTriggerAndWaitUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseButton", "CBaseButtonTriggerAndWait");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseButton::CBaseButtonTriggerAndWait.");
            }
            CBaseButtonTriggerAndWaitUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseButtonTriggerAndWaitDelegate>(address);
        }
        return CBaseButtonTriggerAndWaitUnmanagedFunction;
    }

    internal static Guid HookCBaseButtonTriggerAndWait()
    {
        CBaseButtonTriggerAndWaitHookGuid = CBaseButtonTriggerAndWaitGetUnmanagedFunction().AddHook(next => (a1) => CBaseButtonTriggerAndWaitPipeline(a1, () => next()(a1)));
        return CBaseButtonTriggerAndWaitHookGuid;
    }

    internal static Guid UnhookCBaseButtonTriggerAndWait()
    {
        CBaseButtonTriggerAndWaitGetUnmanagedFunction().RemoveHook(CBaseButtonTriggerAndWaitHookGuid);
        return Guid.Empty;
    }

    private static void CBaseButtonTriggerAndWaitPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseButton>(a1);

            var preCtx = new CBaseButtonTriggerAndWaitPreContext { SchemaObject = schemaObject };
            InvokeCBaseButtonTriggerAndWaitPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseButtonTriggerAndWaitPostContext { SchemaObject = schemaObject };
            InvokeCBaseButtonTriggerAndWaitPost(ref postCtx);
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

    internal static void InvokeCBaseButtonTriggerAndWait(nint a1)
    {
        CBaseButtonTriggerAndWaitGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseButtonTriggerAndWaitPre(ref CBaseButtonTriggerAndWaitPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseButtonTriggerAndWaitPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseButtonTriggerAndWaitPost(ref CBaseButtonTriggerAndWaitPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseButtonTriggerAndWaitPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseButtonTriggerAndWaitHook : ICBaseButtonTriggerAndWaitHook
{
    private event OnCBaseButtonTriggerAndWaitPreDelegate? _Pre;
    private event OnCBaseButtonTriggerAndWaitPostDelegate? _Post;

    public event OnCBaseButtonTriggerAndWaitPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseButtonTriggerAndWait);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonTriggerAndWait);
            }
        }
    }

    public event OnCBaseButtonTriggerAndWaitPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseButtonTriggerAndWait);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonTriggerAndWait);
            }
        }
    }

    public void InvokePre(ref CBaseButtonTriggerAndWaitPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseButtonTriggerAndWaitPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonTriggerAndWait);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonTriggerAndWait);
        }
    }

    public void Invoke(CBaseButton schemaObject) => DatamapHooksPublisher.InvokeCBaseButtonTriggerAndWait(schemaObject.Address);
}