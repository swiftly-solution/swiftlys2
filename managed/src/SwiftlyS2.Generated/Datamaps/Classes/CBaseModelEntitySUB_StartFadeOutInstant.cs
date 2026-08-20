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
    private delegate void CBaseModelEntitySUB_StartFadeOutInstantDelegate(nint a1);

    private static IUnmanagedFunction<CBaseModelEntitySUB_StartFadeOutInstantDelegate>? CBaseModelEntitySUB_StartFadeOutInstantUnmanagedFunction;
    private static Guid CBaseModelEntitySUB_StartFadeOutInstantHookGuid;

    private static IUnmanagedFunction<CBaseModelEntitySUB_StartFadeOutInstantDelegate> CBaseModelEntitySUB_StartFadeOutInstantGetUnmanagedFunction()
    {
        if (CBaseModelEntitySUB_StartFadeOutInstantUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseModelEntity", "CBaseModelEntitySUB_StartFadeOutInstant");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseModelEntity::CBaseModelEntitySUB_StartFadeOutInstant.");
            }
            CBaseModelEntitySUB_StartFadeOutInstantUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseModelEntitySUB_StartFadeOutInstantDelegate>(address);
        }
        return CBaseModelEntitySUB_StartFadeOutInstantUnmanagedFunction;
    }

    internal static Guid HookCBaseModelEntitySUB_StartFadeOutInstant()
    {
        CBaseModelEntitySUB_StartFadeOutInstantHookGuid = CBaseModelEntitySUB_StartFadeOutInstantGetUnmanagedFunction().AddHook(next => (a1) => CBaseModelEntitySUB_StartFadeOutInstantPipeline(a1, () => next()(a1)));
        return CBaseModelEntitySUB_StartFadeOutInstantHookGuid;
    }

    internal static Guid UnhookCBaseModelEntitySUB_StartFadeOutInstant()
    {
        CBaseModelEntitySUB_StartFadeOutInstantGetUnmanagedFunction().RemoveHook(CBaseModelEntitySUB_StartFadeOutInstantHookGuid);
        return Guid.Empty;
    }

    private static void CBaseModelEntitySUB_StartFadeOutInstantPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseModelEntity>(a1);

            var preCtx = new CBaseModelEntitySUB_StartFadeOutInstantPreContext { SchemaObject = schemaObject };
            InvokeCBaseModelEntitySUB_StartFadeOutInstantPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseModelEntitySUB_StartFadeOutInstantPostContext { SchemaObject = schemaObject };
            InvokeCBaseModelEntitySUB_StartFadeOutInstantPost(ref postCtx);
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

    internal static void InvokeCBaseModelEntitySUB_StartFadeOutInstant(nint a1)
    {
        CBaseModelEntitySUB_StartFadeOutInstantGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseModelEntitySUB_StartFadeOutInstantPre(ref CBaseModelEntitySUB_StartFadeOutInstantPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseModelEntitySUB_StartFadeOutInstantPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseModelEntitySUB_StartFadeOutInstantPost(ref CBaseModelEntitySUB_StartFadeOutInstantPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseModelEntitySUB_StartFadeOutInstantPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseModelEntitySUB_StartFadeOutInstantHook : ICBaseModelEntitySUB_StartFadeOutInstantHook
{
    private event OnCBaseModelEntitySUB_StartFadeOutInstantPreDelegate? _Pre;
    private event OnCBaseModelEntitySUB_StartFadeOutInstantPostDelegate? _Post;

    public event OnCBaseModelEntitySUB_StartFadeOutInstantPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseModelEntitySUB_StartFadeOutInstant);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_StartFadeOutInstant);
            }
        }
    }

    public event OnCBaseModelEntitySUB_StartFadeOutInstantPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseModelEntitySUB_StartFadeOutInstant);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_StartFadeOutInstant);
            }
        }
    }

    public void InvokePre(ref CBaseModelEntitySUB_StartFadeOutInstantPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseModelEntitySUB_StartFadeOutInstantPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_StartFadeOutInstant);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_StartFadeOutInstant);
        }
    }

    public void Invoke(CBaseModelEntity schemaObject) => DatamapHooksPublisher.InvokeCBaseModelEntitySUB_StartFadeOutInstant(schemaObject.Address);
}