using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct ChangeAccessorFieldPathIndex_t
{
    public int Value;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CNetworkVarChainer
{
    public void* pEntity;
    private fixed byte _padding[24];
    public ChangeAccessorFieldPathIndex_t PathIndex;
    private fixed byte _padding2[4];

    public readonly CEntityInstance Entity => new CEntityInstanceImpl((nint)pEntity);
}