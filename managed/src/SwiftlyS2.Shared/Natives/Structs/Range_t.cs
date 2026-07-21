using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct Range_t
{
    public float Min;
    public float Max;

    public float Restrict( float value )
    {
        return Math.Clamp(value, Min, Max);
    }
};