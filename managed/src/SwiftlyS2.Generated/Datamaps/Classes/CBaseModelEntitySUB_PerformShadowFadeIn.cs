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
    private delegate void CBaseModelEntitySUB_PerformShadowFadeInDelegate(nint a1);

    private static IUnmanagedFunction<CBaseModelEntitySUB_PerformShadowFadeInDelegate>? CBaseModelEntitySUB_PerformShadowFadeInUnmanagedFunction;
    private static Guid CBaseModelEntitySUB_PerformShadowFadeInHookGuid;

    private static IUnmanagedFunction<CBaseModelEntitySUB_PerformShadowFadeInDelegate> CBaseModelEntitySUB_PerformShadowFadeInGetUnmanagedFunction()
    {
        if (CBaseModelEntitySUB_PerformShadowFadeInUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseModelEntity", "CBaseModelEntitySUB_PerformShadowFadeIn");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseModelEntity::CBaseModelEntitySUB_PerformShadowFadeIn.");
            }
            CBaseModelEntitySUB_PerformShadowFadeInUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseModelEntitySUB_PerformShadowFadeInDelegate>(address);
        }
        return CBaseModelEntitySUB_PerformShadowFadeInUnmanagedFunction;
    }

    internal static Guid HookCBaseModelEntitySUB_PerformShadowFadeIn()
    {
        CBaseModelEntitySUB_PerformShadowFadeInHookGuid = CBaseModelEntitySUB_PerformShadowFadeInGetUnmanagedFunction().AddHook(next => (a1) => CBaseModelEntitySUB_PerformShadowFadeInPipeline(a1, () => next()(a1)));
        return CBaseModelEntitySUB_PerformShadowFadeInHookGuid;
    }

    internal static Guid UnhookCBaseModelEntitySUB_PerformShadowFadeIn()
    {
        CBaseModelEntitySUB_PerformShadowFadeInGetUnmanagedFunction().RemoveHook(CBaseModelEntitySUB_PerformShadowFadeInHookGuid);
        return Guid.Empty;
    }

    private static void CBaseModelEntitySUB_PerformShadowFadeInPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseModelEntity>(a1);

            var preCtx = new CBaseModelEntitySUB_PerformShadowFadeInPreContext { SchemaObject = schemaObject };
            InvokeCBaseModelEntitySUB_PerformShadowFadeInPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseModelEntitySUB_PerformShadowFadeInPostContext { SchemaObject = schemaObject };
            InvokeCBaseModelEntitySUB_PerformShadowFadeInPost(ref postCtx);
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

    internal static void InvokeCBaseModelEntitySUB_PerformShadowFadeIn(nint a1)
    {
        CBaseModelEntitySUB_PerformShadowFadeInGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseModelEntitySUB_PerformShadowFadeInPre(ref CBaseModelEntitySUB_PerformShadowFadeInPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseModelEntitySUB_PerformShadowFadeInPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseModelEntitySUB_PerformShadowFadeInPost(ref CBaseModelEntitySUB_PerformShadowFadeInPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseModelEntitySUB_PerformShadowFadeInPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseModelEntitySUB_PerformShadowFadeInHook : ICBaseModelEntitySUB_PerformShadowFadeInHook
{
    private event OnCBaseModelEntitySUB_PerformShadowFadeInPreDelegate? _Pre;
    private event OnCBaseModelEntitySUB_PerformShadowFadeInPostDelegate? _Post;

    public event OnCBaseModelEntitySUB_PerformShadowFadeInPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseModelEntitySUB_PerformShadowFadeIn);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_PerformShadowFadeIn);
            }
        }
    }

    public event OnCBaseModelEntitySUB_PerformShadowFadeInPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseModelEntitySUB_PerformShadowFadeIn);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_PerformShadowFadeIn);
            }
        }
    }

    public void InvokePre(ref CBaseModelEntitySUB_PerformShadowFadeInPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseModelEntitySUB_PerformShadowFadeInPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_PerformShadowFadeIn);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_PerformShadowFadeIn);
        }
    }

    public void Invoke(CBaseModelEntity schemaObject) => DatamapHooksPublisher.InvokeCBaseModelEntitySUB_PerformShadowFadeIn(schemaObject.Address);
}