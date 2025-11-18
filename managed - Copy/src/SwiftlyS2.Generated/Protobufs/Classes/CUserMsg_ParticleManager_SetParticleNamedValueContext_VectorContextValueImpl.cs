
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_SetParticleNamedValueContext_VectorContextValueImpl : TypedProtobuf<CUserMsg_ParticleManager_SetParticleNamedValueContext_VectorContextValue>, CUserMsg_ParticleManager_SetParticleNamedValueContext_VectorContextValue
{
  public CUserMsg_ParticleManager_SetParticleNamedValueContext_VectorContextValueImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint ValueNameHash
  { get => Accessor.GetUInt32("value_name_hash"); set => Accessor.SetUInt32("value_name_hash", value); }


  public Vector Value
  { get => Accessor.GetVector("value"); set => Accessor.SetVector("value", value); }

}
