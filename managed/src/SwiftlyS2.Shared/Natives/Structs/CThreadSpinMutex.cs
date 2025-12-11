using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct CThreadSpinMutex
{
    public uint OwnerID;
    public int Depth;
}