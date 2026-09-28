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
    private delegate void CPhysicalButtonButtonActivateDelegate(nint a1);

    private static IUnmanagedFunction<CPhysicalButtonButtonActivateDelegate>? CPhysicalButtonButtonActivateUnmanagedFunction;
    private static Guid CPhysicalButtonButtonActivateHookGuid;

    private static IUnmanagedFunction<CPhysicalButtonButtonActivateDelegate> CPhysicalButtonButtonActivateGetUnmanagedFunction()
    {
        if (CPhysicalButtonButtonActivateUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPhysicalButton", "CPhysicalButtonButtonActivate");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPhysicalButton::CPhysicalButtonButtonActivate.");
            }
            CPhysicalButtonButtonActivateUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPhysicalButtonButtonActivateDelegate>(address);
        }
        return CPhysicalButtonButtonActivateUnmanagedFunction;
    }

    internal static Guid HookCPhysicalButtonButtonActivate()
    {
        CPhysicalButtonButtonActivateHookGuid = CPhysicalButtonButtonActivateGetUnmanagedFunction().AddHook(next => (a1) => CPhysicalButtonButtonActivatePipeline(a1, () => next()(a1)));
        return CPhysicalButtonButtonActivateHookGuid;
    }

    internal static Guid UnhookCPhysicalButtonButtonActivate()
    {
        CPhysicalButtonButtonActivateGetUnmanagedFunction().RemoveHook(CPhysicalButtonButtonActivateHookGuid);
        return Guid.Empty;
    }

    private static void CPhysicalButtonButtonActivatePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPhysicalButton>(a1);

            var preCtx = new CPhysicalButtonButtonActivatePreContext { SchemaObject = schemaObject };
            InvokeCPhysicalButtonButtonActivatePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPhysicalButtonButtonActivatePostContext { SchemaObject = schemaObject };
            InvokeCPhysicalButtonButtonActivatePost(ref postCtx);
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

    internal static void InvokeCPhysicalButtonButtonActivate(nint a1)
    {
        CPhysicalButtonButtonActivateGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPhysicalButtonButtonActivatePre(ref CPhysicalButtonButtonActivatePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicalButtonButtonActivatePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPhysicalButtonButtonActivatePost(ref CPhysicalButtonButtonActivatePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicalButtonButtonActivatePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPhysicalButtonButtonActivateHook : ICPhysicalButtonButtonActivateHook
{
    private event OnCPhysicalButtonButtonActivatePreDelegate? _Pre;
    private event OnCPhysicalButtonButtonActivatePostDelegate? _Post;

    public event OnCPhysicalButtonButtonActivatePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicalButtonButtonActivate);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonButtonActivate);
            }
        }
    }

    public event OnCPhysicalButtonButtonActivatePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicalButtonButtonActivate);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonButtonActivate);
            }
        }
    }

    public void InvokePre(ref CPhysicalButtonButtonActivatePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPhysicalButtonButtonActivatePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonButtonActivate);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonButtonActivate);
        }
    }

    public void Invoke(CPhysicalButton schemaObject) => DatamapHooksPublisher.InvokeCPhysicalButtonButtonActivate(schemaObject.Address);
}