using SwiftlyS2.Shared.SchemaDefinitions;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct CHitBoxTrace
{
    public CUtlString m_name;
    public CUtlString m_sSurfaceProperty;
    public CUtlString m_sBoneName;
    public Vector m_vMinBounds;
    public Vector m_vMaxBounds;
    public float m_flShapeRadius;
    public CUtlStringToken m_nBoneNameHash;
    public HitGroup_t m_nGroupId;
    public byte m_nShapeType;
    public bool m_bTranslationOnly;
    public uint m_CRC;
    public Color m_cRenderColor;
    public ushort m_nHitBoxIndex;
    public bool m_bForcedTransform;
    public CTransform m_forcedTransform;
}