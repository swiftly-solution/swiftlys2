using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential, Pack = 4)]
public unsafe struct QuaternionStorage
{
    public fixed byte Padding[16];
}