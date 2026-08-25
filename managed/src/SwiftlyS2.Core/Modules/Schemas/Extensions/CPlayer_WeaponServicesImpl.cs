using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Core.SchemaDefinitions;

internal partial class CPlayer_WeaponServicesImpl
{
    public void DropWeapon( CBasePlayerWeapon weapon )
    {
        NativeBinding.ThrowIfNonMainThread();
        unsafe
        {
            GameFunctions.CCSPlayer_WeaponServices_DropWeapon(Address, weapon.Address, null);
        }
    }

    public void DropWeapon( CBasePlayerWeapon weapon, Vector momentum )
    {
        NativeBinding.ThrowIfNonMainThread();
        unsafe
        {
            GameFunctions.CCSPlayer_WeaponServices_DropWeapon(Address, weapon.Address, &momentum);
        }
    }

    public void RemoveWeapon( CBasePlayerWeapon weapon )
    {
        NativeBinding.ThrowIfNonMainThread();
        unsafe
        {
            GameFunctions.CCSPlayer_WeaponServices_DropWeapon(Address, weapon.Address, null);
        }
        weapon.Despawn();
    }

    public void SelectWeapon( CBasePlayerWeapon weapon )
    {
        NativeBinding.ThrowIfNonMainThread();
        GameFunctions.CCSPlayer_WeaponServices_SelectWeapon(Address, weapon.Address);
    }

    public void DropWeaponBySlot( gear_slot_t slot )
    {
        NativeBinding.ThrowIfNonMainThread();
        MyWeapons.ToList().ForEach(weapon =>
        {
            if (weapon.Value?.As<CCSWeaponBase>().WeaponBaseVData.GearSlot == slot)
            {
                DropWeapon(weapon.Value);
            }
        });
    }

    public void DropWeaponBySlot( gear_slot_t slot, Vector momentum )
    {
        NativeBinding.ThrowIfNonMainThread();
        MyWeapons.ToList().ForEach(weapon =>
        {
            if (weapon.Value?.As<CCSWeaponBase>().WeaponBaseVData.GearSlot == slot)
            {
                DropWeapon(weapon.Value, momentum);
            }
        });
    }

    public void RemoveWeaponBySlot( gear_slot_t slot )
    {
        NativeBinding.ThrowIfNonMainThread();
        MyWeapons.ToList().ForEach(weapon =>
        {
            var basePlayerWeapon = weapon.Value;
            if (basePlayerWeapon == null || !basePlayerWeapon.IsValid) return;

            var weaponBase = basePlayerWeapon.As<CCSWeaponBase>();
            if (weaponBase.WeaponBaseVData.GearSlot == slot)
            {
                RemoveWeapon(basePlayerWeapon);
            }
        });
    }

    public void SelectWeaponBySlot( gear_slot_t slot )
    {
        NativeBinding.ThrowIfNonMainThread();
        MyWeapons.ToList().ForEach(weapon =>
        {
            if (weapon.Value?.As<CCSWeaponBase>().WeaponBaseVData.GearSlot == slot)
            {
                SelectWeapon(weapon.Value);
                return;
            }
        });
    }

    public void DropWeaponByDesignerName( string designerName )
    {
        NativeBinding.ThrowIfNonMainThread();
        MyWeapons.ToList().ForEach(weapon =>
        {
            if (weapon.Value?.DesignerName == designerName)
            {
                DropWeapon(weapon.Value);
            }
        });
    }

    public void DropWeaponByDesignerName( string designerName, Vector momentum )
    {
        NativeBinding.ThrowIfNonMainThread();
        MyWeapons.ToList().ForEach(weapon =>
        {
            if (weapon.Value?.DesignerName == designerName)
            {
                DropWeapon(weapon.Value, momentum);
            }
        });
    }

    public void RemoveWeaponByDesignerName( string designerName )
    {
        NativeBinding.ThrowIfNonMainThread();
        MyWeapons.ToList().ForEach(weapon =>
        {
            if (weapon.Value?.DesignerName == designerName)
            {
                RemoveWeapon(weapon.Value);
            }
        });
    }

    public void SelectWeaponByDesignerName( string designerName )
    {
        NativeBinding.ThrowIfNonMainThread();
        MyWeapons.ToList().ForEach(weapon =>
        {
            if (weapon.Value?.DesignerName == designerName)
            {
                SelectWeapon(weapon.Value);
            }
        });
    }

    public void DropWeaponByClass<T>() where T : class, ISchemaClass<T>
    {
        NativeBinding.ThrowIfNonMainThread();
        var name = T.ClassName;
        if (name == null)
        {
            throw new ArgumentException(
                $"Can't drop weapon with class {typeof(T).Name}, which doesn't have a designer name.");
        }

        DropWeaponByDesignerName(name);
    }

