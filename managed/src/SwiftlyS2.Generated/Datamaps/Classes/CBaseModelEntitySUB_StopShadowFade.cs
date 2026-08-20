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
    private delegate void CBaseModelEntitySUB_StopShadowFadeDelegate(nint a1);

    private static IUnmanagedFunction<CBaseModelEntitySUB_StopShadowFadeDelegate>? CBaseModelEntitySUB_StopShadowFadeUnmanagedFunction;
    private static Guid CBaseModelEntitySUB_StopShadowFadeHookGuid;

    private static IUnmanagedFunction<CBaseModelEntitySUB_StopShadowFadeDelegate> CBaseModelEntitySUB_StopShadowFadeGetUnmanagedFunction()
    {
        if (CBaseModelEntitySUB_StopShadowFadeUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseModelEntity", "CBaseModelEntitySUB_StopShadowFade");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseModelEntity::CBaseModelEntitySUB_StopShadowFade.");
            }
            CBaseModelEntitySUB_StopShadowFadeUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseModelEntitySUB_StopShadowFadeDelegate>(address);
        }
        return CBaseModelEntitySUB_StopShadowFadeUnmanagedFunction;
    }

    internal static Guid HookCBaseModelEntitySUB_StopShadowFade()
    {
        CBaseModelEntitySUB_StopShadowFadeHookGuid = CBaseModelEntitySUB_StopShadowFadeGetUnmanagedFunction().AddHook(next => (a1) => CBaseModelEntitySUB_StopShadowFadePipeline(a1, () => next()(a1)));
        return CBaseModelEntitySUB_StopShadowFadeHookGuid;
    }

    internal static Guid UnhookCBaseModelEntitySUB_StopShadowFade()
    {
        CBaseModelEntitySUB_StopShadowFadeGetUnmanagedFunction().RemoveHook(CBaseModelEntitySUB_StopShadowFadeHookGuid);
        return Guid.Empty;
    }

    private static void CBaseModelEntitySUB_StopShadowFadePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseModelEntity>(a1);

            var preCtx = new CBaseModelEntitySUB_StopShadowFadePreContext { SchemaObject = schemaObject };
            InvokeCBaseModelEntitySUB_StopShadowFadePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseModelEntitySUB_StopShadowFadePostContext { SchemaObject = schemaObject };
            InvokeCBaseModelEntitySUB_StopShadowFadePost(ref postCtx);
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

    internal static void InvokeCBaseModelEntitySUB_StopShadowFade(nint a1)
    {
        CBaseModelEntitySUB_StopShadowFadeGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseModelEntitySUB_StopShadowFadePre(ref CBaseModelEntitySUB_StopShadowFadePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseModelEntitySUB_StopShadowFadePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseModelEntitySUB_StopShadowFadePost(ref CBaseModelEntitySUB_StopShadowFadePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseModelEntitySUB_StopShadowFadePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseModelEntitySUB_StopShadowFadeHook : ICBaseModelEntitySUB_StopShadowFadeHook
{
    private event OnCBaseModelEntitySUB_StopShadowFadePreDelegate? _Pre;
    private event OnCBaseModelEntitySUB_StopShadowFadePostDelegate? _Post;

    public event OnCBaseModelEntitySUB_StopShadowFadePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseModelEntitySUB_StopShadowFade);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_StopShadowFade);
            }
        }
    }

    public event OnCBaseModelEntitySUB_StopShadowFadePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseModelEntitySUB_StopShadowFade);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_StopShadowFade);
            }
        }
    }

    public void InvokePre(ref CBaseModelEntitySUB_StopShadowFadePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseModelEntitySUB_StopShadowFadePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_StopShadowFade);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_StopShadowFade);
        }
    }

    public void Invoke(CBaseModelEntity schemaObject) => DatamapHooksPublisher.InvokeCBaseModelEntitySUB_StopShadowFade(schemaObject.Address);
}