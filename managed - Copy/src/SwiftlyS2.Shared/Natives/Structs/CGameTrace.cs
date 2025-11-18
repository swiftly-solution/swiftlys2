using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

public enum RayType_t: byte
{
    RAY_TYPE_LINE = 0,
    RAY_TYPE_SPHERE,
    RAY_TYPE_HULL,
    RAY_TYPE_CAPSULE,
    RAY_TYPE_MESH,
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

    public CEntityInstance Entity => new CEntityInstanceImpl((nint)pEntity);
}