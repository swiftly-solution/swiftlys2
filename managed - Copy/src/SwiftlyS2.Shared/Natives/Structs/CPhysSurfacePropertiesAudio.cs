using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct CPhysSurfacePropertiesAudioTrace
{
    public float Reflectivity;
    public float HardnessFactor;
    public float RoughnessFactor;
    public float RoughThreshold;
    public float HardThreshold;
    public float HardVelocityThreshold;
    public float StaticImpactVolume;
    public float OcclusionFactor;
}