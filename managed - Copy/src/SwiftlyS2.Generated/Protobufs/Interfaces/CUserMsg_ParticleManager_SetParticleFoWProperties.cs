
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_SetParticleFoWProperties : ITypedProtobuf<CUserMsg_ParticleManager_SetParticleFoWProperties>
{
  static CUserMsg_ParticleManager_SetParticleFoWProperties ITypedProtobuf<CUserMsg_ParticleManager_SetParticleFoWProperties>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_SetParticleFoWPropertiesImpl(handle, isManuallyAllocated);


  public int FowControlPoint { get; set; }


  public int FowControlPoint2 { get; set; }


  public float FowRadius { get; set; }

}
