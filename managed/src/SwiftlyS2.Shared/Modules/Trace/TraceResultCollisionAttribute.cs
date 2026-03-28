using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.Trace;

public struct TraceCollisionAttributes
{
    public TraceCollisionAttributes()
    {
    }
    /// <summary>
    /// The interaction layer mask defining the types of surfaces or entities that this object interacts as during collision.
    /// </summary>
    public ulong InteractsAs { get; internal set; }
    /// <summary>
    /// The interaction layer mask defining the types of surfaces or entities that this object interacts with during collision.
    /// </summary>
    public ulong InteractsWith { get; internal set; }
    /// <summary>
    /// The interaction layer mask defining the types of surfaces or entities that this object excludes from interaction during collision.
    /// </summary>
    public ulong InteractsExclude { get; internal set; }
    /// <summary>
    /// The entity ID associated with these collision attributes.
    /// </summary>
    public uint EntityId { get; internal set; }
    /// <summary>
    /// The owner entity ID associated with these collision attributes. This is typically the ID of the entity that owns or created this object.
    /// </summary>
    public uint OwnerId { get; internal set; }
    /// <summary>
    /// The hierarchy ID associated with these collision attributes.
    /// </summary>
    public ushort HierarchyId { get; internal set; }
    /// <summary>
    /// The detail layer mask that defines which detail layers are included in collision detection.
    /// </summary>
    public ushort DetailLayerMask { get; internal set; }
    /// <summary>
    /// The type of detail layer mask being used.
    /// </summary>
    public byte DetailLayerMaskType { get; internal set; }
    /// <summary>
    /// The target detail layer for collision detection.
    /// </summary>
    public byte TargetDetailLayer { get; internal set; }
    /// <summary>
    /// The collision group that defines the collision behavior for these attributes.
    /// </summary>
    public CollisionGroup CollisionGroup { get; internal set; }
    /// <summary>
    /// The collision function mask that determines which collision functions apply.
    /// </summary>
    public CollisionFunctionMask_t CollisionFunctionMask { get; internal set; }

    public override string ToString()
    {
        return $"TraceCollisionAttributes {{ InteractsAs: {InteractsAs}, InteractsWith: {InteractsWith}, InteractsExclude: {InteractsExclude}, EntityId: {EntityId}, OwnerId: {OwnerId}, HierarchyId: {HierarchyId}, DetailLayerMask: {DetailLayerMask}, DetailLayerMaskType: {DetailLayerMaskType}, TargetDetailLayer: {TargetDetailLayer}, CollisionGroup: {CollisionGroup}, CollisionFunctionMask: {CollisionFunctionMask} }}";
    }
}