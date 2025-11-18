
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_MatchmakingServerReservationResponseImpl : TypedProtobuf<CMsgGCCStrike15_v2_MatchmakingServerReservationResponse>, CMsgGCCStrike15_v2_MatchmakingServerReservationResponse
{
  public CMsgGCCStrike15_v2_MatchmakingServerReservationResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Reservationid
  { get => Accessor.GetUInt64("reservationid"); set => Accessor.SetUInt64("reservationid", value); }


  public CMsgGCCStrike15_v2_MatchmakingGC2ServerReserve Reservation
  { get => new CMsgGCCStrike15_v2_MatchmakingGC2ServerReserveImpl(NativeNetMessages.GetNestedMessage(Address, "reservation"), false); }


  public string Map
  { get => Accessor.GetString("map"); set => Accessor.SetString("map", value); }


  public ulong GcReservationSent
  { get => Accessor.GetUInt64("gc_reservation_sent"); set => Accessor.SetUInt64("gc_reservation_sent", value); }


  public uint ServerVersion
  { get => Accessor.GetUInt32("server_version"); set => Accessor.SetUInt32("server_version", value); }


  public ServerHltvInfo TvInfo
  { get => new ServerHltvInfoImpl(NativeNetMessages.GetNestedMessage(Address, "tv_info"), false); }


  public IProtobufRepeatedFieldValueType<uint> RewardPlayerAccounts
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "reward_player_accounts"); }


  public IProtobufRepeatedFieldValueType<uint> IdlePlayerAccounts
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "idle_player_accounts"); }


  public uint RewardItemAttrDefIdx
  { get => Accessor.GetUInt32("reward_item_attr_def_idx"); set => Accessor.SetUInt32("reward_item_attr_def_idx", value); }


  public uint RewardItemAttrValue
  { get => Accessor.GetUInt32("reward_item_attr_value"); set => Accessor.SetUInt32("reward_item_attr_value", value); }


  public uint RewardItemAttrRewardIdx
  { get => Accessor.GetUInt32("reward_item_attr_reward_idx"); set => Accessor.SetUInt32("reward_item_attr_reward_idx", value); }


  public uint RewardDropList
  { get => Accessor.GetUInt32("reward_drop_list"); set => Accessor.SetUInt32("reward_drop_list", value); }


  public string TournamentTag
  { get => Accessor.GetString("tournament_tag"); set => Accessor.SetString("tournament_tag", value); }


  public uint LegacySteamdatagramPort
  { get => Accessor.GetUInt32("legacy_steamdatagram_port"); set => Accessor.SetUInt32("legacy_steamdatagram_port", value); }


  public uint SteamdatagramRouting
  { get => Accessor.GetUInt32("steamdatagram_routing"); set => Accessor.SetUInt32("steamdatagram_routing", value); }


  public uint TestToken
  { get => Accessor.GetUInt32("test_token"); set => Accessor.SetUInt32("test_token", value); }


  public uint Flags
  { get => Accessor.GetUInt32("flags"); set => Accessor.SetUInt32("flags", value); }


  public uint SystemLoad
  { get => Accessor.GetUInt32("system_load"); set => Accessor.SetUInt32("system_load", value); }


  public uint CpusOnline
  { get => Accessor.GetUInt32("cpus_online"); set => Accessor.SetUInt32("cpus_online", value); }

}
