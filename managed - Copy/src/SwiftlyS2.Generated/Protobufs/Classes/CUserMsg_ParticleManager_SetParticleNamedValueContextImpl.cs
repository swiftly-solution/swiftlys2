
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_SetParticleNamedValueContextImpl : TypedProtobuf<CUserMsg_ParticleManager_SetParticleNamedValueContext>, CUserMsg_ParticleManager_SetParticleNamedValueContext
{
  public CUserMsg_ParticleManager_SetParticleNamedValueContextImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CUserMsg_ParticleManager_SetParticleNamedValueContext_FloatContextValue> FloatValues
  { get => new ProtobufRepeatedFieldSubMessageType<CUserMsg_ParticleManager_SetParticleNamedValueContext_FloatContextValue>(Accessor, "float_values"); }


  public IProtobufRepeatedFieldSubMessageType<CUserMsg_ParticleManager_SetParticleNamedValueContext_VectorContextValue> VectorValues
  { get => new ProtobufRepeatedFieldSubMessageType<CUserMsg_ParticleManager_SetParticleNamedValueContext_VectorContextValue>(Accessor, "vector_values"); }


  public IProtobufRepeatedFieldSubMessageType<CUserMsg_ParticleManager_SetParticleNamedValueContext_TransformContextValue> TransformValues
  { get => new ProtobufRepeatedFieldSubMessageType<CUserMsg_ParticleManager_SetParticleNamedValueContext_TransformContextValue>(Accessor, "transform_values"); }


  public IProtobufRepeatedFieldSubMessageType<CUserMsg_ParticleManager_SetParticleNamedValueContext_EHandleContext> EhandleValues
  { get => new ProtobufRepeatedFieldSubMessageType<CUserMsg_ParticleManager_SetParticleNamedValueContext_EHandleContext>(Accessor, "ehandle_values"); }

}
