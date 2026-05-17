using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Memory;
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

                ICanUseWeapon @event = new CanUseWeaponData {
                    Player = player,
                    Weapon = basePlayerWeapon,
                    OriginalResult = true,
                    Intercepted = false
                };

                InvokeCanUsePre(ref @event);
                if (@event.Intercepted) return @event.OriginalResult ? (byte)1 : (byte)0;

                var result = next()(pWeaponServices, pBasePlayerWeapon);

                @event.SetResult(result != 0);
                @event.Intercepted = false;

                InvokeCanUsePost(ref @event);

                return @event.Intercepted ? (@event.OriginalResult ? (byte)1 : (byte)0) : result;
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

    internal static void InvokeCanUsePre( ref ICanUseWeapon @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCanUsePre(ref @event);
            }
        }
    }

    internal static void InvokeCanUsePost( ref ICanUseWeapon @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCanUsePost(ref @event);
            }
        }
    }
}
