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
    private delegate void CPhysicalButtonTriggerAndWaitDelegate(nint a1);

    private static IUnmanagedFunction<CPhysicalButtonTriggerAndWaitDelegate>? CPhysicalButtonTriggerAndWaitUnmanagedFunction;
    private static Guid CPhysicalButtonTriggerAndWaitHookGuid;

    private static IUnmanagedFunction<CPhysicalButtonTriggerAndWaitDelegate> CPhysicalButtonTriggerAndWaitGetUnmanagedFunction()
    {
        if (CPhysicalButtonTriggerAndWaitUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPhysicalButton", "CPhysicalButtonTriggerAndWait");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPhysicalButton::CPhysicalButtonTriggerAndWait.");
            }
            CPhysicalButtonTriggerAndWaitUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPhysicalButtonTriggerAndWaitDelegate>(address);
        }
        return CPhysicalButtonTriggerAndWaitUnmanagedFunction;
    }

    internal static Guid HookCPhysicalButtonTriggerAndWait()
    {
        CPhysicalButtonTriggerAndWaitHookGuid = CPhysicalButtonTriggerAndWaitGetUnmanagedFunction().AddHook(next => (a1) => CPhysicalButtonTriggerAndWaitPipeline(a1, () => next()(a1)));
        return CPhysicalButtonTriggerAndWaitHookGuid;
    }

    internal static Guid UnhookCPhysicalButtonTriggerAndWait()
    {
        CPhysicalButtonTriggerAndWaitGetUnmanagedFunction().RemoveHook(CPhysicalButtonTriggerAndWaitHookGuid);
        return Guid.Empty;
    }

    private static void CPhysicalButtonTriggerAndWaitPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPhysicalButton>(a1);

            var preCtx = new CPhysicalButtonTriggerAndWaitPreContext { SchemaObject = schemaObject };
            InvokeCPhysicalButtonTriggerAndWaitPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPhysicalButtonTriggerAndWaitPostContext { SchemaObject = schemaObject };
            InvokeCPhysicalButtonTriggerAndWaitPost(ref postCtx);
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

    internal static void InvokeCPhysicalButtonTriggerAndWait(nint a1)
    {
        CPhysicalButtonTriggerAndWaitGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPhysicalButtonTriggerAndWaitPre(ref CPhysicalButtonTriggerAndWaitPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicalButtonTriggerAndWaitPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPhysicalButtonTriggerAndWaitPost(ref CPhysicalButtonTriggerAndWaitPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicalButtonTriggerAndWaitPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPhysicalButtonTriggerAndWaitHook : ICPhysicalButtonTriggerAndWaitHook
{
    private event OnCPhysicalButtonTriggerAndWaitPreDelegate? _Pre;
    private event OnCPhysicalButtonTriggerAndWaitPostDelegate? _Post;

    public event OnCPhysicalButtonTriggerAndWaitPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicalButtonTriggerAndWait);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonTriggerAndWait);
            }
        }
    }

    public event OnCPhysicalButtonTriggerAndWaitPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicalButtonTriggerAndWait);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonTriggerAndWait);
            }
        }
    }

    public void InvokePre(ref CPhysicalButtonTriggerAndWaitPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPhysicalButtonTriggerAndWaitPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonTriggerAndWait);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonTriggerAndWait);
        }
    }

    public void Invoke(CPhysicalButton schemaObject) => DatamapHooksPublisher.InvokeCPhysicalButtonTriggerAndWait(schemaObject.Address);
}