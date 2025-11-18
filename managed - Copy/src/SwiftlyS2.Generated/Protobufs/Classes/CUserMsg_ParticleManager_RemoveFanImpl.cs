
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_RemoveFanImpl : TypedProtobuf<CUserMsg_ParticleManager_RemoveFan>, CUserMsg_ParticleManager_RemoveFan
{
  public CUserMsg_ParticleManager_RemoveFanImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


}
