
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_SetMaterialOverride : ITypedProtobuf<CUserMsg_ParticleManager_SetMaterialOverride>
{
  static CUserMsg_ParticleManager_SetMaterialOverride ITypedProtobuf<CUserMsg_ParticleManager_SetMaterialOverride>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_SetMaterialOverrideImpl(handle, isManuallyAllocated);


  public string MaterialName { get; set; }


  public bool IncludeChildren { get; set; }

}
