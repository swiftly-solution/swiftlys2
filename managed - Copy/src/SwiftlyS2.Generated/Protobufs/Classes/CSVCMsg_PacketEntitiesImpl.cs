
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_PacketEntitiesImpl : NetMessage<CSVCMsg_PacketEntities>, CSVCMsg_PacketEntities
{
  public CSVCMsg_PacketEntitiesImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int MaxEntries
  { get => Accessor.GetInt32("max_entries"); set => Accessor.SetInt32("max_entries", value); }


  public int UpdatedEntries
  { get => Accessor.GetInt32("updated_entries"); set => Accessor.SetInt32("updated_entries", value); }


  public bool LegacyIsDelta
  { get => Accessor.GetBool("legacy_is_delta"); set => Accessor.SetBool("legacy_is_delta", value); }


  public bool UpdateBaseline
  { get => Accessor.GetBool("update_baseline"); set => Accessor.SetBool("update_baseline", value); }


  public int Baseline
  { get => Accessor.GetInt32("baseline"); set => Accessor.SetInt32("baseline", value); }


  public int DeltaFrom
  { get => Accessor.GetInt32("delta_from"); set => Accessor.SetInt32("delta_from", value); }


  public byte[] EntityData
  { get => Accessor.GetBytes("entity_data"); set => Accessor.SetBytes("entity_data", value); }


  public bool PendingFullFrame
  { get => Accessor.GetBool("pending_full_frame"); set => Accessor.SetBool("pending_full_frame", value); }


  public uint ActiveSpawngroupHandle
  { get => Accessor.GetUInt32("active_spawngroup_handle"); set => Accessor.SetUInt32("active_spawngroup_handle", value); }


  public uint MaxSpawngroupCreationsequence
  { get => Accessor.GetUInt32("max_spawngroup_creationsequence"); set => Accessor.SetUInt32("max_spawngroup_creationsequence", value); }


  public uint LastCmdNumberExecuted
  { get => Accessor.GetUInt32("last_cmd_number_executed"); set => Accessor.SetUInt32("last_cmd_number_executed", value); }


  public int LastCmdNumberRecvDelta
  { get => Accessor.GetInt32("last_cmd_number_recv_delta"); set => Accessor.SetInt32("last_cmd_number_recv_delta", value); }


  public uint ServerTick
  { get => Accessor.GetUInt32("server_tick"); set => Accessor.SetUInt32("server_tick", value); }


  public byte[] SerializedEntities
  { get => Accessor.GetBytes("serialized_entities"); set => Accessor.SetBytes("serialized_entities", value); }


  public IProtobufRepeatedFieldSubMessageType<CSVCMsg_PacketEntities_alternate_baseline_t> AlternateBaselines
  { get => new ProtobufRepeatedFieldSubMessageType<CSVCMsg_PacketEntities_alternate_baseline_t>(Accessor, "alternate_baselines"); }


  public uint HasPvsVisBitsDeprecated
  { get => Accessor.GetUInt32("has_pvs_vis_bits_deprecated"); set => Accessor.SetUInt32("has_pvs_vis_bits_deprecated", value); }


  public IProtobufRepeatedFieldValueType<int> CmdRecvStatus
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "cmd_recv_status"); }


  public CSVCMsg_PacketEntities_non_transmitted_entities_t NonTransmittedEntities
  { get => new CSVCMsg_PacketEntities_non_transmitted_entities_tImpl(NativeNetMessages.GetNestedMessage(Address, "non_transmitted_entities"), false); }


  public uint CqStarvedCommandTicks
  { get => Accessor.GetUInt32("cq_starved_command_ticks"); set => Accessor.SetUInt32("cq_starved_command_ticks", value); }


  public uint CqDiscardedCommandTicks
  { get => Accessor.GetUInt32("cq_discarded_command_ticks"); set => Accessor.SetUInt32("cq_discarded_command_ticks", value); }


  public CSVCMsg_PacketEntities_outofpvs_entity_updates_t OutofpvsEntityUpdates
  { get => new CSVCMsg_PacketEntities_outofpvs_entity_updates_tImpl(NativeNetMessages.GetNestedMessage(Address, "outofpvs_entity_updates"), false); }


  public byte[] DevPadding
  { get => Accessor.GetBytes("dev_padding"); set => Accessor.SetBytes("dev_padding", value); }

}
