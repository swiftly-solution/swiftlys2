
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_ParticleFreezeTransitionOverride : ITypedProtobuf<CUserMsg_ParticleManager_ParticleFreezeTransitionOverride>
{
  static CUserMsg_ParticleManager_ParticleFreezeTransitionOverride ITypedProtobuf<CUserMsg_ParticleManager_ParticleFreezeTransitionOverride>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_ParticleFreezeTransitionOverrideImpl(handle, isManuallyAllocated);


  public float FreezeTransitionOverride { get; set; }

}
