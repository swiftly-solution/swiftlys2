
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchmakingGC2ServerReserve : ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ServerReserve>
{
  static CMsgGCCStrike15_v2_MatchmakingGC2ServerReserve ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ServerReserve>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchmakingGC2ServerReserveImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldValueType<uint> AccountIds { get; }


  public uint GameType { get; set; }


  public ulong MatchId { get; set; }


  public uint ServerVersion { get; set; }


  public uint Flags { get; set; }


  public IProtobufRepeatedFieldSubMessageType<PlayerRankingInfo> Rankings { get; }


  public ulong EncryptionKey { get; set; }


  public ulong EncryptionKeyPub { get; set; }


  public IProtobufRepeatedFieldValueType<uint> PartyIds { get; }


  public IProtobufRepeatedFieldSubMessageType<IpAddressMask> Whitelist { get; }


  public ulong TvMasterSteamid { get; set; }


  public TournamentEvent TournamentEvent { get; }


  public IProtobufRepeatedFieldSubMessageType<TournamentTeam> TournamentTeams { get; }


  public IProtobufRepeatedFieldValueType<uint> TournamentCastersAccountIds { get; }


  public ulong TvRelaySteamid { get; set; }


  public CPreMatchInfoData PreMatchData { get; }


  public uint TvControl { get; set; }


  public IProtobufRepeatedFieldSubMessageType<OperationalVarValue> OpVarValues { get; }


  public uint SocacheControl { get; set; }


  public IProtobufRepeatedFieldValueType<int> TeammateColors { get; }


  public uint MatchIdAdditional { get; set; }

}
