using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate byte CCSPlayerWeaponServicesCanUse( nint pWeaponServices, nint pBasePlayerWeapon );

    internal static Guid HookCanUse()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var offset = _core.GameData.GetOffset("CCSPlayer_WeaponServices::CanUse");
        if (offset < 0)
            throw new InvalidOperationException("Failed to find offset for CCSPlayer_WeaponServices::CanUse.");

        var canUse = _core.Memory.GetUnmanagedFunctionByVTable<CCSPlayerWeaponServicesCanUse>(_core.Memory.GetVTableAddress(Library.Server, "CCSPlayer_WeaponServices")!.Value, offset);
        return canUse.AddHook(next =>
        {
            return ( pWeaponServices, pBasePlayerWeapon ) =>
            {
                _dummyPawnComponent.DangerousSetHandle(pWeaponServices);
                var player = _dummyPawnComponent.ToPlayer();
                if (player == null) return next()(pWeaponServices, pBasePlayerWeapon);

                var basePlayerWeapon = EntityManager.GetEntityByAddress(pBasePlayerWeapon) as CCSWeaponBase ?? _core.Memory.ToSchemaClass<CCSWeaponBase>(pBasePlayerWeapon);

                var preCtx = new CanUseWeaponPreContext {
                    Params = new CanUseWeaponParams {
                        Player = player,
                        Weapon = basePlayerWeapon
                    }
                };

                InvokeCanUsePre(ref preCtx);

                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                    return preCtx.IsReturnSet ? (preCtx.Return ? (byte)1 : (byte)0) : (byte)1;

                var result = next()(pWeaponServices, pBasePlayerWeapon);

                var postCtx = new CanUseWeaponPostContext { Params = preCtx.Params, Return = result != 0 };

                InvokeCanUsePost(ref postCtx);

                return postCtx.Return ? (byte)1 : (byte)0;
            };
        });
    }

    internal static Guid UnhookCanUse()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.CanUse, out var hookId))
        {
            var offset = _core.GameData.GetOffset("CCSPlayer_WeaponServices::CanUse");
            if (offset < 0)
                throw new InvalidOperationException("Failed to find offset for CCSPlayer_WeaponServices::CanUse.");

            var canUse = _core.Memory.GetUnmanagedFunctionByVTable<CCSPlayerWeaponServicesCanUse>(_core.Memory.GetVTableAddress(Library.Server, "CCSPlayer_WeaponServices")!.Value, offset);
            canUse.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeCanUsePre( ref CanUseWeaponPreContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCanUsePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeCanUsePost( ref CanUseWeaponPostContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCanUsePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }
}
