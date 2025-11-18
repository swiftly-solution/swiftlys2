using SwiftlyS2.Shared.Misc;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

public enum CollisionGroup: byte
{
    /// <summary>
    /// Default layer, always collides with everything.
    /// </summary>
    Always = 0,
    /// <summary>
    /// This is how you turn off all collisions for an object - move it to this group
    /// </summary>
    Nonphysical,
    /// <summary>
    /// Trigger layer, never collides with anything, only triggers/interacts.  Use when querying for interaction layers.
    /// </summary>
    Trigger,
    /// <summary>
    /// Conditionally solid means that the collision response will be zero or as defined in the table when there are matching interactions
    /// </summary>
    ConditionallySolid,
    /// <summary>
    /// First unreserved collision layer index.
    /// </summary>
    FirstUser,
    Default = FirstUser,
    /// <summary>
    /// Collides with nothing but world, static stuff and triggers
    /// </summary>
    Debris,
    /// <summary>
    /// Collides with everything except other interactive debris or debris
    /// </summary>
    InteractiveDebris,
    /// <summary>
    /// Collides with everything except interactive debris or debris
    /// </summary>
    Interactive,
    Player,
    BreakableGlass,
    Vehicle,
    /// <summary>
    /// For HL2, same as Collision_Group_Player, for TF2, this filters out other players and CBaseObjects
    /// </summary>
    PlayerMovement,
    /// <summary>
    /// Generic NPC group
    /// </summary>
    NPC,
    /// <summary>
    /// for any entity inside a vehicle
    /// </summary>
    InVehicle,
    /// <summary>
    /// for any weapons that need collision detection
    /// </summary>
    Weapon,
    /// <summary>
    /// vehicle clip brush to restrict vehicle movement
    /// </summary>
    VehicleClip,
    /// <summary>
    /// Projectiles!
    /// </summary>
    Projectile,
    /// <summary>
    /// Blocks entities not permitted to get near moving doors
    /// </summary>
    DoorBlocker,
    /// <summary>
    /// Doors that the player shouldn't collide with
    /// </summary>
    PassableDoor,
    /// <summary>
    /// Things that are dissolving are in this group
    /// </summary>
    Dissolving,
    /// <summary>
    /// Nonsolid on client and server, pushaway in player code
    /// </summary>
    Pushaway,
    /// <summary>
    /// Used so NPCs in scripts ignore the player.
    /// </summary>
    NPCActor,
    /// <summary>
    /// Used for NPCs in scripts that should not collide with each other
    /// </summary>
    NPCScripted,
    PZClip,
    Props,
    LastSharedCollisionGroup,
    MaxAllowed = 64
}

public enum InteractionLayer: sbyte
{
    ContentsSolid = 0,
    ContentsHitbox,
    ContentsTrigger,
    ContentsSky,
    FirstUser,
    ContentsPlayerClip = FirstUser,
    ContentsNpcClip,
    ContentsBlockLos,
    ContentsBlockLight,
    ContentsLadder,
    ContentsPickup,
    ContentsBlockSound,
    ContentsNoDraw,
    ContentsWindow,
    ContentsPassBullets,
    ContentsWorldGeometry,
    ContentsWater,
    ContentsSlime,
    ContentsTouchAll,
    ContentsPlayer,
    ContentsNpc,
    ContentsDebris,
    ContentsPhysicsProp,
    ContentsNavIgnore,
    ContentsNavLocalIgnore,
    ContentsPostProcessingVolume,
    ContentsUnusedLayer3,
    ContentsCarriedObject,
    ContentsPushaway,
    ContentsServerEntityOnClient,
    ContentsCarriedWeapon,
    ContentsStaticLevel,
    FirstModSpecific,
    ContentsCsgoTeam1 = FirstModSpecific,
    ContentsCsgoTeam2,
    ContentsCsgoGrenadeClip,
    ContentsCsgoDroneClip,
    ContentsCsgoMoveable,
    ContentsCsgoOpaque,
    ContentsCsgoMonster,
    ContentsCsgoUnusedLayer,
    ContentsCsgoThrownGrenade,
    NotFound = -1,
    MaxAllowed = 64,
}

