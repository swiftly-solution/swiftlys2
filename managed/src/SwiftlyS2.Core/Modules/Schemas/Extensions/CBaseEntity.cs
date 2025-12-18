using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.SchemaDefinitions;

public partial interface CBaseEntity
{
    /// <summary>
    /// Gets the subclass-specific data associated with this entity.
    /// </summary>
    public CEntitySubclassVDataBase VData { get; }

    /// <summary>
    /// Gets the absolute origin position of the entity.
    /// </summary>
    public Vector? AbsOrigin { get; }
    /// <summary>
    /// Gets the absolute rotation of the entity.
    /// </summary>
    public QAngle? AbsRotation { get; }

    /// <summary>
    /// Gets the team of the entity.
    /// </summary>
    public Team Team { get; set; }

    /// <summary>
    /// Teleports the entity to the specified position, orientation, and velocity.
    /// </summary>
    /// <remarks>Any parameter set to null will leave the corresponding property of the entity unchanged. This
    /// method can be used to update one or more aspects of the entity's state in a single operation.</remarks>
    /// <param name="position">The target position to move the entity to. If null, the entity's position is not changed.</param>
    /// <param name="angle">The target orientation to set for the entity. If null, the entity's orientation is not changed.</param>
    /// <param name="velocity">The velocity to apply to the entity after teleportation. If null, the entity's velocity is not changed.</param>
    public void Teleport( Vector? position, QAngle? angle, Vector? velocity );

    /// <summary>
    /// Applies damage to the entity based on the specified damage information.
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    /// <param name="dmgInfo">An object containing details about the damage to be applied, including the amount, type, and source. Cannot be null.</param>
    [ThreadUnsafe]
    public void TakeDamage( CTakeDamageInfo dmgInfo );

    /// <summary>
    /// Applies damage to the entity based on the specified damage information asynchronously.
    /// </summary>
    public Task TakeDamageAsync( CTakeDamageInfo dmgInfo );

    /// <summary>
    /// Applies damage to the entity based on the specified damage information.
    /// 
    /// Thread unsafe, use async variant instead for non-main thread context.
    /// </summary>
    [ThreadUnsafe]
    public void TakeDamage( float flDamage, DamageTypes_t bitsDamageType, CBaseEntity? inflictor = null,  CBaseEntity? attacker = null, CBaseEntity? ability = null );

    /// <summary>
    /// Applies damage to the entity based on the specified damage information asynchronously.
    /// </summary>
    public Task TakeDamageAsync( float flDamage, DamageTypes_t bitsDamageType, CBaseEntity? inflictor = null,  CBaseEntity? attacker = null, CBaseEntity? ability = null );

    /// <summary>
    /// Notify the game that the collision rules of the entity have changed.
    /// Call this when you change the Collision of the entity.
    /// </summary>
    public void CollisionRulesChanged();
}