
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_CreateParticleImpl : TypedProtobuf<CUserMsg_ParticleManager_CreateParticle>, CUserMsg_ParticleManager_CreateParticle
{
  public CUserMsg_ParticleManager_CreateParticleImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong ParticleNameIndex
  { get => Accessor.GetUInt64("particle_name_index"); set => Accessor.SetUInt64("particle_name_index", value); }


  public int AttachType
  { get => Accessor.GetInt32("attach_type"); set => Accessor.SetInt32("attach_type", value); }


  public uint EntityHandle
  { get => Accessor.GetUInt32("entity_handle"); set => Accessor.SetUInt32("entity_handle", value); }


  public uint EntityHandleForModifiers
  { get => Accessor.GetUInt32("entity_handle_for_modifiers"); set => Accessor.SetUInt32("entity_handle_for_modifiers", value); }


  public bool ApplyVoiceBanRules
  { get => Accessor.GetBool("apply_voice_ban_rules"); set => Accessor.SetBool("apply_voice_ban_rules", value); }


  public int TeamBehavior
  { get => Accessor.GetInt32("team_behavior"); set => Accessor.SetInt32("team_behavior", value); }


  public string ControlPointConfiguration
  { get => Accessor.GetString("control_point_configuration"); set => Accessor.SetString("control_point_configuration", value); }


  public bool Cluster
  { get => Accessor.GetBool("cluster"); set => Accessor.SetBool("cluster", value); }


  public float EndcapTime
  { get => Accessor.GetFloat("endcap_time"); set => Accessor.SetFloat("endcap_time", value); }


  public Vector AggregationPosition
  { get => Accessor.GetVector("aggregation_position"); set => Accessor.SetVector("aggregation_position", value); }

}
