using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct HSCRIPTHandler
{
    public HSCRIPT* Script;
}

[StructLayout(LayoutKind.Sequential)]
public struct HSCRIPT
{
    public int Unused;
}