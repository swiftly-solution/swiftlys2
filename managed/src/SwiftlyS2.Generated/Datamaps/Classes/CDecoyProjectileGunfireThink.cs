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
    private delegate void CDecoyProjectileGunfireThinkDelegate(nint a1);

    private static IUnmanagedFunction<CDecoyProjectileGunfireThinkDelegate>? CDecoyProjectileGunfireThinkUnmanagedFunction;
    private static Guid CDecoyProjectileGunfireThinkHookGuid;

    private static IUnmanagedFunction<CDecoyProjectileGunfireThinkDelegate> CDecoyProjectileGunfireThinkGetUnmanagedFunction()
    {
        if (CDecoyProjectileGunfireThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CDecoyProjectile", "CDecoyProjectileGunfireThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CDecoyProjectile::CDecoyProjectileGunfireThink.");
            }
            CDecoyProjectileGunfireThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CDecoyProjectileGunfireThinkDelegate>(address);
        }
        return CDecoyProjectileGunfireThinkUnmanagedFunction;
    }

    internal static Guid HookCDecoyProjectileGunfireThink()
    {
        CDecoyProjectileGunfireThinkHookGuid = CDecoyProjectileGunfireThinkGetUnmanagedFunction().AddHook(next => (a1) => CDecoyProjectileGunfireThinkPipeline(a1, () => next()(a1)));
        return CDecoyProjectileGunfireThinkHookGuid;
    }

    internal static Guid UnhookCDecoyProjectileGunfireThink()
    {
        CDecoyProjectileGunfireThinkGetUnmanagedFunction().RemoveHook(CDecoyProjectileGunfireThinkHookGuid);
        return Guid.Empty;
    }

    private static void CDecoyProjectileGunfireThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CDecoyProjectile>(a1);

            var preCtx = new CDecoyProjectileGunfireThinkPreContext { SchemaObject = schemaObject };
            InvokeCDecoyProjectileGunfireThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CDecoyProjectileGunfireThinkPostContext { SchemaObject = schemaObject };
            InvokeCDecoyProjectileGunfireThinkPost(ref postCtx);
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

    internal static void InvokeCDecoyProjectileGunfireThink(nint a1)
    {
        CDecoyProjectileGunfireThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCDecoyProjectileGunfireThinkPre(ref CDecoyProjectileGunfireThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCDecoyProjectileGunfireThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCDecoyProjectileGunfireThinkPost(ref CDecoyProjectileGunfireThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCDecoyProjectileGunfireThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CDecoyProjectileGunfireThinkHook : ICDecoyProjectileGunfireThinkHook
{
    private event OnCDecoyProjectileGunfireThinkPreDelegate? _Pre;
    private event OnCDecoyProjectileGunfireThinkPostDelegate? _Post;

    public event OnCDecoyProjectileGunfireThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CDecoyProjectileGunfireThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CDecoyProjectileGunfireThink);
            }
        }
    }

    public event OnCDecoyProjectileGunfireThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CDecoyProjectileGunfireThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CDecoyProjectileGunfireThink);
            }
        }
    }

    public void InvokePre(ref CDecoyProjectileGunfireThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CDecoyProjectileGunfireThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CDecoyProjectileGunfireThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CDecoyProjectileGunfireThink);
        }
    }

    public void Invoke(CDecoyProjectile schemaObject) => DatamapHooksPublisher.InvokeCDecoyProjectileGunfireThink(schemaObject.Address);
}