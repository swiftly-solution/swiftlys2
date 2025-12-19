using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

public enum EntityIOTargetType_t : uint
{
    ENTITY_IO_TARGET_INVALID = 0xFFFFFFFF,
    ENTITY_IO_TARGET_CLASSNAME = 0x0,
    ENTITY_IO_TARGET_CLASSNAME_DERIVES_FROM = 0x1,
    ENTITY_IO_TARGET_ENTITYNAME = 0x2,
    ENTITY_IO_TARGET_CONTAINS_COMPONENT = 0x3,
    ENTITY_IO_TARGET_SPECIAL_ACTIVATOR = 0x4,
    ENTITY_IO_TARGET_SPECIAL_CALLER = 0x5,
    ENTITY_IO_TARGET_EHANDLE = 0x6,
    ENTITY_IO_TARGET_ENTITYNAME_OR_CLASSNAME = 0x7
}

[StructLayout(LayoutKind.Sequential)]
public struct EntityIOConnectionDesc_t
{
    public nint m_targetDesc;
    public nint m_targetInput;
    public nint m_valueOverride;
    public uint m_hTarget;
    public EntityIOTargetType_t m_nTargetType;
    public int m_nTimesToFire;
    public float m_flDelay;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct EntityIOConnection_t
{
    public nint m_targetDesc;
    public nint m_targetInput;
    public nint m_valueOverride;
    public uint m_hTarget;
    public EntityIOTargetType_t m_nTargetType;
    public int m_nTimesToFire;
    public float m_flDelay;

    public bool m_bMarkedForRemoval;
    public EntityIOConnection_t* m_pNext;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct EntityIOOutputDesc_t
{
    public nint m_pName;
    public uint m_nFlags;
    public uint m_nOutputOffset;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct NativeCEntityIOOutput
{
    public void* vtable;
    public EntityIOConnection_t* m_pConnections;
    public EntityIOOutputDesc_t* m_pDesc;
}