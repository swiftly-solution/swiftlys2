
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_DestroyParticleNamedImpl : TypedProtobuf<CUserMsg_ParticleManager_DestroyParticleNamed>, CUserMsg_ParticleManager_DestroyParticleNamed
{
  public CUserMsg_ParticleManager_DestroyParticleNamedImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong ParticleNameIndex
  { get => Accessor.GetUInt64("particle_name_index"); set => Accessor.SetUInt64("particle_name_index", value); }


  public uint EntityHandle
  { get => Accessor.GetUInt32("entity_handle"); set => Accessor.SetUInt32("entity_handle", value); }


  public bool DestroyImmediately
  { get => Accessor.GetBool("destroy_immediately"); set => Accessor.SetBool("destroy_immediately", value); }


  public bool PlayEndcap
  { get => Accessor.GetBool("play_endcap"); set => Accessor.SetBool("play_endcap", value); }

}
