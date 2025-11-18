
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_SetParticleNamedValueContext_FloatContextValueImpl : TypedProtobuf<CUserMsg_ParticleManager_SetParticleNamedValueContext_FloatContextValue>, CUserMsg_ParticleManager_SetParticleNamedValueContext_FloatContextValue
{
  public CUserMsg_ParticleManager_SetParticleNamedValueContext_FloatContextValueImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint ValueNameHash
  { get => Accessor.GetUInt32("value_name_hash"); set => Accessor.SetUInt32("value_name_hash", value); }


  public float Value
  { get => Accessor.GetFloat("value"); set => Accessor.SetFloat("value", value); }

}
