
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_SetParticleNamedValueContext_TransformContextValue : ITypedProtobuf<CUserMsg_ParticleManager_SetParticleNamedValueContext_TransformContextValue>
{
  static CUserMsg_ParticleManager_SetParticleNamedValueContext_TransformContextValue ITypedProtobuf<CUserMsg_ParticleManager_SetParticleNamedValueContext_TransformContextValue>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_SetParticleNamedValueContext_TransformContextValueImpl(handle, isManuallyAllocated);


  public uint ValueNameHash { get; set; }


  public QAngle Angles { get; set; }


  public Vector Translation { get; set; }

}
