
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_SetParticleNamedValueContext_VectorContextValue : ITypedProtobuf<CUserMsg_ParticleManager_SetParticleNamedValueContext_VectorContextValue>
{
  static CUserMsg_ParticleManager_SetParticleNamedValueContext_VectorContextValue ITypedProtobuf<CUserMsg_ParticleManager_SetParticleNamedValueContext_VectorContextValue>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_SetParticleNamedValueContext_VectorContextValueImpl(handle, isManuallyAllocated);


  public uint ValueNameHash { get; set; }


  public Vector Value { get; set; }

}
