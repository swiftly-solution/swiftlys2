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
    private delegate void CDecoyProjectileThink_DetonateDelegate(nint a1);

    private static IUnmanagedFunction<CDecoyProjectileThink_DetonateDelegate>? CDecoyProjectileThink_DetonateUnmanagedFunction;
    private static Guid CDecoyProjectileThink_DetonateHookGuid;

    private static IUnmanagedFunction<CDecoyProjectileThink_DetonateDelegate> CDecoyProjectileThink_DetonateGetUnmanagedFunction()
    {
        if (CDecoyProjectileThink_DetonateUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CDecoyProjectile", "CDecoyProjectileThink_Detonate");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CDecoyProjectile::CDecoyProjectileThink_Detonate.");
            }
            CDecoyProjectileThink_DetonateUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CDecoyProjectileThink_DetonateDelegate>(address);
        }
        return CDecoyProjectileThink_DetonateUnmanagedFunction;
    }

    internal static Guid HookCDecoyProjectileThink_Detonate()
    {
        CDecoyProjectileThink_DetonateHookGuid = CDecoyProjectileThink_DetonateGetUnmanagedFunction().AddHook(next => (a1) => CDecoyProjectileThink_DetonatePipeline(a1, () => next()(a1)));
        return CDecoyProjectileThink_DetonateHookGuid;
    }

    internal static Guid UnhookCDecoyProjectileThink_Detonate()
    {
        CDecoyProjectileThink_DetonateGetUnmanagedFunction().RemoveHook(CDecoyProjectileThink_DetonateHookGuid);
        return Guid.Empty;
    }

    private static void CDecoyProjectileThink_DetonatePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CDecoyProjectile>(a1);

            var preCtx = new CDecoyProjectileThink_DetonatePreContext { SchemaObject = schemaObject };
            InvokeCDecoyProjectileThink_DetonatePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CDecoyProjectileThink_DetonatePostContext { SchemaObject = schemaObject };
            InvokeCDecoyProjectileThink_DetonatePost(ref postCtx);
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

    internal static void InvokeCDecoyProjectileThink_Detonate(nint a1)
    {
        CDecoyProjectileThink_DetonateGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCDecoyProjectileThink_DetonatePre(ref CDecoyProjectileThink_DetonatePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCDecoyProjectileThink_DetonatePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCDecoyProjectileThink_DetonatePost(ref CDecoyProjectileThink_DetonatePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCDecoyProjectileThink_DetonatePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CDecoyProjectileThink_DetonateHook : ICDecoyProjectileThink_DetonateHook
{
    private event OnCDecoyProjectileThink_DetonatePreDelegate? _Pre;
    private event OnCDecoyProjectileThink_DetonatePostDelegate? _Post;

    public event OnCDecoyProjectileThink_DetonatePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CDecoyProjectileThink_Detonate);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CDecoyProjectileThink_Detonate);
            }
        }
    }

    public event OnCDecoyProjectileThink_DetonatePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CDecoyProjectileThink_Detonate);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CDecoyProjectileThink_Detonate);
            }
        }
    }

    public void InvokePre(ref CDecoyProjectileThink_DetonatePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CDecoyProjectileThink_DetonatePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CDecoyProjectileThink_Detonate);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CDecoyProjectileThink_Detonate);
        }
    }

    public void Invoke(CDecoyProjectile schemaObject) => DatamapHooksPublisher.InvokeCDecoyProjectileThink_Detonate(schemaObject.Address);
}