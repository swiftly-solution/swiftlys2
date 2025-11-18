
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdateImpl : TypedProtobuf<CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate>, CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate
{
  public CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdateImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Matchmaking
  { get => Accessor.GetInt32("matchmaking"); set => Accessor.SetInt32("matchmaking", value); }


  public IProtobufRepeatedFieldValueType<uint> WaitingAccountIdSessions
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "waiting_account_id_sessions"); }


  public string Error
  { get => Accessor.GetString("error"); set => Accessor.SetString("error", value); }


  public IProtobufRepeatedFieldValueType<uint> OngoingmatchAccountIdSessions
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "ongoingmatch_account_id_sessions"); }


  public GlobalStatistics GlobalStats
  { get => new GlobalStatisticsImpl(NativeNetMessages.GetNestedMessage(Address, "global_stats"), false); }


  public IProtobufRepeatedFieldValueType<uint> FailpingAccountIdSessions
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "failping_account_id_sessions"); }


  public IProtobufRepeatedFieldValueType<uint> PenaltyAccountIdSessions
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "penalty_account_id_sessions"); }


  public IProtobufRepeatedFieldValueType<uint> FailreadyAccountIdSessions
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "failready_account_id_sessions"); }


  public IProtobufRepeatedFieldValueType<uint> VacbannedAccountIdSessions
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "vacbanned_account_id_sessions"); }


  public IpAddressMask ServerIpaddressMask
  { get => new IpAddressMaskImpl(NativeNetMessages.GetNestedMessage(Address, "server_ipaddress_mask"), false); }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate_Note> Notes
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_MatchmakingGC2ClientUpdate_Note>(Accessor, "notes"); }


  public IProtobufRepeatedFieldValueType<uint> PenaltyAccountIdSessionsGreen
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "penalty_account_id_sessions_green"); }


  public IProtobufRepeatedFieldValueType<uint> InsufficientlevelSessions
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "insufficientlevel_sessions"); }


  public IProtobufRepeatedFieldValueType<uint> VsncheckAccountIdSessions
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "vsncheck_account_id_sessions"); }


  public IProtobufRepeatedFieldValueType<uint> LauncherMismatchSessions
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "launcher_mismatch_sessions"); }


  public IProtobufRepeatedFieldValueType<uint> InsecureAccountIdSessions
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "insecure_account_id_sessions"); }

}
