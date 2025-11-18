
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_ClearModellistOverride : ITypedProtobuf<CUserMsg_ParticleManager_ClearModellistOverride>
{
  static CUserMsg_ParticleManager_ClearModellistOverride ITypedProtobuf<CUserMsg_ParticleManager_ClearModellistOverride>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_ClearModellistOverrideImpl(handle, isManuallyAllocated);


  public uint Groupid { get; set; }

}
