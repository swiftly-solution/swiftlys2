
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchmakingStart : ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingStart>
{
  static CMsgGCCStrike15_v2_MatchmakingStart ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingStart>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchmakingStartImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldValueType<uint> AccountIds { get; }


  public uint GameType { get; set; }


  public string TicketData { get; set; }


  public uint ClientVersion { get; set; }


  public TournamentMatchSetup TournamentMatch { get; }


  public bool PrimeOnly { get; set; }


  public uint TvControl { get; set; }


  public ulong LobbyId { get; set; }

}
