
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_SetParticleTextImpl : TypedProtobuf<CUserMsg_ParticleManager_SetParticleText>, CUserMsg_ParticleManager_SetParticleText
{
  public CUserMsg_ParticleManager_SetParticleTextImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string Text
  { get => Accessor.GetString("text"); set => Accessor.SetString("text", value); }

}
