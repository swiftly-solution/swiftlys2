using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Trace;

public class TraceFilter
{
    /// <summary>
    /// Indicates whether the trace should iterate through entities during the trace operation. 
    /// </summary>
    public bool IterateEntities { get; set; }
    /// <summary>
    /// The interaction layer mask with the entities that the trace should interact with.
    /// </summary>
    public MaskTrace InteractsWith { get; set; }
    /// <summary>
    /// The interaction layer mask with the entities that the trace should not interact with.
    /// </summary>
    public MaskTrace InteractsExclude { get; set; }
    /// <summary>
    /// The interaction layer mask with the entities that the trace should interact as.
    /// </summary>
    public MaskTrace InteractsAs { get; set; }
    /// <summary>
    /// The list of entities that the trace should ignore.
    /// This doesn't get used on TracePlayerBBox.
    /// </summary>
    public List<CEntityInstance> EntitiesToIgnore { get; set; } = [];
    /// <summary>
    /// The list of entity owners that the trace should ignore. This is typically used to ignore entities owned by the player performing the trace, such as their own hitboxes or projectiles.
    /// This doesn't get used on TracePlayerBBox.
    /// </summary>
    public List<CEntityInstance> OwnersToIgnore { get; set; } = [];
    /// <summary>
    /// The list of hierarchy IDs that the trace should consider during the trace operation. Maximum 2, by default they're 0.
    /// </summary>
    public List<ushort> HierarchyIds { get; set; } = [];
    /// <summary>
    /// Not sure what this does
    /// </summary>
    public ushort IncludedDetailLayers { get; set; }
    /// <summary>
    /// Not sure what this does
    /// </summary>
    public byte TargetDetailLayer { get; set; }
    /// <summary>
    /// What kind of objects should the trace query for the conditions specified.
    /// </summary>
    public RnQueryObjectSet ObjectSetMask { get; set; }
    /// <summary>
    /// The collision group that the trace should act as.
    /// </summary>
    public CollisionGroup CollisionGroup { get; set; }
    /// <summary>
    /// If set to true, the trace will report hits with solid entities. This is typically used to allow traces to interact with solid objects in the game world, enabling functionality such as proper collision detection and response when a trace intersects with a solid entity.
    /// </summary>
    public bool HitSolid { get; set; }
    /// <summary>
    /// If set to true, the trace will report hits with solid entities that require generating contacts. This is typically used to allow traces to interact with solid objects in the game world that require contact generation for accurate collision detection and response, enabling functionality such as proper physics interactions and realistic hit reactions when a trace intersects
    /// </summary>
    public bool HitSolidRequiresGenerateContacts { get; set; }
    /// <summary>
    /// If set to true, the trace will report hits with trigger entities. This is typically used to allow traces to interact with trigger volumes or areas that are designed to detect when entities enter or exit them, enabling functionality such as triggering events or activating certain behaviors when a trace intersects with a trigger entity.
    /// </summary>
    public bool HitTrigger { get; set; }
    /// <summary>
    /// If set to true, the trace will ignore hits with disabled entities. This is typically used to prevent traces from hitting entities that are currently disabled, allowing for more accurate collision detection in scenarios where certain entities may be temporarily inactive or non-collidable.
    /// </summary>
    public bool ShouldIgnoreDisabledPairs { get; set; }
    /// <summary>
    /// If set to true, the trace will ignore hits with hitboxes if both the entity being traced and the entity being hit interact with hitboxes. This is typically used to prevent traces from hitting hitboxes when both entities are designed to interact with them, allowing for more accurate collision detection in certain scenarios.
    /// </summary>
    public bool IgnoreIfBothInteractWithHitboxes { get; set; }
    /// <summary>
    /// Force the trace to hit everything
    /// </summary>
    public bool ForceHitEverything { get; set; }
    /// <summary>
    /// A custom callback to check if the trace should hit a specific entity.
    /// This doesn't get called on TracePlayerBBox.
    /// </summary>
    public Func<CEntityInstance, bool>? ShouldHitEntity { get; set; } = null;

    public override string ToString()
    {
        return $"TraceFilter {{ IterateEntities: {IterateEntities}, InteractsWith: {InteractsWith}, InteractsExclude: {InteractsExclude}, InteractsAs: {InteractsAs}, EntitiesToIgnore: [{string.Join(", ", EntitiesToIgnore)}], OwnersToIgnore: [{string.Join(", ", OwnersToIgnore)}], HierarchyIds: [{string.Join(", ", HierarchyIds)}], IncludedDetailLayers: {IncludedDetailLayers}, TargetDetailLayer: {TargetDetailLayer}, ObjectSetMask: {ObjectSetMask}, CollisionGroup: {CollisionGroup}, HitSolid: {HitSolid}, HitSolidRequiresGenerateContacts: {HitSolidRequiresGenerateContacts}, HitTrigger: {HitTrigger}, ShouldIgnoreDisabledPairs: {ShouldIgnoreDisabledPairs}, IgnoreIfBothInteractWithHitboxes: {IgnoreIfBothInteractWithHitboxes}, ForceHitEverything: {ForceHitEverything} }}";
    }
}