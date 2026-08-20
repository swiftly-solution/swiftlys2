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
    private delegate void CBaseModelEntitySUB_DissolveIfUncarriedDelegate(nint a1);

    private static IUnmanagedFunction<CBaseModelEntitySUB_DissolveIfUncarriedDelegate>? CBaseModelEntitySUB_DissolveIfUncarriedUnmanagedFunction;
    private static Guid CBaseModelEntitySUB_DissolveIfUncarriedHookGuid;

    private static IUnmanagedFunction<CBaseModelEntitySUB_DissolveIfUncarriedDelegate> CBaseModelEntitySUB_DissolveIfUncarriedGetUnmanagedFunction()
    {
        if (CBaseModelEntitySUB_DissolveIfUncarriedUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseModelEntity", "CBaseModelEntitySUB_DissolveIfUncarried");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseModelEntity::CBaseModelEntitySUB_DissolveIfUncarried.");
            }
            CBaseModelEntitySUB_DissolveIfUncarriedUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseModelEntitySUB_DissolveIfUncarriedDelegate>(address);
        }
        return CBaseModelEntitySUB_DissolveIfUncarriedUnmanagedFunction;
    }

    internal static Guid HookCBaseModelEntitySUB_DissolveIfUncarried()
    {
        CBaseModelEntitySUB_DissolveIfUncarriedHookGuid = CBaseModelEntitySUB_DissolveIfUncarriedGetUnmanagedFunction().AddHook(next => (a1) => CBaseModelEntitySUB_DissolveIfUncarriedPipeline(a1, () => next()(a1)));
        return CBaseModelEntitySUB_DissolveIfUncarriedHookGuid;
    }

    internal static Guid UnhookCBaseModelEntitySUB_DissolveIfUncarried()
    {
        CBaseModelEntitySUB_DissolveIfUncarriedGetUnmanagedFunction().RemoveHook(CBaseModelEntitySUB_DissolveIfUncarriedHookGuid);
        return Guid.Empty;
    }

    private static void CBaseModelEntitySUB_DissolveIfUncarriedPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseModelEntity>(a1);

            var preCtx = new CBaseModelEntitySUB_DissolveIfUncarriedPreContext { SchemaObject = schemaObject };
            InvokeCBaseModelEntitySUB_DissolveIfUncarriedPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseModelEntitySUB_DissolveIfUncarriedPostContext { SchemaObject = schemaObject };
            InvokeCBaseModelEntitySUB_DissolveIfUncarriedPost(ref postCtx);
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

    internal static void InvokeCBaseModelEntitySUB_DissolveIfUncarried(nint a1)
    {
        CBaseModelEntitySUB_DissolveIfUncarriedGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseModelEntitySUB_DissolveIfUncarriedPre(ref CBaseModelEntitySUB_DissolveIfUncarriedPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseModelEntitySUB_DissolveIfUncarriedPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseModelEntitySUB_DissolveIfUncarriedPost(ref CBaseModelEntitySUB_DissolveIfUncarriedPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseModelEntitySUB_DissolveIfUncarriedPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseModelEntitySUB_DissolveIfUncarriedHook : ICBaseModelEntitySUB_DissolveIfUncarriedHook
{
    private event OnCBaseModelEntitySUB_DissolveIfUncarriedPreDelegate? _Pre;
    private event OnCBaseModelEntitySUB_DissolveIfUncarriedPostDelegate? _Post;

    public event OnCBaseModelEntitySUB_DissolveIfUncarriedPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseModelEntitySUB_DissolveIfUncarried);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_DissolveIfUncarried);
            }
        }
    }

    public event OnCBaseModelEntitySUB_DissolveIfUncarriedPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseModelEntitySUB_DissolveIfUncarried);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_DissolveIfUncarried);
            }
        }
    }

    public void InvokePre(ref CBaseModelEntitySUB_DissolveIfUncarriedPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseModelEntitySUB_DissolveIfUncarriedPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_DissolveIfUncarried);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_DissolveIfUncarried);
        }
    }

    public void Invoke(CBaseModelEntity schemaObject) => DatamapHooksPublisher.InvokeCBaseModelEntitySUB_DissolveIfUncarried(schemaObject.Address);
}