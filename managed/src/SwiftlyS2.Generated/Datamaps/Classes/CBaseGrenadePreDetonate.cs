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
    private delegate void CBaseGrenadePreDetonateDelegate(nint a1);

    private static IUnmanagedFunction<CBaseGrenadePreDetonateDelegate>? CBaseGrenadePreDetonateUnmanagedFunction;
    private static Guid CBaseGrenadePreDetonateHookGuid;

    private static IUnmanagedFunction<CBaseGrenadePreDetonateDelegate> CBaseGrenadePreDetonateGetUnmanagedFunction()
    {
        if (CBaseGrenadePreDetonateUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseGrenade", "CBaseGrenadePreDetonate");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseGrenade::CBaseGrenadePreDetonate.");
            }
            CBaseGrenadePreDetonateUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseGrenadePreDetonateDelegate>(address);
        }
        return CBaseGrenadePreDetonateUnmanagedFunction;
    }

    internal static Guid HookCBaseGrenadePreDetonate()
    {
        CBaseGrenadePreDetonateHookGuid = CBaseGrenadePreDetonateGetUnmanagedFunction().AddHook(next => (a1) => CBaseGrenadePreDetonatePipeline(a1, () => next()(a1)));
        return CBaseGrenadePreDetonateHookGuid;
    }

    internal static Guid UnhookCBaseGrenadePreDetonate()
    {
        CBaseGrenadePreDetonateGetUnmanagedFunction().RemoveHook(CBaseGrenadePreDetonateHookGuid);
        return Guid.Empty;
    }

    private static void CBaseGrenadePreDetonatePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseGrenade>(a1);

            var preCtx = new CBaseGrenadePreDetonatePreContext { SchemaObject = schemaObject };
            InvokeCBaseGrenadePreDetonatePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseGrenadePreDetonatePostContext { SchemaObject = schemaObject };
            InvokeCBaseGrenadePreDetonatePost(ref postCtx);
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

    internal static void InvokeCBaseGrenadePreDetonate(nint a1)
    {
        CBaseGrenadePreDetonateGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseGrenadePreDetonatePre(ref CBaseGrenadePreDetonatePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseGrenadePreDetonatePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseGrenadePreDetonatePost(ref CBaseGrenadePreDetonatePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseGrenadePreDetonatePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseGrenadePreDetonateHook : ICBaseGrenadePreDetonateHook
{
    private event OnCBaseGrenadePreDetonatePreDelegate? _Pre;
    private event OnCBaseGrenadePreDetonatePostDelegate? _Post;

    public event OnCBaseGrenadePreDetonatePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseGrenadePreDetonate);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadePreDetonate);
            }
        }
    }

    public event OnCBaseGrenadePreDetonatePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseGrenadePreDetonate);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadePreDetonate);
            }
        }
    }

    public void InvokePre(ref CBaseGrenadePreDetonatePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseGrenadePreDetonatePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadePreDetonate);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadePreDetonate);
        }
    }

    public void Invoke(CBaseGrenade schemaObject) => DatamapHooksPublisher.InvokeCBaseGrenadePreDetonate(schemaObject.Address);
}