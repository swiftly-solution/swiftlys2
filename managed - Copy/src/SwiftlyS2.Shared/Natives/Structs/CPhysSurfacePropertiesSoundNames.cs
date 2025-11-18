using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct CPhysSurfacePropertiesSoundNamesTrace
{
    public CUtlString ImpactSoft;
    public CUtlString ImpactHard;
    public CUtlString ScrapeSmooth;
    public CUtlString ScrapeRough;
    public CUtlString BulletImpact;
    public CUtlString Rolling;
    public CUtlString Break;
    public CUtlString Strain;
    public CUtlString MeleeImpact;
    public CUtlString PushOff;
    public CUtlString SkidStop;
}