
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchmakingServerReservationResponse : ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingServerReservationResponse>
{
  static CMsgGCCStrike15_v2_MatchmakingServerReservationResponse ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingServerReservationResponse>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchmakingServerReservationResponseImpl(handle, isManuallyAllocated);


  public ulong Reservationid { get; set; }


  public CMsgGCCStrike15_v2_MatchmakingGC2ServerReserve Reservation { get; }


  public string Map { get; set; }


  public ulong GcReservationSent { get; set; }


  public uint ServerVersion { get; set; }


  public ServerHltvInfo TvInfo { get; }


  public IProtobufRepeatedFieldValueType<uint> RewardPlayerAccounts { get; }


  public IProtobufRepeatedFieldValueType<uint> IdlePlayerAccounts { get; }


  public uint RewardItemAttrDefIdx { get; set; }


  public uint RewardItemAttrValue { get; set; }


  public uint RewardItemAttrRewardIdx { get; set; }


  public uint RewardDropList { get; set; }


  public string TournamentTag { get; set; }


  public uint LegacySteamdatagramPort { get; set; }


  public uint SteamdatagramRouting { get; set; }


  public uint TestToken { get; set; }


  public uint Flags { get; set; }


  public uint SystemLoad { get; set; }


  public uint CpusOnline { get; set; }

}
