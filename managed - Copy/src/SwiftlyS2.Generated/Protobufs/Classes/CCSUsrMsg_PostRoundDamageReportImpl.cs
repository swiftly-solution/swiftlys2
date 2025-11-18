
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_PostRoundDamageReportImpl : NetMessage<CCSUsrMsg_PostRoundDamageReport>, CCSUsrMsg_PostRoundDamageReport
{
  public CCSUsrMsg_PostRoundDamageReportImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public ulong OtherXuid
  { get => Accessor.GetUInt64("other_xuid"); set => Accessor.SetUInt64("other_xuid", value); }


  public int GivenKillType
  { get => Accessor.GetInt32("given_kill_type"); set => Accessor.SetInt32("given_kill_type", value); }


  public int GivenHealthRemoved
  { get => Accessor.GetInt32("given_health_removed"); set => Accessor.SetInt32("given_health_removed", value); }


  public int GivenNumHits
  { get => Accessor.GetInt32("given_num_hits"); set => Accessor.SetInt32("given_num_hits", value); }


  public int TakenKillType
  { get => Accessor.GetInt32("taken_kill_type"); set => Accessor.SetInt32("taken_kill_type", value); }


  public int TakenHealthRemoved
  { get => Accessor.GetInt32("taken_health_removed"); set => Accessor.SetInt32("taken_health_removed", value); }


  public int TakenNumHits
  { get => Accessor.GetInt32("taken_num_hits"); set => Accessor.SetInt32("taken_num_hits", value); }

}
