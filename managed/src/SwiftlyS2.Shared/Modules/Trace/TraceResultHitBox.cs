using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Trace;

public struct HitBoxTrace
{
    public HitBoxTrace()
    {
    }
    /// <summary>
    /// The name of the hitbox.
    /// </summary>
    public string Name { get; internal set; } = "";
    /// <summary>
    /// The surface property name associated with this hitbox.
    /// </summary>
    public string SurfaceProperty { get; internal set; } = "";
    /// <summary>
    /// The name of the bone that this hitbox is attached to.
    /// </summary>
    public string BoneName { get; internal set; } = "";
    /// <summary>
    /// The minimum bounds of the hitbox in 3D space.
    /// </summary>
    public Vector MinBounds { get; internal set; }
    /// <summary>
    /// The maximum bounds of the hitbox in 3D space.
    /// </summary>
    public Vector MaxBounds { get; internal set; }
    /// <summary>
    /// The radius of the shape used for this hitbox.
    /// </summary>
    public float ShapeRadius { get; internal set; }
    /// <summary>
    /// The hashed token of the bone name for faster lookup.
    /// </summary>
    public CUtlStringToken BoneNameHash { get; internal set; }
    /// <summary>
    /// The hit group ID associated with this hitbox, determining damage behavior.
    /// </summary>
    public HitGroup_t GroupId { get; internal set; }
    /// <summary>
    /// The type of shape used for this hitbox.
    /// </summary>
    public byte ShapeType { get; internal set; }
    /// <summary>
    /// Indicates whether this hitbox uses only translation, without rotation.
    /// </summary>
    public bool TranslationOnly { get; internal set; }
    /// <summary>
    /// The CRC checksum of the hitbox data.
    /// </summary>
    public uint CRC { get; internal set; }
    /// <summary>
    /// The render color of the hitbox, typically used for debugging visualization.
    /// </summary>
    public Color RenderColor { get; internal set; }
    /// <summary>
    /// The index of this hitbox within the entity's hitbox list.
    /// </summary>
    public ushort HitBoxIndex { get; internal set; }
    /// <summary>
    /// Indicates whether this hitbox has a forced transform applied.
    /// </summary>
    public bool ForcedTransform { get; internal set; }
    /// <summary>
    /// The forced transform object that defines additional positioning or rotation for this hitbox.
    /// </summary>
    public CTransform ForcedTransformObject { get; internal set; }

    public override string ToString()
    {
        return $"HitBoxTrace {{ Name: \"{Name}\", SurfaceProperty: \"{SurfaceProperty}\", BoneName: \"{BoneName}\", MinBounds: {MinBounds}, MaxBounds: {MaxBounds}, ShapeRadius: {ShapeRadius}, BoneNameHash: {BoneNameHash}, GroupId: {GroupId}, ShapeType: {ShapeType}, TranslationOnly: {TranslationOnly}, CRC: {CRC}, RenderColor: {RenderColor}, HitBoxIndex: {HitBoxIndex}, ForcedTransform: {ForcedTransform}, ForcedTransformObject: {ForcedTransformObject} }}";
    }
}