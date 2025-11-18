
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate : ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate>
{
  static CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate ITypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdateImpl(handle, isManuallyAllocated);


  public int Matchmaking { get; set; }


  public IProtobufRepeatedFieldValueType<uint> WaitingAccountIdSessions { get; }


  public string Error { get; set; }


  public IProtobufRepeatedFieldValueType<uint> OngoingmatchAccountIdSessions { get; }


  public GlobalStatistics GlobalStats { get; }


  public IProtobufRepeatedFieldValueType<uint> FailpingAccountIdSessions { get; }


  public IProtobufRepeatedFieldValueType<uint> PenaltyAccountIdSessions { get; }


  public IProtobufRepeatedFieldValueType<uint> FailreadyAccountIdSessions { get; }


  public IProtobufRepeatedFieldValueType<uint> VacbannedAccountIdSessions { get; }


  public IpAddressMask ServerIpaddressMask { get; }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate_Note> Notes { get; }


  public IProtobufRepeatedFieldValueType<uint> PenaltyAccountIdSessionsGreen { get; }


  public IProtobufRepeatedFieldValueType<uint> InsufficientlevelSessions { get; }


  public IProtobufRepeatedFieldValueType<uint> VsncheckAccountIdSessions { get; }


  public IProtobufRepeatedFieldValueType<uint> LauncherMismatchSessions { get; }


  public IProtobufRepeatedFieldValueType<uint> InsecureAccountIdSessions { get; }

}
