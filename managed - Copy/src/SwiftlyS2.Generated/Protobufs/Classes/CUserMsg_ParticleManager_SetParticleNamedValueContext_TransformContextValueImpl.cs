
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_SetParticleNamedValueContext_TransformContextValueImpl : TypedProtobuf<CUserMsg_ParticleManager_SetParticleNamedValueContext_TransformContextValue>, CUserMsg_ParticleManager_SetParticleNamedValueContext_TransformContextValue
{
  public CUserMsg_ParticleManager_SetParticleNamedValueContext_TransformContextValueImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint ValueNameHash
  { get => Accessor.GetUInt32("value_name_hash"); set => Accessor.SetUInt32("value_name_hash", value); }


  public QAngle Angles
  { get => Accessor.GetQAngle("angles"); set => Accessor.SetQAngle("angles", value); }


  public Vector Translation
  { get => Accessor.GetVector("translation"); set => Accessor.SetVector("translation", value); }

}
