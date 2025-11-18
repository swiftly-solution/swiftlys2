
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager_SetParticleNamedValueContext : ITypedProtobuf<CUserMsg_ParticleManager_SetParticleNamedValueContext>
{
  static CUserMsg_ParticleManager_SetParticleNamedValueContext ITypedProtobuf<CUserMsg_ParticleManager_SetParticleNamedValueContext>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManager_SetParticleNamedValueContextImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CUserMsg_ParticleManager_SetParticleNamedValueContext_FloatContextValue> FloatValues { get; }


  public IProtobufRepeatedFieldSubMessageType<CUserMsg_ParticleManager_SetParticleNamedValueContext_VectorContextValue> VectorValues { get; }


  public IProtobufRepeatedFieldSubMessageType<CUserMsg_ParticleManager_SetParticleNamedValueContext_TransformContextValue> TransformValues { get; }


  public IProtobufRepeatedFieldSubMessageType<CUserMsg_ParticleManager_SetParticleNamedValueContext_EHandleContext> EhandleValues { get; }

}
