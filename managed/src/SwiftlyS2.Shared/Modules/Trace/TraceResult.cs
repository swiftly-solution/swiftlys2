using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Shared.Trace;

public struct TraceResult
{
    public TraceResult()
    {
    }
    /// <summary>
    /// The physical surface properties of the surface that was hit by the trace.
    /// </summary>
    public PhysSurfacePropertiesTrace? SurfaceProperties { get; internal set; } = null;
    /// <summary>
    /// Information about the hitbox that was hit by the trace, if applicable.
    /// </summary>
    public HitBoxTrace? HitBox { get; internal set; } = null;
    /// <summary>
    /// The contents mask of the surface or volume that was hit.
    /// </summary>
    public ulong Contents { get; internal set; }
    /// <summary>
    /// The transformation matrix of the body that was hit.
    /// </summary>
    public CTransform BodyTransform { get; internal set; }
    /// <summary>
    /// The collision attributes of the shape that was hit.
    /// </summary>
    public TraceCollisionAttributes ShapeAttributes { get; internal set; } = new();
    /// <summary>
    /// The starting position of the trace.
    /// </summary>
    public Vector StartPos { get; internal set; }
    /// <summary>
    /// The ending position of the trace.
    /// </summary>
    public Vector EndPos { get; internal set; }
    /// <summary>
    /// The surface normal of the point where the trace hit.
    /// </summary>
    public Vector HitNormal { get; internal set; }
    /// <summary>
    /// The exact point in 3D space where the trace hit a surface or entity.
    /// </summary>
    public Vector HitPoint { get; internal set; }
    /// <summary>
    /// The distance from the trace start position to the hit point along the trace direction.
    /// </summary>
    public float HitOffset { get; internal set; }
    /// <summary>
    /// The fraction of the trace distance traveled before hitting something (0.0 = start, 1.0 = end, > 1.0 = no hit).
    /// </summary>
    public float Fraction { get; internal set; }
    /// <summary>
    /// The triangle index of the surface that was hit, if applicable.
    /// </summary>
    public int Triangle { get; internal set; }
    /// <summary>
    /// The bone index of the hitbox that was hit, if applicable.
    /// </summary>
    public short HitboxBoneIndex { get; internal set; }
    /// <summary>
    /// The type of ray that was used for this trace.
    /// </summary>
    public RayType_t RayType { get; internal set; }
    /// <summary>
    /// Indicates whether the starting position of the trace was already inside a solid object.
    /// </summary>
    public bool StartInSolid { get; internal set; }
    /// <summary>
    /// Indicates whether the exact hit point was calculated for this trace result.
    /// </summary>
    public bool ExactHitPoint { get; internal set; }
    /// <summary>
    /// The entity that was hit by the trace, if applicable. Null if nothing or a static surface was hit.
    /// </summary>
    public CEntityInstance? Entity { get; internal set; } = null;
    /// <summary>
    /// Returns true if the trace hit something (either the trace fraction is less than 1.0 or the start position was in solid).
    /// </summary>
    public bool DidHit => Fraction < 1.0f || StartInSolid;
    /// <summary>
    /// Calculates the total distance traveled from the start position to the end position.
    /// </summary>
    public float Distance => EndPos.Distance(StartPos);
    /// <summary>
    /// Calculates the normalized direction vector of the trace from start to end.
    /// </summary>
    public Vector Direction {
        get {
            var dir = EndPos - StartPos;
            dir.Normalize();
            return dir;
        }
    }

    public bool HitEntityByDesignerName<T>( string designerName, out T? outEntity, NameMatchType matchType = NameMatchType.StartsWith ) where T : class, ISchemaClass<T>
    {
        outEntity = null;

        if (!DidHit)
        {
            return false;
        }

        if (Entity == null || !Entity.IsValid)
        {
            return false;
        }

        var typedEntity = Entity.As<T>();
        if (!typedEntity.IsValid)
        {
            return false;
        }

        var isMatch = matchType switch {
            NameMatchType.Exact => Entity.DesignerName.Equals(designerName, StringComparison.OrdinalIgnoreCase),
            NameMatchType.StartsWith => Entity.DesignerName.StartsWith(designerName, StringComparison.OrdinalIgnoreCase),
            NameMatchType.EndsWith => Entity.DesignerName.EndsWith(designerName, StringComparison.OrdinalIgnoreCase),
            NameMatchType.Contains => Entity.DesignerName.Contains(designerName, StringComparison.OrdinalIgnoreCase),
            _ => false
        };

        if (isMatch)
        {
            outEntity = typedEntity;
        }

        return isMatch;
    }

    public bool HitEntityByDesignerName<T>( string designerName, NameMatchType matchType = NameMatchType.StartsWith ) where T : class, ISchemaClass<T>
    {
        return HitEntityByDesignerName<T>(designerName, out _, matchType);
    }

    public bool HitPlayer( out IPlayer? player )
    {
        player = null;

        if (!DidHit || Entity == null || !Entity.IsValid)
        {
            return false;
        }

        if (!Entity.DesignerName?.StartsWith("player", StringComparison.OrdinalIgnoreCase) == true)
        {
            return false;
        }

        var playerPawn = Entity.As<CCSPlayerPawn>();
        if (!playerPawn.IsValid)
        {
            return false;
        }

        player = playerPawn.ToPlayer();
        return player != null;
    }

    public bool HitPlayer()
    {
        return HitPlayer(out _);
    }

    public bool HitEntity<T>( out T? entity ) where T : class, ISchemaClass<T>
    {
        entity = null;
        return T.ClassName != null && HitEntityByDesignerName(T.ClassName, out entity, NameMatchType.Exact);
    }

    public bool HitEntity<T>() where T : class, ISchemaClass<T>
    {
        return HitEntity<T>(out _);
    }

    public override string ToString()
    {
        return $"TraceResult {{ SurfaceProperties: {SurfaceProperties}, HitBox: {HitBox}, Contents: {Contents}, BodyTransform: {BodyTransform}, ShapeAttributes: {ShapeAttributes}, StartPos: {StartPos}, EndPos: {EndPos}, HitNormal: {HitNormal}, HitPoint: {HitPoint}, HitOffset: {HitOffset}, Fraction: {Fraction}, Triangle: {Triangle}, HitboxBoneIndex: {HitboxBoneIndex}, RayType: {RayType}, StartInSolid: {StartInSolid}, ExactHitPoint: {ExactHitPoint}, Entity: {(Entity != null && Entity.IsValid ? Entity.DesignerName : "null")} }}";
    }
}