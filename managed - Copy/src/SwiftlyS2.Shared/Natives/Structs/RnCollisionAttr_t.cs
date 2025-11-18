using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

public enum CollisionFunctionMask_t: byte
{
    FCOLLISION_FUNC_ENABLE_SOLID_CONTACT = (1 << 0),
    FCOLLISION_FUNC_ENABLE_TRACE_QUERY = (1 << 1),
    FCOLLISION_FUNC_ENABLE_TOUCH_EVENT = (1 << 2),
    FCOLLISION_FUNC_ENABLE_SELF_COLLISIONS = (1 << 3),
    FCOLLISION_FUNC_IGNORE_FOR_HITBOX_TEST = (1 << 4),
    FCOLLISION_FUNC_ENABLE_TOUCH_PERSISTS = (1 << 5),
}

[StructLayout(LayoutKind.Sequential)]
public struct RnCollisionAttr_t
{
    public ulong InteractsAs;
    public ulong InteractsWith;
    public ulong InteractsExclude;
    public uint EntityId;
    public uint OwnerId;
    public ushort HierarchyId;
    public CollisionGroup CollisionGroup;
    public CollisionFunctionMask_t CollisionFunctionMask;
}