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
    private delegate void CBaseGrenadeDetonateDelegate(nint a1);

    private static IUnmanagedFunction<CBaseGrenadeDetonateDelegate>? CBaseGrenadeDetonateUnmanagedFunction;
    private static Guid CBaseGrenadeDetonateHookGuid;

    private static IUnmanagedFunction<CBaseGrenadeDetonateDelegate> CBaseGrenadeDetonateGetUnmanagedFunction()
    {
        if (CBaseGrenadeDetonateUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseGrenade", "CBaseGrenadeDetonate");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseGrenade::CBaseGrenadeDetonate.");
            }
            CBaseGrenadeDetonateUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseGrenadeDetonateDelegate>(address);
        }
        return CBaseGrenadeDetonateUnmanagedFunction;
    }

    internal static Guid HookCBaseGrenadeDetonate()
    {
        CBaseGrenadeDetonateHookGuid = CBaseGrenadeDetonateGetUnmanagedFunction().AddHook(next => (a1) => CBaseGrenadeDetonatePipeline(a1, () => next()(a1)));
        return CBaseGrenadeDetonateHookGuid;
    }

    internal static Guid UnhookCBaseGrenadeDetonate()
    {
        CBaseGrenadeDetonateGetUnmanagedFunction().RemoveHook(CBaseGrenadeDetonateHookGuid);
        return Guid.Empty;
    }

    private static void CBaseGrenadeDetonatePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseGrenade>(a1);

            var preCtx = new CBaseGrenadeDetonatePreContext { SchemaObject = schemaObject };
            InvokeCBaseGrenadeDetonatePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseGrenadeDetonatePostContext { SchemaObject = schemaObject };
            InvokeCBaseGrenadeDetonatePost(ref postCtx);
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

    internal static void InvokeCBaseGrenadeDetonate(nint a1)
    {
        CBaseGrenadeDetonateGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseGrenadeDetonatePre(ref CBaseGrenadeDetonatePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseGrenadeDetonatePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseGrenadeDetonatePost(ref CBaseGrenadeDetonatePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseGrenadeDetonatePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseGrenadeDetonateHook : ICBaseGrenadeDetonateHook
{
    private event OnCBaseGrenadeDetonatePreDelegate? _Pre;
    private event OnCBaseGrenadeDetonatePostDelegate? _Post;

    public event OnCBaseGrenadeDetonatePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseGrenadeDetonate);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeDetonate);
            }
        }
    }

    public event OnCBaseGrenadeDetonatePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseGrenadeDetonate);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeDetonate);
            }
        }
    }

    public void InvokePre(ref CBaseGrenadeDetonatePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseGrenadeDetonatePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeDetonate);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeDetonate);
        }
    }

    public void Invoke(CBaseGrenade schemaObject) => DatamapHooksPublisher.InvokeCBaseGrenadeDetonate(schemaObject.Address);
}