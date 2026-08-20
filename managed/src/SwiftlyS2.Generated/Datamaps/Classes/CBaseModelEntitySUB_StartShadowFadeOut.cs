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
    private delegate void CBaseModelEntitySUB_StartShadowFadeOutDelegate(nint a1);

    private static IUnmanagedFunction<CBaseModelEntitySUB_StartShadowFadeOutDelegate>? CBaseModelEntitySUB_StartShadowFadeOutUnmanagedFunction;
    private static Guid CBaseModelEntitySUB_StartShadowFadeOutHookGuid;

    private static IUnmanagedFunction<CBaseModelEntitySUB_StartShadowFadeOutDelegate> CBaseModelEntitySUB_StartShadowFadeOutGetUnmanagedFunction()
    {
        if (CBaseModelEntitySUB_StartShadowFadeOutUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseModelEntity", "CBaseModelEntitySUB_StartShadowFadeOut");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseModelEntity::CBaseModelEntitySUB_StartShadowFadeOut.");
            }
            CBaseModelEntitySUB_StartShadowFadeOutUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseModelEntitySUB_StartShadowFadeOutDelegate>(address);
        }
        return CBaseModelEntitySUB_StartShadowFadeOutUnmanagedFunction;
    }

    internal static Guid HookCBaseModelEntitySUB_StartShadowFadeOut()
    {
        CBaseModelEntitySUB_StartShadowFadeOutHookGuid = CBaseModelEntitySUB_StartShadowFadeOutGetUnmanagedFunction().AddHook(next => (a1) => CBaseModelEntitySUB_StartShadowFadeOutPipeline(a1, () => next()(a1)));
        return CBaseModelEntitySUB_StartShadowFadeOutHookGuid;
    }

    internal static Guid UnhookCBaseModelEntitySUB_StartShadowFadeOut()
    {
        CBaseModelEntitySUB_StartShadowFadeOutGetUnmanagedFunction().RemoveHook(CBaseModelEntitySUB_StartShadowFadeOutHookGuid);
        return Guid.Empty;
    }

    private static void CBaseModelEntitySUB_StartShadowFadeOutPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseModelEntity>(a1);

            var preCtx = new CBaseModelEntitySUB_StartShadowFadeOutPreContext { SchemaObject = schemaObject };
            InvokeCBaseModelEntitySUB_StartShadowFadeOutPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseModelEntitySUB_StartShadowFadeOutPostContext { SchemaObject = schemaObject };
            InvokeCBaseModelEntitySUB_StartShadowFadeOutPost(ref postCtx);
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

    internal static void InvokeCBaseModelEntitySUB_StartShadowFadeOut(nint a1)
    {
        CBaseModelEntitySUB_StartShadowFadeOutGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseModelEntitySUB_StartShadowFadeOutPre(ref CBaseModelEntitySUB_StartShadowFadeOutPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseModelEntitySUB_StartShadowFadeOutPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseModelEntitySUB_StartShadowFadeOutPost(ref CBaseModelEntitySUB_StartShadowFadeOutPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseModelEntitySUB_StartShadowFadeOutPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseModelEntitySUB_StartShadowFadeOutHook : ICBaseModelEntitySUB_StartShadowFadeOutHook
{
    private event OnCBaseModelEntitySUB_StartShadowFadeOutPreDelegate? _Pre;
    private event OnCBaseModelEntitySUB_StartShadowFadeOutPostDelegate? _Post;

    public event OnCBaseModelEntitySUB_StartShadowFadeOutPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseModelEntitySUB_StartShadowFadeOut);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_StartShadowFadeOut);
            }
        }
    }

    public event OnCBaseModelEntitySUB_StartShadowFadeOutPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseModelEntitySUB_StartShadowFadeOut);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_StartShadowFadeOut);
            }
        }
    }

    public void InvokePre(ref CBaseModelEntitySUB_StartShadowFadeOutPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseModelEntitySUB_StartShadowFadeOutPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_StartShadowFadeOut);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_StartShadowFadeOut);
        }
    }

    public void Invoke(CBaseModelEntity schemaObject) => DatamapHooksPublisher.InvokeCBaseModelEntitySUB_StartShadowFadeOut(schemaObject.Address);
}