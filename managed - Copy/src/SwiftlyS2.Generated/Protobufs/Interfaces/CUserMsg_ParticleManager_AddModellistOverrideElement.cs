
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_AddModellistOverrideElement : ITypedProtobuf<CUserMsg_ParticleManager_AddModellistOverrideElement>
{
  static CUserMsg_ParticleManager_AddModellistOverrideElement ITypedProtobuf<CUserMsg_ParticleManager_AddModellistOverrideElement>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_AddModellistOverrideElementImpl(handle, isManuallyAllocated);


  public string ModelName { get; set; }


  public float SpawnProbability { get; set; }


  public uint Groupid { get; set; }

}
