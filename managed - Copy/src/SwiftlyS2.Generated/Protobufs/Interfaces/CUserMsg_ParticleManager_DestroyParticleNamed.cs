
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_DestroyParticleNamed : ITypedProtobuf<CUserMsg_ParticleManager_DestroyParticleNamed>
{
  static CUserMsg_ParticleManager_DestroyParticleNamed ITypedProtobuf<CUserMsg_ParticleManager_DestroyParticleNamed>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_DestroyParticleNamedImpl(handle, isManuallyAllocated);


  public ulong ParticleNameIndex { get; set; }


  public uint EntityHandle { get; set; }


  public bool DestroyImmediately { get; set; }


  public bool PlayEndcap { get; set; }

}