    public void DropWeaponByClass<T>( Vector momentum ) where T : class, ISchemaClass<T>
    {
        NativeBinding.ThrowIfNonMainThread();
        var name = T.ClassName;
        if (name == null)
        {
            throw new ArgumentException(
                $"Can't drop weapon with class {typeof(T).Name}, which doesn't have a designer name.");
        }

        DropWeaponByDesignerName(name, momentum);
    }

    public void RemoveWeaponByClass<T>() where T : class, ISchemaClass<T>
    {
        NativeBinding.ThrowIfNonMainThread();
        var name = T.ClassName;
        if (name == null)
        {
            throw new ArgumentException(
                $"Can't drop weapon with class {typeof(T).Name}, which doesn't have a designer name.");
        }

        RemoveWeaponByDesignerName(name);
    }

    public void SelectWeaponByClass<T>() where T : class, ISchemaClass<T>
    {
        NativeBinding.ThrowIfNonMainThread();
        var name = T.ClassName;
        if (name == null)
        {
            throw new ArgumentException(
                $"Can't drop weapon with class {typeof(T).Name}, which doesn't have a designer name.");
        }

        SelectWeaponByDesignerName(name);
    }

    public Task DropWeaponAsync( CBasePlayerWeapon weapon )
    {
        return SchedulerManager.QueueOrNow(() => DropWeapon(weapon));
    }

    public Task DropWeaponAsync( CBasePlayerWeapon weapon, Vector momentum )
    {
        return SchedulerManager.QueueOrNow(() => DropWeapon(weapon, momentum));
    }

    public Task RemoveWeaponAsync( CBasePlayerWeapon weapon )
    {
        return SchedulerManager.QueueOrNow(() => RemoveWeapon(weapon));
    }

    public Task SelectWeaponAsync( CBasePlayerWeapon weapon )
    {
        return SchedulerManager.QueueOrNow(() => SelectWeapon(weapon));
    }

    public Task DropWeaponBySlotAsync( gear_slot_t slot )
    {
        return SchedulerManager.QueueOrNow(() => DropWeaponBySlot(slot));
    }

    public Task DropWeaponBySlotAsync( gear_slot_t slot, Vector momentum )
    {
        return SchedulerManager.QueueOrNow(() => DropWeaponBySlot(slot, momentum));
    }

    public Task RemoveWeaponBySlotAsync( gear_slot_t slot )
    {
        return SchedulerManager.QueueOrNow(() => RemoveWeaponBySlot(slot));
    }

    public Task SelectWeaponBySlotAsync( gear_slot_t slot )
    {
        return SchedulerManager.QueueOrNow(() => SelectWeaponBySlot(slot));
    }

    public Task DropWeaponByDesignerNameAsync( string designerName )
    {
        return SchedulerManager.QueueOrNow(() => DropWeaponByDesignerName(designerName));
    }

    public Task DropWeaponByDesignerNameAsync( string designerName, Vector momentum )
    {
        return SchedulerManager.QueueOrNow(() => DropWeaponByDesignerName(designerName, momentum));
    }

    public Task RemoveWeaponByDesignerNameAsync( string designerName )
    {
        return SchedulerManager.QueueOrNow(() => RemoveWeaponByDesignerName(designerName));
    }

    public Task SelectWeaponByDesignerNameAsync( string designerName )
    {
        return SchedulerManager.QueueOrNow(() => SelectWeaponByDesignerName(designerName));
    }

    public Task DropWeaponByClassAsync<T>() where T : class, ISchemaClass<T>
    {
        return SchedulerManager.QueueOrNow(() => DropWeaponByClass<T>());
    }

    public Task DropWeaponByClassAsync<T>( Vector momentum ) where T : class, ISchemaClass<T>
    {
        return SchedulerManager.QueueOrNow(() => DropWeaponByClass<T>(momentum));
    }

    public Task RemoveWeaponByClassAsync<T>() where T : class, ISchemaClass<T>
    {
        return SchedulerManager.QueueOrNow(() => RemoveWeaponByClass<T>());
    }

    public Task SelectWeaponByClassAsync<T>() where T : class, ISchemaClass<T>
    {
        return SchedulerManager.QueueOrNow(() => SelectWeaponByClass<T>());
    }

    public IEnumerable<CBasePlayerWeapon> MyValidWeapons => MyWeapons.ToList()
        .Where(w => w.IsValid && w.Value != null && w.Value.IsValid).Select(w => w.Value!);
}