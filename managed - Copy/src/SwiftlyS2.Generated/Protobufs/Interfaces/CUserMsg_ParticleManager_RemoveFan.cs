
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_RemoveFan : ITypedProtobuf<CUserMsg_ParticleManager_RemoveFan>
{
  static CUserMsg_ParticleManager_RemoveFan ITypedProtobuf<CUserMsg_ParticleManager_RemoveFan>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_RemoveFanImpl(handle, isManuallyAllocated);


}
