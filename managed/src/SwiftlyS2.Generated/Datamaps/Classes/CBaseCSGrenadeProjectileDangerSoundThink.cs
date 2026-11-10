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
    private delegate void CBaseCSGrenadeProjectileDangerSoundThinkDelegate(nint a1);

    private static IUnmanagedFunction<CBaseCSGrenadeProjectileDangerSoundThinkDelegate>? CBaseCSGrenadeProjectileDangerSoundThinkUnmanagedFunction;
    private static Guid CBaseCSGrenadeProjectileDangerSoundThinkHookGuid;

    private static IUnmanagedFunction<CBaseCSGrenadeProjectileDangerSoundThinkDelegate> CBaseCSGrenadeProjectileDangerSoundThinkGetUnmanagedFunction()
    {
        if (CBaseCSGrenadeProjectileDangerSoundThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseCSGrenadeProjectile", "CBaseCSGrenadeProjectileDangerSoundThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseCSGrenadeProjectile::CBaseCSGrenadeProjectileDangerSoundThink.");
            }
            CBaseCSGrenadeProjectileDangerSoundThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseCSGrenadeProjectileDangerSoundThinkDelegate>(address);
        }
        return CBaseCSGrenadeProjectileDangerSoundThinkUnmanagedFunction;
    }

    internal static Guid HookCBaseCSGrenadeProjectileDangerSoundThink()
    {
        CBaseCSGrenadeProjectileDangerSoundThinkHookGuid = CBaseCSGrenadeProjectileDangerSoundThinkGetUnmanagedFunction().AddHook(next => (a1) => CBaseCSGrenadeProjectileDangerSoundThinkPipeline(a1, () => next()(a1)));
        return CBaseCSGrenadeProjectileDangerSoundThinkHookGuid;
    }

    internal static Guid UnhookCBaseCSGrenadeProjectileDangerSoundThink()
    {
        CBaseCSGrenadeProjectileDangerSoundThinkGetUnmanagedFunction().RemoveHook(CBaseCSGrenadeProjectileDangerSoundThinkHookGuid);
        return Guid.Empty;
    }

    private static void CBaseCSGrenadeProjectileDangerSoundThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseCSGrenadeProjectile>(a1);

            var preCtx = new CBaseCSGrenadeProjectileDangerSoundThinkPreContext { SchemaObject = schemaObject };
            InvokeCBaseCSGrenadeProjectileDangerSoundThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseCSGrenadeProjectileDangerSoundThinkPostContext { SchemaObject = schemaObject };
            InvokeCBaseCSGrenadeProjectileDangerSoundThinkPost(ref postCtx);
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

    internal static void InvokeCBaseCSGrenadeProjectileDangerSoundThink(nint a1)
    {
        CBaseCSGrenadeProjectileDangerSoundThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseCSGrenadeProjectileDangerSoundThinkPre(ref CBaseCSGrenadeProjectileDangerSoundThinkPreContext ctx)
    {
        var subs = GetSubscribers(DatamapHookListener.CBaseCSGrenadeProjectileDangerSoundThink);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCBaseCSGrenadeProjectileDangerSoundThinkPre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
            {
                return;
            }
        }
    }

    internal static void InvokeCBaseCSGrenadeProjectileDangerSoundThinkPost(ref CBaseCSGrenadeProjectileDangerSoundThinkPostContext ctx)
    {
        var subs = GetSubscribers(DatamapHookListener.CBaseCSGrenadeProjectileDangerSoundThink);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCBaseCSGrenadeProjectileDangerSoundThinkPost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
            {
                return;
            }
        }
    }
}

internal sealed class CBaseCSGrenadeProjectileDangerSoundThinkHook(GameHooksService owner) : ICBaseCSGrenadeProjectileDangerSoundThinkHook
{
    private event OnCBaseCSGrenadeProjectileDangerSoundThinkPreDelegate? _Pre;
    private event OnCBaseCSGrenadeProjectileDangerSoundThinkPostDelegate? _Post;

    public event OnCBaseCSGrenadeProjectileDangerSoundThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseCSGrenadeProjectileDangerSoundThink, owner);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseCSGrenadeProjectileDangerSoundThink, owner);
            }
        }
    }

    public event OnCBaseCSGrenadeProjectileDangerSoundThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseCSGrenadeProjectileDangerSoundThink, owner);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseCSGrenadeProjectileDangerSoundThink, owner);
            }
        }
    }

    public void InvokePre(ref CBaseCSGrenadeProjectileDangerSoundThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseCSGrenadeProjectileDangerSoundThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseCSGrenadeProjectileDangerSoundThink, owner);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseCSGrenadeProjectileDangerSoundThink, owner);
        }
    }

    public void Invoke(CBaseCSGrenadeProjectile schemaObject) => DatamapHooksPublisher.InvokeCBaseCSGrenadeProjectileDangerSoundThink(schemaObject.Address);
}