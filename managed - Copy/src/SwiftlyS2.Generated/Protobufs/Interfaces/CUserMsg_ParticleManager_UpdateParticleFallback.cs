
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_UpdateParticleFallback : ITypedProtobuf<CUserMsg_ParticleManager_UpdateParticleFallback>
{
  static CUserMsg_ParticleManager_UpdateParticleFallback ITypedProtobuf<CUserMsg_ParticleManager_UpdateParticleFallback>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_UpdateParticleFallbackImpl(handle, isManuallyAllocated);


  public int ControlPoint { get; set; }


  public Vector Position { get; set; }

}
