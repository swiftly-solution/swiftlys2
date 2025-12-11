using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct CCircularBuffer
{
    public int Count;
    public int Read;
    public int Write;
    public int Size;
    public nint Data;
}