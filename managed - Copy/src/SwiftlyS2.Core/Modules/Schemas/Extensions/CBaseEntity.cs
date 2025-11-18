using SwiftlyS2.Shared.Natives;

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
    /// Teleports the entity to the specified position, orientation, and velocity.
    /// </summary>
    /// <remarks>Any parameter set to null will leave the corresponding property of the entity unchanged. This
    /// method can be used to update one or more aspects of the entity's state in a single operation.</remarks>
    /// <param name="position">The target position to move the entity to. If null, the entity's position is not changed.</param>
    /// <param name="angle">The target orientation to set for the entity. If null, the entity's orientation is not changed.</param>
    /// <param name="velocity">The velocity to apply to the entity after teleportation. If null, the entity's velocity is not changed.</param>
    public void Teleport(Vector? position, QAngle? angle, Vector? velocity);


    /// <summary>
    /// Notify the game that the collision rules of the entity have changed.
    /// Call this when you change the Collision of the entity.
    /// </summary>
    public void CollisionRulesChanged();
}