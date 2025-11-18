
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_SetSceneObjectTintAndDesat : ITypedProtobuf<CUserMsg_ParticleManager_SetSceneObjectTintAndDesat>
{
  static CUserMsg_ParticleManager_SetSceneObjectTintAndDesat ITypedProtobuf<CUserMsg_ParticleManager_SetSceneObjectTintAndDesat>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_SetSceneObjectTintAndDesatImpl(handle, isManuallyAllocated);


  public uint Tint { get; set; }


  public float Desat { get; set; }

}
