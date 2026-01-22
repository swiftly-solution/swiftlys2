using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CUtlSlot
{
    public fixed byte Mutex[16];
    public CUtlVector<nint> ConnectedSignalers;
}