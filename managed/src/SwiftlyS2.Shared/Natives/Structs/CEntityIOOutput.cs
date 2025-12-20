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
    public nint TargetDesc;
    public nint TargetInput;
    public nint ValueOverride;
    public uint Target;
    public EntityIOTargetType_t TargetType;
    public int TimesToFire;
    public float Delay;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct EntityIOConnection_t
{
    public EntityIOConnectionDesc_t Desc;
    public bool MarkedForRemoval;
    public EntityIOConnection_t* Next;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct EntityIOOutputDesc_t
{
    public CString Name;
    public uint Flags;
    public uint OutputOffset;
}

[StructLayout(LayoutKind.Sequential)]
public readonly unsafe struct CEntityIOOutput
{
    private readonly void* vtable;
    private readonly EntityIOConnection_t* connections;
    private readonly EntityIOOutputDesc_t* desc;

    public readonly ref EntityIOConnection_t Connections => ref *connections;
    public readonly ref EntityIOOutputDesc_t Desc => ref *desc;
}
