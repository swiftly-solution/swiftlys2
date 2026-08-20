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
    private delegate void CBaseModelEntitySUB_StartShadowFadeInDelegate(nint a1);

    private static IUnmanagedFunction<CBaseModelEntitySUB_StartShadowFadeInDelegate>? CBaseModelEntitySUB_StartShadowFadeInUnmanagedFunction;
    private static Guid CBaseModelEntitySUB_StartShadowFadeInHookGuid;

    private static IUnmanagedFunction<CBaseModelEntitySUB_StartShadowFadeInDelegate> CBaseModelEntitySUB_StartShadowFadeInGetUnmanagedFunction()
    {
        if (CBaseModelEntitySUB_StartShadowFadeInUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseModelEntity", "CBaseModelEntitySUB_StartShadowFadeIn");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseModelEntity::CBaseModelEntitySUB_StartShadowFadeIn.");
            }
            CBaseModelEntitySUB_StartShadowFadeInUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseModelEntitySUB_StartShadowFadeInDelegate>(address);
        }
        return CBaseModelEntitySUB_StartShadowFadeInUnmanagedFunction;
    }

    internal static Guid HookCBaseModelEntitySUB_StartShadowFadeIn()
    {
        CBaseModelEntitySUB_StartShadowFadeInHookGuid = CBaseModelEntitySUB_StartShadowFadeInGetUnmanagedFunction().AddHook(next => (a1) => CBaseModelEntitySUB_StartShadowFadeInPipeline(a1, () => next()(a1)));
        return CBaseModelEntitySUB_StartShadowFadeInHookGuid;
    }

    internal static Guid UnhookCBaseModelEntitySUB_StartShadowFadeIn()
    {
        CBaseModelEntitySUB_StartShadowFadeInGetUnmanagedFunction().RemoveHook(CBaseModelEntitySUB_StartShadowFadeInHookGuid);
        return Guid.Empty;
    }

    private static void CBaseModelEntitySUB_StartShadowFadeInPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseModelEntity>(a1);

            var preCtx = new CBaseModelEntitySUB_StartShadowFadeInPreContext { SchemaObject = schemaObject };
            InvokeCBaseModelEntitySUB_StartShadowFadeInPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseModelEntitySUB_StartShadowFadeInPostContext { SchemaObject = schemaObject };
            InvokeCBaseModelEntitySUB_StartShadowFadeInPost(ref postCtx);
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

    internal static void InvokeCBaseModelEntitySUB_StartShadowFadeIn(nint a1)
    {
        CBaseModelEntitySUB_StartShadowFadeInGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseModelEntitySUB_StartShadowFadeInPre(ref CBaseModelEntitySUB_StartShadowFadeInPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseModelEntitySUB_StartShadowFadeInPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseModelEntitySUB_StartShadowFadeInPost(ref CBaseModelEntitySUB_StartShadowFadeInPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseModelEntitySUB_StartShadowFadeInPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseModelEntitySUB_StartShadowFadeInHook : ICBaseModelEntitySUB_StartShadowFadeInHook
{
    private event OnCBaseModelEntitySUB_StartShadowFadeInPreDelegate? _Pre;
    private event OnCBaseModelEntitySUB_StartShadowFadeInPostDelegate? _Post;

    public event OnCBaseModelEntitySUB_StartShadowFadeInPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseModelEntitySUB_StartShadowFadeIn);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_StartShadowFadeIn);
            }
        }
    }

    public event OnCBaseModelEntitySUB_StartShadowFadeInPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseModelEntitySUB_StartShadowFadeIn);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_StartShadowFadeIn);
            }
        }
    }

    public void InvokePre(ref CBaseModelEntitySUB_StartShadowFadeInPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseModelEntitySUB_StartShadowFadeInPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_StartShadowFadeIn);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_StartShadowFadeIn);
        }
    }

    public void Invoke(CBaseModelEntity schemaObject) => DatamapHooksPublisher.InvokeCBaseModelEntitySUB_StartShadowFadeIn(schemaObject.Address);
}