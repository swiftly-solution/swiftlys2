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
    private delegate void CBaseGrenadeDetonateUseDelegate(nint a1);

    private static IUnmanagedFunction<CBaseGrenadeDetonateUseDelegate>? CBaseGrenadeDetonateUseUnmanagedFunction;
    private static Guid CBaseGrenadeDetonateUseHookGuid;

    private static IUnmanagedFunction<CBaseGrenadeDetonateUseDelegate> CBaseGrenadeDetonateUseGetUnmanagedFunction()
    {
        if (CBaseGrenadeDetonateUseUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseGrenade", "CBaseGrenadeDetonateUse");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseGrenade::CBaseGrenadeDetonateUse.");
            }
            CBaseGrenadeDetonateUseUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseGrenadeDetonateUseDelegate>(address);
        }
        return CBaseGrenadeDetonateUseUnmanagedFunction;
    }

    internal static Guid HookCBaseGrenadeDetonateUse()
    {
        CBaseGrenadeDetonateUseHookGuid = CBaseGrenadeDetonateUseGetUnmanagedFunction().AddHook(next => (a1) => CBaseGrenadeDetonateUsePipeline(a1, () => next()(a1)));
        return CBaseGrenadeDetonateUseHookGuid;
    }

    internal static Guid UnhookCBaseGrenadeDetonateUse()
    {
        CBaseGrenadeDetonateUseGetUnmanagedFunction().RemoveHook(CBaseGrenadeDetonateUseHookGuid);
        return Guid.Empty;
    }

    private static void CBaseGrenadeDetonateUsePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseGrenade>(a1);

            var preCtx = new CBaseGrenadeDetonateUsePreContext { SchemaObject = schemaObject };
            InvokeCBaseGrenadeDetonateUsePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseGrenadeDetonateUsePostContext { SchemaObject = schemaObject };
            InvokeCBaseGrenadeDetonateUsePost(ref postCtx);
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

    internal static void InvokeCBaseGrenadeDetonateUse(nint a1)
    {
        CBaseGrenadeDetonateUseGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseGrenadeDetonateUsePre(ref CBaseGrenadeDetonateUsePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseGrenadeDetonateUsePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseGrenadeDetonateUsePost(ref CBaseGrenadeDetonateUsePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseGrenadeDetonateUsePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseGrenadeDetonateUseHook : ICBaseGrenadeDetonateUseHook
{
    private event OnCBaseGrenadeDetonateUsePreDelegate? _Pre;
    private event OnCBaseGrenadeDetonateUsePostDelegate? _Post;

    public event OnCBaseGrenadeDetonateUsePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseGrenadeDetonateUse);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeDetonateUse);
            }
        }
    }

    public event OnCBaseGrenadeDetonateUsePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseGrenadeDetonateUse);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeDetonateUse);
            }
        }
    }

    public void InvokePre(ref CBaseGrenadeDetonateUsePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseGrenadeDetonateUsePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeDetonateUse);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeDetonateUse);
        }
    }

    public void Invoke(CBaseGrenade schemaObject) => DatamapHooksPublisher.InvokeCBaseGrenadeDetonateUse(schemaObject.Address);
}