
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_MatchmakingGC2ServerReserveImpl : TypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ServerReserve>, CMsgGCCStrike15_v2_MatchmakingGC2ServerReserve
{
  public CMsgGCCStrike15_v2_MatchmakingGC2ServerReserveImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldValueType<uint> AccountIds
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "account_ids"); }


  public uint GameType
  { get => Accessor.GetUInt32("game_type"); set => Accessor.SetUInt32("game_type", value); }


  public ulong MatchId
  { get => Accessor.GetUInt64("match_id"); set => Accessor.SetUInt64("match_id", value); }


  public uint ServerVersion
  { get => Accessor.GetUInt32("server_version"); set => Accessor.SetUInt32("server_version", value); }


  public uint Flags
  { get => Accessor.GetUInt32("flags"); set => Accessor.SetUInt32("flags", value); }


  public IProtobufRepeatedFieldSubMessageType<PlayerRankingInfo> Rankings
  { get => new ProtobufRepeatedFieldSubMessageType<PlayerRankingInfo>(Accessor, "rankings"); }


  public ulong EncryptionKey
  { get => Accessor.GetUInt64("encryption_key"); set => Accessor.SetUInt64("encryption_key", value); }


  public ulong EncryptionKeyPub
  { get => Accessor.GetUInt64("encryption_key_pub"); set => Accessor.SetUInt64("encryption_key_pub", value); }


  public IProtobufRepeatedFieldValueType<uint> PartyIds
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "party_ids"); }


  public IProtobufRepeatedFieldSubMessageType<IpAddressMask> Whitelist
  { get => new ProtobufRepeatedFieldSubMessageType<IpAddressMask>(Accessor, "whitelist"); }


  public ulong TvMasterSteamid
  { get => Accessor.GetUInt64("tv_master_steamid"); set => Accessor.SetUInt64("tv_master_steamid", value); }


  public TournamentEvent TournamentEvent
  { get => new TournamentEventImpl(NativeNetMessages.GetNestedMessage(Address, "tournament_event"), false); }


  public IProtobufRepeatedFieldSubMessageType<TournamentTeam> TournamentTeams
  { get => new ProtobufRepeatedFieldSubMessageType<TournamentTeam>(Accessor, "tournament_teams"); }


  public IProtobufRepeatedFieldValueType<uint> TournamentCastersAccountIds
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "tournament_casters_account_ids"); }


  public ulong TvRelaySteamid
  { get => Accessor.GetUInt64("tv_relay_steamid"); set => Accessor.SetUInt64("tv_relay_steamid", value); }


  public CPreMatchInfoData PreMatchData
  { get => new CPreMatchInfoDataImpl(NativeNetMessages.GetNestedMessage(Address, "pre_match_data"), false); }


  public uint TvControl
  { get => Accessor.GetUInt32("tv_control"); set => Accessor.SetUInt32("tv_control", value); }


  public IProtobufRepeatedFieldSubMessageType<OperationalVarValue> OpVarValues
  { get => new ProtobufRepeatedFieldSubMessageType<OperationalVarValue>(Accessor, "op_var_values"); }


  public uint SocacheControl
  { get => Accessor.GetUInt32("socache_control"); set => Accessor.SetUInt32("socache_control", value); }


  public IProtobufRepeatedFieldValueType<int> TeammateColors
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "teammate_colors"); }


  public uint MatchIdAdditional
  { get => Accessor.GetUInt32("match_id_additional"); set => Accessor.SetUInt32("match_id_additional", value); }

}
