using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[Flags]
public enum ResourceBindingFlags_t : ushort
{
    RESOURCE_BINDING_LOADED = 0x1,
    RESOURCE_BINDING_ERROR = 0x2,
    RESOURCE_BINDING_UNLOADABLE = 0x4,
    RESOURCE_BINDING_PROCEDURAL = 0x8,
    RESOURCE_BINDING_TRACKLEAKS = 0x20,
    RESOURCE_BINDING_IS_ERROR_BINDING_FOR_TYPE = 0x40,
    RESOURCE_BINDING_HAS_EVER_BEEN_LOADED = 0x80,
    RESOURCE_BINDING_ANONYMOUS = 0x100,
    RESOURCE_BINDING_FIRST_UNUSED_FLAG = 0x200
}

[StructLayout(LayoutKind.Sequential)]
public struct ResourceNameInfo
{
    public CUtlSymbolLarge ResourceNameSymbol;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct ResourceHandle
{
    public nint Data;
    public ResourceNameInfo* Name;
    public ResourceBindingFlags_t Flags;
    public ushort ReloadCounter;
    public sbyte TypeIndex;
    public ushort LoadingResource;
    public int RefCount;
    public uint ExtRefHandle;
}