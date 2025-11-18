
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface DeepPlayerStatsEntry : ITypedProtobuf<DeepPlayerStatsEntry>
{
  static DeepPlayerStatsEntry ITypedProtobuf<DeepPlayerStatsEntry>.Wrap(nint handle, bool isManuallyAllocated) => new DeepPlayerStatsEntryImpl(handle, isManuallyAllocated);


  public uint Accountid { get; set; }


  public ulong MatchId { get; set; }


  public uint MmGameMode { get; set; }


  public uint Mapid { get; set; }


  public bool BStartingCt { get; set; }


  public uint MatchOutcome { get; set; }


  public uint RoundsWon { get; set; }


  public uint RoundsLost { get; set; }


  public uint StatScore { get; set; }


  public uint StatDeaths { get; set; }


  public uint StatMvps { get; set; }


  public uint EnemyKills { get; set; }


  public uint EnemyHeadshots { get; set; }


  public uint Enemy2ks { get; set; }


  public uint Enemy3ks { get; set; }


  public uint Enemy4ks { get; set; }


  public uint TotalDamage { get; set; }


  public uint EngagementsEntryCount { get; set; }


  public uint EngagementsEntryWins { get; set; }


  public uint Engagements1v1Count { get; set; }


  public uint Engagements1v1Wins { get; set; }


  public uint Engagements1v2Count { get; set; }


  public uint Engagements1v2Wins { get; set; }


  public uint UtilityCount { get; set; }


  public uint UtilitySuccess { get; set; }


  public uint FlashCount { get; set; }


  public uint FlashSuccess { get; set; }


  public IProtobufRepeatedFieldValueType<uint> Mates { get; }

}
