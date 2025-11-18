using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct CPhysSurfacePropertiesTrace
{
    public CUtlString Name;
    public uint NameHash;
    public uint BaseNameHash;
    public int ListIndex;
    public int BaseListIndex;
    public bool Hidden;
    public CUtlString Description;
    public CPhysSurfacePropertiesPhysicsTrace Physics;
    public CPhysSurfacePropertiesSoundNamesTrace AudioSounds;
    public CPhysSurfacePropertiesAudioTrace AudioParams;
}