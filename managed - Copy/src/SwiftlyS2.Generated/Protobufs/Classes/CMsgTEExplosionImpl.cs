
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTEExplosionImpl : NetMessage<CMsgTEExplosion>, CMsgTEExplosion
{
  public CMsgTEExplosionImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public Vector Origin
  { get => Accessor.GetVector("origin"); set => Accessor.SetVector("origin", value); }


  public uint Flags
  { get => Accessor.GetUInt32("flags"); set => Accessor.SetUInt32("flags", value); }


  public Vector Normal
  { get => Accessor.GetVector("normal"); set => Accessor.SetVector("normal", value); }


  public uint Radius
  { get => Accessor.GetUInt32("radius"); set => Accessor.SetUInt32("radius", value); }


  public uint Magnitude
  { get => Accessor.GetUInt32("magnitude"); set => Accessor.SetUInt32("magnitude", value); }


  public bool AffectRagdolls
  { get => Accessor.GetBool("affect_ragdolls"); set => Accessor.SetBool("affect_ragdolls", value); }


  public string SoundName
  { get => Accessor.GetString("sound_name"); set => Accessor.SetString("sound_name", value); }


  public uint ExplosionType
  { get => Accessor.GetUInt32("explosion_type"); set => Accessor.SetUInt32("explosion_type", value); }


  public bool CreateDebris
  { get => Accessor.GetBool("create_debris"); set => Accessor.SetBool("create_debris", value); }


  public Vector DebrisOrigin
  { get => Accessor.GetVector("debris_origin"); set => Accessor.SetVector("debris_origin", value); }


  public uint DebrisSurfaceprop
  { get => Accessor.GetUInt32("debris_surfaceprop"); set => Accessor.SetUInt32("debris_surfaceprop", value); }

}
