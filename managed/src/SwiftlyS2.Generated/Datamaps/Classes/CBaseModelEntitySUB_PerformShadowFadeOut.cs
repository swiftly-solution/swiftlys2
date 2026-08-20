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
    private delegate void CBaseModelEntitySUB_PerformShadowFadeOutDelegate(nint a1);

    private static IUnmanagedFunction<CBaseModelEntitySUB_PerformShadowFadeOutDelegate>? CBaseModelEntitySUB_PerformShadowFadeOutUnmanagedFunction;
    private static Guid CBaseModelEntitySUB_PerformShadowFadeOutHookGuid;

    private static IUnmanagedFunction<CBaseModelEntitySUB_PerformShadowFadeOutDelegate> CBaseModelEntitySUB_PerformShadowFadeOutGetUnmanagedFunction()
    {
        if (CBaseModelEntitySUB_PerformShadowFadeOutUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseModelEntity", "CBaseModelEntitySUB_PerformShadowFadeOut");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseModelEntity::CBaseModelEntitySUB_PerformShadowFadeOut.");
            }
            CBaseModelEntitySUB_PerformShadowFadeOutUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseModelEntitySUB_PerformShadowFadeOutDelegate>(address);
        }
        return CBaseModelEntitySUB_PerformShadowFadeOutUnmanagedFunction;
    }

    internal static Guid HookCBaseModelEntitySUB_PerformShadowFadeOut()
    {
        CBaseModelEntitySUB_PerformShadowFadeOutHookGuid = CBaseModelEntitySUB_PerformShadowFadeOutGetUnmanagedFunction().AddHook(next => (a1) => CBaseModelEntitySUB_PerformShadowFadeOutPipeline(a1, () => next()(a1)));
        return CBaseModelEntitySUB_PerformShadowFadeOutHookGuid;
    }

    internal static Guid UnhookCBaseModelEntitySUB_PerformShadowFadeOut()
    {
        CBaseModelEntitySUB_PerformShadowFadeOutGetUnmanagedFunction().RemoveHook(CBaseModelEntitySUB_PerformShadowFadeOutHookGuid);
        return Guid.Empty;
    }

    private static void CBaseModelEntitySUB_PerformShadowFadeOutPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseModelEntity>(a1);

            var preCtx = new CBaseModelEntitySUB_PerformShadowFadeOutPreContext { SchemaObject = schemaObject };
            InvokeCBaseModelEntitySUB_PerformShadowFadeOutPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseModelEntitySUB_PerformShadowFadeOutPostContext { SchemaObject = schemaObject };
            InvokeCBaseModelEntitySUB_PerformShadowFadeOutPost(ref postCtx);
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

    internal static void InvokeCBaseModelEntitySUB_PerformShadowFadeOut(nint a1)
    {
        CBaseModelEntitySUB_PerformShadowFadeOutGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseModelEntitySUB_PerformShadowFadeOutPre(ref CBaseModelEntitySUB_PerformShadowFadeOutPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseModelEntitySUB_PerformShadowFadeOutPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseModelEntitySUB_PerformShadowFadeOutPost(ref CBaseModelEntitySUB_PerformShadowFadeOutPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseModelEntitySUB_PerformShadowFadeOutPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseModelEntitySUB_PerformShadowFadeOutHook : ICBaseModelEntitySUB_PerformShadowFadeOutHook
{
    private event OnCBaseModelEntitySUB_PerformShadowFadeOutPreDelegate? _Pre;
    private event OnCBaseModelEntitySUB_PerformShadowFadeOutPostDelegate? _Post;

    public event OnCBaseModelEntitySUB_PerformShadowFadeOutPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseModelEntitySUB_PerformShadowFadeOut);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_PerformShadowFadeOut);
            }
        }
    }

    public event OnCBaseModelEntitySUB_PerformShadowFadeOutPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseModelEntitySUB_PerformShadowFadeOut);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_PerformShadowFadeOut);
            }
        }
    }

    public void InvokePre(ref CBaseModelEntitySUB_PerformShadowFadeOutPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseModelEntitySUB_PerformShadowFadeOutPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_PerformShadowFadeOut);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_PerformShadowFadeOut);
        }
    }

    public void Invoke(CBaseModelEntity schemaObject) => DatamapHooksPublisher.InvokeCBaseModelEntitySUB_PerformShadowFadeOut(schemaObject.Address);
}