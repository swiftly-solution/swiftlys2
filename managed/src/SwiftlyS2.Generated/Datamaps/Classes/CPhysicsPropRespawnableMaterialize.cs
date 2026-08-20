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
    private delegate void CPhysicsPropRespawnableMaterializeDelegate(nint a1);

    private static IUnmanagedFunction<CPhysicsPropRespawnableMaterializeDelegate>? CPhysicsPropRespawnableMaterializeUnmanagedFunction;
    private static Guid CPhysicsPropRespawnableMaterializeHookGuid;

    private static IUnmanagedFunction<CPhysicsPropRespawnableMaterializeDelegate> CPhysicsPropRespawnableMaterializeGetUnmanagedFunction()
    {
        if (CPhysicsPropRespawnableMaterializeUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPhysicsPropRespawnable", "CPhysicsPropRespawnableMaterialize");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPhysicsPropRespawnable::CPhysicsPropRespawnableMaterialize.");
            }
            CPhysicsPropRespawnableMaterializeUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPhysicsPropRespawnableMaterializeDelegate>(address);
        }
        return CPhysicsPropRespawnableMaterializeUnmanagedFunction;
    }

    internal static Guid HookCPhysicsPropRespawnableMaterialize()
    {
        CPhysicsPropRespawnableMaterializeHookGuid = CPhysicsPropRespawnableMaterializeGetUnmanagedFunction().AddHook(next => (a1) => CPhysicsPropRespawnableMaterializePipeline(a1, () => next()(a1)));
        return CPhysicsPropRespawnableMaterializeHookGuid;
    }

    internal static Guid UnhookCPhysicsPropRespawnableMaterialize()
    {
        CPhysicsPropRespawnableMaterializeGetUnmanagedFunction().RemoveHook(CPhysicsPropRespawnableMaterializeHookGuid);
        return Guid.Empty;
    }

    private static void CPhysicsPropRespawnableMaterializePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPhysicsPropRespawnable>(a1);

            var preCtx = new CPhysicsPropRespawnableMaterializePreContext { SchemaObject = schemaObject };
            InvokeCPhysicsPropRespawnableMaterializePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPhysicsPropRespawnableMaterializePostContext { SchemaObject = schemaObject };
            InvokeCPhysicsPropRespawnableMaterializePost(ref postCtx);
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

    internal static void InvokeCPhysicsPropRespawnableMaterialize(nint a1)
    {
        CPhysicsPropRespawnableMaterializeGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPhysicsPropRespawnableMaterializePre(ref CPhysicsPropRespawnableMaterializePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicsPropRespawnableMaterializePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPhysicsPropRespawnableMaterializePost(ref CPhysicsPropRespawnableMaterializePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysicsPropRespawnableMaterializePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPhysicsPropRespawnableMaterializeHook : ICPhysicsPropRespawnableMaterializeHook
{
    private event OnCPhysicsPropRespawnableMaterializePreDelegate? _Pre;
    private event OnCPhysicsPropRespawnableMaterializePostDelegate? _Post;

    public event OnCPhysicsPropRespawnableMaterializePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicsPropRespawnableMaterialize);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicsPropRespawnableMaterialize);
            }
        }
    }

    public event OnCPhysicsPropRespawnableMaterializePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysicsPropRespawnableMaterialize);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicsPropRespawnableMaterialize);
            }
        }
    }

    public void InvokePre(ref CPhysicsPropRespawnableMaterializePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPhysicsPropRespawnableMaterializePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicsPropRespawnableMaterialize);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysicsPropRespawnableMaterialize);
        }
    }

    public void Invoke(CPhysicsPropRespawnable schemaObject) => DatamapHooksPublisher.InvokeCPhysicsPropRespawnableMaterialize(schemaObject.Address);
}