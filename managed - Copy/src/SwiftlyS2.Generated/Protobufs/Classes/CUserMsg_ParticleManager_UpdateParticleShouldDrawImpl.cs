
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_UpdateParticleShouldDrawImpl : TypedProtobuf<CUserMsg_ParticleManager_UpdateParticleShouldDraw>, CUserMsg_ParticleManager_UpdateParticleShouldDraw
{
  public CUserMsg_ParticleManager_UpdateParticleShouldDrawImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool ShouldDraw
  { get => Accessor.GetBool("should_draw"); set => Accessor.SetBool("should_draw", value); }

}
