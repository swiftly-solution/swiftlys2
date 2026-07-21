using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct RnSphere_t
{
    public Vector Center;
    public float Radius;
};