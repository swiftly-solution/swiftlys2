
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_SetSceneObjectGenericFlag : ITypedProtobuf<CUserMsg_ParticleManager_SetSceneObjectGenericFlag>
{
  static CUserMsg_ParticleManager_SetSceneObjectGenericFlag ITypedProtobuf<CUserMsg_ParticleManager_SetSceneObjectGenericFlag>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_SetSceneObjectGenericFlagImpl(handle, isManuallyAllocated);


  public bool FlagValue { get; set; }

}
