
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_ParticleSkipToTimeImpl : TypedProtobuf<CUserMsg_ParticleManager_ParticleSkipToTime>, CUserMsg_ParticleManager_ParticleSkipToTime
{
  public CUserMsg_ParticleManager_ParticleSkipToTimeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public float SkipToTime
  { get => Accessor.GetFloat("skip_to_time"); set => Accessor.SetFloat("skip_to_time", value); }

}
