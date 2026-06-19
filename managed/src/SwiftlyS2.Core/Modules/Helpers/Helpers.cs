using System.Collections.Concurrent;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.Helpers;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Services;

internal class HelpersService : IHelpers
{
    public static readonly Dictionary<string, int> WeaponItemDefinitionIndices = new()
    {
        // Pistols
        { "weapon_deagle", 1 },
        { "weapon_elite", 2 },
        { "weapon_fiveseven", 3 },
        { "weapon_glock", 4 },
        { "weapon_tec9", 30 },
        { "weapon_hkp2000", 32 },
        { "weapon_p250", 36 },
        { "weapon_usp_silencer", 61 },
        { "weapon_cz75a", 63 },
        { "weapon_revolver", 64 },

        // Rifles
        { "weapon_ak47", 7 },
        { "weapon_aug", 8 },
        { "weapon_awp", 9 },
        { "weapon_famas", 10 },
        { "weapon_g3sg1", 11 },
        { "weapon_galilar", 13 },
        { "weapon_m249", 14 },
        { "weapon_m4a1", 16 },
        { "weapon_mac10", 17 },
        { "weapon_p90", 19 },
        { "weapon_mp5sd", 23 },
        { "weapon_ump45", 24 },
        { "weapon_xm1014", 25 },
        { "weapon_bizon", 26 },
        { "weapon_mag7", 27 },
        { "weapon_negev", 28 },
        { "weapon_sawedoff", 29 },
        { "weapon_mp7", 33 },
        { "weapon_mp9", 34 },
        { "weapon_nova", 35 },
        { "weapon_scar20", 38 },
        { "weapon_sg556", 39 },
        { "weapon_ssg08", 40 },
        { "weapon_m4a1_silencer", 60 },

        // Grenades
        { "weapon_flashbang", 43 },
        { "weapon_hegrenade", 44 },
        { "weapon_smokegrenade", 45 },
        { "weapon_molotov", 46 },
        { "weapon_decoy", 47 },
        { "weapon_incgrenade", 48 },

        // Knives and Equipment
        { "weapon_taser", 31 },
        { "weapon_knifegg", 41 },
        { "weapon_knife", 42 },
        { "weapon_c4", 49 },
        { "weapon_knife_t", 59 },
        { "weapon_bayonet", 500 },
        { "weapon_knife_css", 503 },
        { "weapon_knife_flip", 505 },
        { "weapon_knife_gut", 506 },
        { "weapon_knife_karambit", 507 },
        { "weapon_knife_m9_bayonet", 508 },
        { "weapon_knife_tactical", 509 },
        { "weapon_knife_falchion", 512 },
        { "weapon_knife_survival_bowie", 514 },
        { "weapon_knife_butterfly", 515 },
        { "weapon_knife_push", 516 },
        { "weapon_knife_cord", 517 },
        { "weapon_knife_canis", 518 },
        { "weapon_knife_ursus", 519 },
        { "weapon_knife_gypsy_jackknife", 520 },
        { "weapon_knife_outdoor", 521 },
        { "weapon_knife_stiletto", 522 },
        { "weapon_knife_widowmaker", 523 },
        { "weapon_knife_skeleton", 525 },
        { "weapon_knife_kukri", 526 },

        // Utility
        { "item_kevlar", 50 },
        { "item_assaultsuit", 51 },
        { "item_heavyassaultsuit", 52 },
        { "item_defuser", 55 },
        { "ammo_50ae", 0 }
    };

    public static ConcurrentDictionary<int, CCSWeaponBaseVData?> WeaponCSDataCache { get; } = new();

    public static void RenewVDataCache()
    {
        WeaponCSDataCache.Clear();

        foreach(var kvp in WeaponItemDefinitionIndices)
        {
            var weaponData = GameFunctions.GetWeaponCSDataFromKey(-1, kvp.Value.ToString());
            _ = WeaponCSDataCache.TryAdd(kvp.Value, weaponData == 0 ? null : new CCSWeaponBaseVDataImpl(weaponData));
        }
    }

    public CCSWeaponBaseVData? GetWeaponCSDataFromKey( int unknown, string key )
    {
        var weaponDataPtr = GameFunctions.GetWeaponCSDataFromKey(unknown, key);
        if(!WeaponCSDataCache.ContainsKey(int.TryParse(key, out var itemDefIndex) ? itemDefIndex : -1))
        {
            _ = WeaponCSDataCache.TryAdd(itemDefIndex, weaponDataPtr == 0 ? null : new CCSWeaponBaseVDataImpl(weaponDataPtr));
        }
        return weaponDataPtr == 0 ? null : (CCSWeaponBaseVData)new CCSWeaponBaseVDataImpl(weaponDataPtr);
    }

    public CCSWeaponBaseVData? GetWeaponCSDataFromKey( int itemDefinitionIndex )
    {
        return WeaponCSDataCache.TryGetValue(itemDefinitionIndex, out var cachedData)
            ? cachedData
            : GetWeaponCSDataFromKey(-1, itemDefinitionIndex.ToString());
    }

    public CCSWeaponBaseVData? GetWeaponCSDataFromKey( ItemDefinitionIndex itemDefinitionIndex )
    {
        return GetWeaponCSDataFromKey((int)itemDefinitionIndex);
    }

    public string? GetClassnameByDefinitionIndex( int itemDefinitionIndex )
    {
        foreach (var kvp in WeaponItemDefinitionIndices)
        {
            if (kvp.Value == itemDefinitionIndex)
            {
                return kvp.Key;
            }
        }
        return null;
    }

    public string? GetClassnameByDefinitionIndex( ItemDefinitionIndex itemDefinitionIndex )
    {
        return GetClassnameByDefinitionIndex((int)itemDefinitionIndex);
    }

    public int? GetDefinitionIndexByClassname( string classname )
    {
        return WeaponItemDefinitionIndices.TryGetValue(classname, out var index) ? index : null;
    }

}
