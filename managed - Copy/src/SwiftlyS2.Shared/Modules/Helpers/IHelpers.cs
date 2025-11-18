using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Helpers;

public enum ItemDefinitionIndex
{
    // Pistols
    Deagle = 1,
    Elite = 2,
    FiveSeven = 3,
    Glock = 4,
    Tec9 = 30,
    HkP2000 = 32,
    P250 = 36,
    UspSilencer = 61,
    Cz75a = 63,
    Revolver = 64,

    // Rifles
    Ak47 = 7,
    Aug = 8,
    Awp = 9,
    Famas = 10,
    G3sg1 = 11,
    Galilar = 13,
    M249 = 14,
    M4a1 = 16,
    Mac10 = 17,
    P90 = 19,
    Mp5sd = 23,
    Ump45 = 24,
    Xm1014 = 25,
    Bizon = 26,
    Mag7 = 27,
    Negev = 28,
    Sawedoff = 29,
    Mp7 = 33,
    Mp9 = 34,
    Nova = 35,
    Scar20 = 38,
    Sg556 = 39,
    Ssg08 = 40,
    M4a1Silencer = 60,

    // Grenades
    Flashbang = 43,
    Hegrenade = 44,
    Smokegrenade = 45,
    Molotov = 46,
    Decoy = 47,
    Incgrenade = 48,

    // Knives and Equipment
    Taser = 31,
    Knifegg = 41,
    Knife = 42,
    C4 = 49,
    KnifeT = 59,
    Bayonet = 500,
    KnifeCss = 503,
    KnifeFlip = 505,
    KnifeGut = 506,
    KnifeKarambit = 507,
    KnifeM9Bayonet = 508,
    KnifeTactical = 509,
    KnifeFalchion = 512,
    KnifeSurvivalBowie = 514,
    KnifeButterfly = 515,
    KnifePush = 516,
    KnifeCord = 517,
    KnifeCanis = 518,
    KnifeUrsus = 519,
    KnifeGypsyJackknife = 520,
    KnifeOutdoor = 521,
    KnifeStiletto = 522,
    KnifeWidowmaker = 523,
    KnifeSkeleton = 525,
    KnifeKukri = 526,

    // Utility
    ItemKevlar = 50,
    ItemAssaultsuit = 51,
    ItemHeavyassaultsuit = 52,
    ItemDefuser = 55,
    Ammo50ae = 0
}

public interface IHelpers
{
    /// <summary>
    /// Get weapon vdata from key.
    /// </summary>
    /// <param name="unknown">Not sure what this argument is for, but in general it's -1.</param>
    /// <param name="key">The key of the weapon (usually item idx).</param>
    /// <returns>The weapon vdata.</returns>
    public CCSWeaponBaseVData? GetWeaponCSDataFromKey(int unknown, string key);

    /// <summary>
    /// Get weapon vdata from item definition index.
    /// </summary>
    /// <param name="itemDefinitionIndex">The item definition index of the weapon.</param>
    /// <returns>The weapon vdata.</returns>
    public CCSWeaponBaseVData? GetWeaponCSDataFromKey(int itemDefinitionIndex);

    /// <summary>
    /// Get weapon classname from item definition index.
    /// </summary>
    /// <param name="itemDefinitionIndex">The item definition index of the weapon.</param>
    /// <returns>The weapon classname (e.g., "weapon_awp") or null if not found.</returns>
    public string? GetClassnameByDefinitionIndex(int itemDefinitionIndex);

    /// <summary>
    /// Get item definition index from weapon classname.
    /// </summary>
    /// <param name="classname">The weapon classname (e.g., "weapon_awp").</param>
    /// <returns>The item definition index or null if not found.</returns>
    public int? GetDefinitionIndexByClassname(string classname);

}
