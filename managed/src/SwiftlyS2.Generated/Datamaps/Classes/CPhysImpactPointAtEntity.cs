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
    private delegate void CPhysImpactPointAtEntityDelegate(nint a1);

    private static IUnmanagedFunction<CPhysImpactPointAtEntityDelegate>? CPhysImpactPointAtEntityUnmanagedFunction;
    private static Guid CPhysImpactPointAtEntityHookGuid;

    private static IUnmanagedFunction<CPhysImpactPointAtEntityDelegate> CPhysImpactPointAtEntityGetUnmanagedFunction()
    {
        if (CPhysImpactPointAtEntityUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPhysImpact", "CPhysImpactPointAtEntity");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPhysImpact::CPhysImpactPointAtEntity.");
            }
            CPhysImpactPointAtEntityUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPhysImpactPointAtEntityDelegate>(address);
        }
        return CPhysImpactPointAtEntityUnmanagedFunction;
    }

    internal static Guid HookCPhysImpactPointAtEntity()
    {
        CPhysImpactPointAtEntityHookGuid = CPhysImpactPointAtEntityGetUnmanagedFunction().AddHook(next => (a1) => CPhysImpactPointAtEntityPipeline(a1, () => next()(a1)));
        return CPhysImpactPointAtEntityHookGuid;
    }

    internal static Guid UnhookCPhysImpactPointAtEntity()
    {
        CPhysImpactPointAtEntityGetUnmanagedFunction().RemoveHook(CPhysImpactPointAtEntityHookGuid);
        return Guid.Empty;
    }

    private static void CPhysImpactPointAtEntityPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPhysImpact>(a1);

            var preCtx = new CPhysImpactPointAtEntityPreContext { SchemaObject = schemaObject };
            InvokeCPhysImpactPointAtEntityPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPhysImpactPointAtEntityPostContext { SchemaObject = schemaObject };
            InvokeCPhysImpactPointAtEntityPost(ref postCtx);
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

    internal static void InvokeCPhysImpactPointAtEntity(nint a1)
    {
        CPhysImpactPointAtEntityGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPhysImpactPointAtEntityPre(ref CPhysImpactPointAtEntityPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysImpactPointAtEntityPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPhysImpactPointAtEntityPost(ref CPhysImpactPointAtEntityPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysImpactPointAtEntityPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPhysImpactPointAtEntityHook : ICPhysImpactPointAtEntityHook
{
    private event OnCPhysImpactPointAtEntityPreDelegate? _Pre;
    private event OnCPhysImpactPointAtEntityPostDelegate? _Post;

    public event OnCPhysImpactPointAtEntityPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysImpactPointAtEntity);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysImpactPointAtEntity);
            }
        }
    }

    public event OnCPhysImpactPointAtEntityPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysImpactPointAtEntity);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysImpactPointAtEntity);
            }
        }
    }

    public void InvokePre(ref CPhysImpactPointAtEntityPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPhysImpactPointAtEntityPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysImpactPointAtEntity);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysImpactPointAtEntity);
        }
    }

    public void Invoke(CPhysImpact schemaObject) => DatamapHooksPublisher.InvokeCPhysImpactPointAtEntity(schemaObject.Address);
}