using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct CPhysSurfacePropertiesPhysicsTrace
{
    public float Friction;
    public float Elasticity;
    public float Density;
    public float Thickness;
    public float SoftContactFrequency;
    public float SoftContactDampingRatio;
    public float WheelDrag;
    public float HeatConductivity;
    public float Flashpoint;
}