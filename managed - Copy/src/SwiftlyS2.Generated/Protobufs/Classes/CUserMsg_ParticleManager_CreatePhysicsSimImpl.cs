
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_CreatePhysicsSimImpl : TypedProtobuf<CUserMsg_ParticleManager_CreatePhysicsSim>, CUserMsg_ParticleManager_CreatePhysicsSim
{
  public CUserMsg_ParticleManager_CreatePhysicsSimImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string PropGroupName
  { get => Accessor.GetString("prop_group_name"); set => Accessor.SetString("prop_group_name", value); }


  public bool UseHighQualitySimulation
  { get => Accessor.GetBool("use_high_quality_simulation"); set => Accessor.SetBool("use_high_quality_simulation", value); }


  public uint MaxParticleCount
  { get => Accessor.GetUInt32("max_particle_count"); set => Accessor.SetUInt32("max_particle_count", value); }

}
