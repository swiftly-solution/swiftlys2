using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;


[StructLayout(LayoutKind.Sequential)]
public struct AttackerInfo_t
{
    public bool NeedInit;
    public bool IsPawn;
    public bool IsWorld;
    public CHandle<CCSPlayerPawn> AttackerPawn;
    public int AttackerPlayerSlot;
    public int TeamChecked;
    public int Team;
};

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CTakeDamageInfo
{
    public nint _pVTable;

    public Vector DamageForce;
    public Vector DamagePosition;
    public Vector ReportedPosition;
    public Vector DamageDirection;
    public CHandle<CEntityInstance> Inflictor;
    public CHandle<CEntityInstance> Attacker;
    public CHandle<CEntityInstance> Ability;
    public float Damage;
    public float TotalledDamage;
    public DamageTypes_t DamageType;
    public uint DamageCustom;
    public sbyte AmmoType;

    private fixed byte _padding1[0xb];

    public float OriginalDamage;
    public bool ShouldBleed;
    public bool ShouldSpark;

    private short _padding2;

    public CGameTrace* Trace;
    public TakeDamageFlags_t DamageFlags;
    public CString DamageSourceName;

    /// <see cref="ActualHitGroup"/>
    [Obsolete("This field somehow holds garbage value in game. Use ActualHitGroup instead.")]
    public HitGroup_t HitGroupId;
    public int NumObjectsPenetrated;
    public float FriendlyFireDamageReductionRatio;

    private fixed byte _padding3[0x5C];

    public void* ScriptInstance;
    public AttackerInfo_t AttackerInfo;
    private fixed byte _padding4[0x1C];
    public bool InTakeDamageFlow;

    private int Unknown;

    public CTakeDamageInfo()
    {
        Vector vec3_origin = Vector.Zero;

        fixed (CTakeDamageInfo* info = &this)
        {
            GameFunctions.CTakeDamageInfoConstructor(info, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, &vec3_origin, &vec3_origin, 0.0f, 0, 0, null);
        }
    }

    public CTakeDamageInfo( CBaseEntity inflictor, CBaseEntity attacker, CBaseEntity ability, float flDamage, DamageTypes_t bitsDamageType )
    {
        Vector vec3_origin = Vector.Zero;

        fixed (CTakeDamageInfo* info = &this)
        {
            GameFunctions.CTakeDamageInfoConstructor(info, inflictor.Address, attacker.Address, ability.Address, &vec3_origin, &vec3_origin, flDamage, (int)bitsDamageType, 0, null);
        }
    }

    public CTakeDamageInfo( float flDamage, DamageTypes_t bitsDamageType, CBaseEntity? inflictor = null, CBaseEntity? attacker = null, CBaseEntity? ability = null )
    {
        Vector vec3_origin = Vector.Zero;

        fixed (CTakeDamageInfo* info = &this)
        {
            GameFunctions.CTakeDamageInfoConstructor(info, inflictor?.Address ?? IntPtr.Zero, attacker?.Address ?? IntPtr.Zero, ability?.Address ?? IntPtr.Zero, &vec3_origin, &vec3_origin, flDamage, (int)bitsDamageType, 0, null);
        }
    }

    public HitGroup_t ActualHitGroup => Trace->HitBox->m_nGroupId;
}

[StructLayout(LayoutKind.Sequential, Pack = 8, Size = 40)]
public unsafe struct CTakeDamageResult
{
    public CTakeDamageInfo* OriginatingInfo;
    public int HealthLost;
    public int HealthBefore;
    public int DamageDealt;
    public float PreModifiedDamage;
    public int TotalledHealthLost;
    public int TotalledDamageDealt;
    public bool WasDamageSuppressed;
}