[Flags]
public enum MaskTrace: ulong
{
    Empty = 0ul,
    Solid = 1ul << InteractionLayer.ContentsSolid,
    Hitbox = 1ul << InteractionLayer.ContentsHitbox,
    Trigger = 1ul << InteractionLayer.ContentsTrigger,
    Sky = 1ul << InteractionLayer.ContentsSky,
    PlayerClip = 1ul << InteractionLayer.ContentsPlayerClip,
    NpcClip = 1ul << InteractionLayer.ContentsNpcClip,
    BlockLos = 1ul << InteractionLayer.ContentsBlockLos,
    BlockLight = 1ul << InteractionLayer.ContentsBlockLight,
    Ladder = 1ul << InteractionLayer.ContentsLadder,
    Pickup = 1ul << InteractionLayer.ContentsPickup,
    BlockSound = 1ul << InteractionLayer.ContentsBlockSound,
    NoDraw = 1ul << InteractionLayer.ContentsNoDraw,
    Window = 1ul << InteractionLayer.ContentsWindow,
    PassBullets = 1ul << InteractionLayer.ContentsPassBullets,
    WorldGeometry = 1ul << InteractionLayer.ContentsWorldGeometry,
    Water = 1ul << InteractionLayer.ContentsWater,
    Slime = 1ul << InteractionLayer.ContentsSlime,
    TouchAll = 1ul << InteractionLayer.ContentsTouchAll,
    Player = 1ul << InteractionLayer.ContentsPlayer,
    Npc = 1ul << InteractionLayer.ContentsNpc,
    Debris = 1ul << InteractionLayer.ContentsDebris,
    PhysicsProp = 1ul << InteractionLayer.ContentsPhysicsProp,
    NavIgnore = 1ul << InteractionLayer.ContentsNavIgnore,
    NavLocalIgnore = 1ul << InteractionLayer.ContentsNavLocalIgnore,
    PostProcessingVolume = 1ul << InteractionLayer.ContentsPostProcessingVolume,
    UnusedLayer3 = 1ul << InteractionLayer.ContentsUnusedLayer3,
    CarriedObject = 1ul << InteractionLayer.ContentsCarriedObject,
    Pushaway = 1ul << InteractionLayer.ContentsPushaway,
    ServerEntityOnClient = 1ul << InteractionLayer.ContentsServerEntityOnClient,
    CarriedWeapon = 1ul << InteractionLayer.ContentsCarriedWeapon,
    StaticLevel = 1ul << InteractionLayer.ContentsStaticLevel,
    CsgoTeam1 = 1ul << InteractionLayer.ContentsCsgoTeam1,
    CsgoTeam2 = 1ul << InteractionLayer.ContentsCsgoTeam2,
    CsgoGrenadeClip = 1ul << InteractionLayer.ContentsCsgoGrenadeClip,
    CsgoDroneClip = 1ul << InteractionLayer.ContentsCsgoDroneClip,
    CsgoMoveable = 1ul << InteractionLayer.ContentsCsgoMoveable,
    CsgoOpaque = 1ul << InteractionLayer.ContentsCsgoOpaque,
    CsgoMonster = 1ul << InteractionLayer.ContentsCsgoMonster,
    CsgoUnusedLayer = 1ul << InteractionLayer.ContentsCsgoUnusedLayer,
    CsgoThrownGrenade = 1ul << InteractionLayer.ContentsCsgoThrownGrenade
};

[Flags]
public enum RnQueryObjectSet: byte
{
    Static = 1 << 0,
    Keyframed = 1 << 1,
    Dynamic = 1 << 2,
    Locatable = 1 << 3,

    AllGameEntities = Keyframed | Dynamic | Locatable,
    All = Static | AllGameEntities,
};

[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x2F)]
public unsafe struct RnQueryShapeAttr_t
{
    [FieldOffset(0x0)] public MaskTrace InteractsWith;
    [FieldOffset(0x8)] public MaskTrace InteractsExclude;
    [FieldOffset(0x10)] public MaskTrace InteractsAs;
    [FieldOffset(0x18)] public fixed uint EntityIdsToIgnore[2];
    [FieldOffset(0x20)] public fixed uint OwnerIdsToIgnore[2];
    [FieldOffset(0x28)] public fixed ushort HierarchyIds[2];
    [FieldOffset(0x2C)] public RnQueryObjectSet ObjectSetMask;
    [FieldOffset(0x2D)] public CollisionGroup CollisionGroup;
    [FieldOffset(0x2E)] private byte data;

    public bool HitSolid
    {
        get => BitFieldHelper.GetBit(ref data, 0);
        set => BitFieldHelper.SetBit(ref data, 0, value);
    }

    public bool HitSolidRequiresGenerateContacts
    {
        get => BitFieldHelper.GetBit(ref data, 1);
        set => BitFieldHelper.SetBit(ref data, 1, value);
    }

    public bool HitTrigger
    {
        get => BitFieldHelper.GetBit(ref data, 2);
        set => BitFieldHelper.SetBit(ref data, 2, value);
    }

    public bool ShouldIgnoreDisabledPairs
    {
        get => BitFieldHelper.GetBit(ref data, 3);
        set => BitFieldHelper.SetBit(ref data, 3, value);
    }

    public bool IgnoreIfBothInteractWithHitboxes
    {
        get => BitFieldHelper.GetBit(ref data, 4);
        set => BitFieldHelper.SetBit(ref data, 4, value);
    }

    public bool ForceHitEverything
    {
        get => BitFieldHelper.GetBit(ref data, 5);
        set => BitFieldHelper.SetBit(ref data, 5, value);
    }

    public bool Unknown
    {
        get => BitFieldHelper.GetBit(ref data, 6);
        set => BitFieldHelper.SetBit(ref data, 6, value);
    }
}