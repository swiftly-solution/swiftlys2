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
    private delegate void CBaseModelEntitySUB_StartFadeOutDelegate(nint a1);

    private static IUnmanagedFunction<CBaseModelEntitySUB_StartFadeOutDelegate>? CBaseModelEntitySUB_StartFadeOutUnmanagedFunction;
    private static Guid CBaseModelEntitySUB_StartFadeOutHookGuid;

    private static IUnmanagedFunction<CBaseModelEntitySUB_StartFadeOutDelegate> CBaseModelEntitySUB_StartFadeOutGetUnmanagedFunction()
    {
        if (CBaseModelEntitySUB_StartFadeOutUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseModelEntity", "CBaseModelEntitySUB_StartFadeOut");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseModelEntity::CBaseModelEntitySUB_StartFadeOut.");
            }
            CBaseModelEntitySUB_StartFadeOutUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseModelEntitySUB_StartFadeOutDelegate>(address);
        }
        return CBaseModelEntitySUB_StartFadeOutUnmanagedFunction;
    }

    internal static Guid HookCBaseModelEntitySUB_StartFadeOut()
    {
        CBaseModelEntitySUB_StartFadeOutHookGuid = CBaseModelEntitySUB_StartFadeOutGetUnmanagedFunction().AddHook(next => (a1) => CBaseModelEntitySUB_StartFadeOutPipeline(a1, () => next()(a1)));
        return CBaseModelEntitySUB_StartFadeOutHookGuid;
    }

    internal static Guid UnhookCBaseModelEntitySUB_StartFadeOut()
    {
        CBaseModelEntitySUB_StartFadeOutGetUnmanagedFunction().RemoveHook(CBaseModelEntitySUB_StartFadeOutHookGuid);
        return Guid.Empty;
    }

    private static void CBaseModelEntitySUB_StartFadeOutPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseModelEntity>(a1);

            var preCtx = new CBaseModelEntitySUB_StartFadeOutPreContext { SchemaObject = schemaObject };
            InvokeCBaseModelEntitySUB_StartFadeOutPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseModelEntitySUB_StartFadeOutPostContext { SchemaObject = schemaObject };
            InvokeCBaseModelEntitySUB_StartFadeOutPost(ref postCtx);
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

    internal static void InvokeCBaseModelEntitySUB_StartFadeOut(nint a1)
    {
        CBaseModelEntitySUB_StartFadeOutGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseModelEntitySUB_StartFadeOutPre(ref CBaseModelEntitySUB_StartFadeOutPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseModelEntitySUB_StartFadeOutPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseModelEntitySUB_StartFadeOutPost(ref CBaseModelEntitySUB_StartFadeOutPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseModelEntitySUB_StartFadeOutPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseModelEntitySUB_StartFadeOutHook : ICBaseModelEntitySUB_StartFadeOutHook
{
    private event OnCBaseModelEntitySUB_StartFadeOutPreDelegate? _Pre;
    private event OnCBaseModelEntitySUB_StartFadeOutPostDelegate? _Post;

    public event OnCBaseModelEntitySUB_StartFadeOutPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseModelEntitySUB_StartFadeOut);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_StartFadeOut);
            }
        }
    }

    public event OnCBaseModelEntitySUB_StartFadeOutPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseModelEntitySUB_StartFadeOut);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_StartFadeOut);
            }
        }
    }

    public void InvokePre(ref CBaseModelEntitySUB_StartFadeOutPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseModelEntitySUB_StartFadeOutPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_StartFadeOut);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_StartFadeOut);
        }
    }

    public void Invoke(CBaseModelEntity schemaObject) => DatamapHooksPublisher.InvokeCBaseModelEntitySUB_StartFadeOut(schemaObject.Address);
}