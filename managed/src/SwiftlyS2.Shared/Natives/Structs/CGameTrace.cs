using System.Runtime.InteropServices;
using SwiftlyS2.Core.Players;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.Schemas;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Natives;

public enum RayType_t : byte
{
    RAY_TYPE_LINE = 0,
    RAY_TYPE_SPHERE,
    RAY_TYPE_HULL,
    RAY_TYPE_CAPSULE,
    RAY_TYPE_MESH,
}

public enum NameMatchType
{
    Exact = 0,
    StartsWith = 1,
    EndsWith = 2,
    Contains = 3
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CGameTrace
{
    public CPhysSurfacePropertiesTrace* SurfaceProperties;
    public void* pEntity;
    public CHitBoxTrace* HitBox;

    public void* Body;
    public void* Shape;

    public ulong Contents;
    public CTransform BodyTransform;
    public RnCollisionAttr_t ShapeAttributes;

    public Vector StartPos;
    public Vector EndPos;
    public Vector HitNormal;
    public Vector HitPoint;

    public float HitOffset;
    public float Fraction;

    public int Triangle;
    public short HitboxBoneIndex;

    public RayType_t RayType;

    public bool StartInSolid;
    public bool ExactHitPoint;

    public readonly CEntityInstance Entity => new CEntityInstanceImpl((nint)pEntity);

    public readonly bool DidHit => Fraction < 1.0f || StartInSolid;
    public readonly float Distance => EndPos.Distance(StartPos);
    public readonly Vector Direction {
        get {
            var dir = EndPos - StartPos;
            dir.Normalize();
            return dir;
        }
    }

    public readonly bool HitEntityByDesignerName<T>( string designerName, out T outEntity, NameMatchType matchType = NameMatchType.StartsWith ) where T : ISchemaClass<T>
    {
        outEntity = T.From(IntPtr.Zero);

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

    public readonly bool HitEntityByDesignerName<T>( string designerName, NameMatchType matchType = NameMatchType.StartsWith ) where T : ISchemaClass<T>
    {
        return HitEntityByDesignerName<T>(designerName, out _, matchType);
    }

    public readonly bool HitPlayer( out IPlayer? player )
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

        var controller = playerPawn.OriginalController;
        if (!controller.IsValid)
        {
            return false;
        }

        player = new Player((int)(controller.Value!.Index - 1));
        return true;
    }

    public readonly bool HitPlayer()
    {
        return HitPlayer(out _);
    }

    public readonly bool HitEntity<T>( out T entity ) where T : ISchemaClass<T>
    {
        entity = T.From(IntPtr.Zero);
        return T.ClassName != null && HitEntityByDesignerName(T.ClassName, out entity, NameMatchType.Exact);
    }

    public readonly bool HitEntity<T>() where T : ISchemaClass<T>
    {
        return HitEntity<T>(out _);
    }
}