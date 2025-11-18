
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_MatchmakingStartImpl : TypedProtobuf<CMsgGCCStrike15_v2_MatchmakingStart>, CMsgGCCStrike15_v2_MatchmakingStart
{
  public CMsgGCCStrike15_v2_MatchmakingStartImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldValueType<uint> AccountIds
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "account_ids"); }


  public uint GameType
  { get => Accessor.GetUInt32("game_type"); set => Accessor.SetUInt32("game_type", value); }


  public string TicketData
  { get => Accessor.GetString("ticket_data"); set => Accessor.SetString("ticket_data", value); }


  public uint ClientVersion
  { get => Accessor.GetUInt32("client_version"); set => Accessor.SetUInt32("client_version", value); }


  public TournamentMatchSetup TournamentMatch
  { get => new TournamentMatchSetupImpl(NativeNetMessages.GetNestedMessage(Address, "tournament_match"), false); }


  public bool PrimeOnly
  { get => Accessor.GetBool("prime_only"); set => Accessor.SetBool("prime_only", value); }


  public uint TvControl
  { get => Accessor.GetUInt32("tv_control"); set => Accessor.SetUInt32("tv_control", value); }


  public ulong LobbyId
  { get => Accessor.GetUInt64("lobby_id"); set => Accessor.SetUInt64("lobby_id", value); }

}
