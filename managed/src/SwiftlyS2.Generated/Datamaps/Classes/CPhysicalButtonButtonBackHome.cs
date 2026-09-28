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
    private delegate void CPhysicalButtonButtonBackHomeDelegate(nint a1);

    private static IUnmanagedFunction<CPhysicalButtonButtonBackHomeDelegate>? CPhysicalButtonButtonBackHomeUnmanagedFunction;
    private static Guid CPhysicalButtonButtonBackHomeHookGuid;

    private static IUnmanagedFunction<CPhysicalButtonButtonBackHomeDelegate> CPhysicalButtonButtonBackHomeGetUnmanagedFunction()
    {
        if (CPhysicalButtonButtonBackHomeUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPhysicalButton", "CPhysicalButtonButtonBackHome");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPhysicalButton::CPhysicalButtonButtonBackHome.");
            }
            CPhysicalButtonButtonBackHomeUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPhysicalButtonButtonBackHomeDelegate>(address);
        }
        return CPhysicalButtonButtonBackHomeUnmanagedFunction;
    }

    internal static Guid HookCPhysicalButtonButtonBackHome()
    {
        CPhysicalButtonButtonBackHomeHookGuid = CPhysicalButtonButtonBackHomeGetUnmanagedFunction().AddHook(next => (a1) => CPhysicalButtonButtonBackHomePipeline(a1, () => next()(a1)));
        return CPhysicalButtonButtonBackHomeHookGuid;
    }

    internal static Guid UnhookCPhysicalButtonButtonBackHome()
    {
        CPhysicalButtonButtonBackHomeGetUnmanagedFunction().RemoveHook(CPhysicalButtonButtonBackHomeHookGuid);
        return Guid.Empty;
    }

    private static void CPhysicalButtonButtonBackHomePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPhysicalButton>(a1);

            var preCtx = new CPhysicalButtonButtonBackHomePreContext { SchemaObject = schemaObject };
            InvokeCPhysicalButtonButtonBackHomePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPhysicalButtonButtonBackHomePostContext { SchemaObject = schemaObject };
            InvokeCPhysicalButtonButtonBackHomePost(ref postCtx);
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

    internal static void InvokeCPhysicalButtonButtonBackHome(nint a1)
    {
        CPhysicalButtonButtonBackHomeGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPhysicalButtonButtonBackHomePre(ref CPhysicalButtonButtonBackHomePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicalButtonButtonBackHomePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPhysicalButtonButtonBackHomePost(ref CPhysicalButtonButtonBackHomePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicalButtonButtonBackHomePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPhysicalButtonButtonBackHomeHook : ICPhysicalButtonButtonBackHomeHook
{
    private event OnCPhysicalButtonButtonBackHomePreDelegate? _Pre;
    private event OnCPhysicalButtonButtonBackHomePostDelegate? _Post;

    public event OnCPhysicalButtonButtonBackHomePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicalButtonButtonBackHome);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonButtonBackHome);
            }
        }
    }

    public event OnCPhysicalButtonButtonBackHomePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicalButtonButtonBackHome);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonButtonBackHome);
            }
        }
    }

    public void InvokePre(ref CPhysicalButtonButtonBackHomePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPhysicalButtonButtonBackHomePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonButtonBackHome);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicalButtonButtonBackHome);
        }
    }

    public void Invoke(CPhysicalButton schemaObject) => DatamapHooksPublisher.InvokeCPhysicalButtonButtonBackHome(schemaObject.Address);
}