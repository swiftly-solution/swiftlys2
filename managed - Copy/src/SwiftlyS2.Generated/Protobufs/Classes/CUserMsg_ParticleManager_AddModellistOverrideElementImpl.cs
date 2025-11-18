
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_AddModellistOverrideElementImpl : TypedProtobuf<CUserMsg_ParticleManager_AddModellistOverrideElement>, CUserMsg_ParticleManager_AddModellistOverrideElement
{
  public CUserMsg_ParticleManager_AddModellistOverrideElementImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string ModelName
  { get => Accessor.GetString("model_name"); set => Accessor.SetString("model_name", value); }


  public float SpawnProbability
  { get => Accessor.GetFloat("spawn_probability"); set => Accessor.SetFloat("spawn_probability", value); }


  public uint Groupid
  { get => Accessor.GetUInt32("groupid"); set => Accessor.SetUInt32("groupid", value); }

}
