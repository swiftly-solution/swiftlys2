
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_SetParticleShouldCheckFoWImpl : TypedProtobuf<CUserMsg_ParticleManager_SetParticleShouldCheckFoW>, CUserMsg_ParticleManager_SetParticleShouldCheckFoW
{
  public CUserMsg_ParticleManager_SetParticleShouldCheckFoWImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool CheckFow
  { get => Accessor.GetBool("check_fow"); set => Accessor.SetBool("check_fow", value); }

}
