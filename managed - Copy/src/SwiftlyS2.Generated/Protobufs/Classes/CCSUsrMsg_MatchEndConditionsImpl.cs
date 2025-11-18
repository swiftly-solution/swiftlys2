
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_MatchEndConditionsImpl : NetMessage<CCSUsrMsg_MatchEndConditions>, CCSUsrMsg_MatchEndConditions
{
  public CCSUsrMsg_MatchEndConditionsImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Fraglimit
  { get => Accessor.GetInt32("fraglimit"); set => Accessor.SetInt32("fraglimit", value); }


  public int MpMaxrounds
  { get => Accessor.GetInt32("mp_maxrounds"); set => Accessor.SetInt32("mp_maxrounds", value); }


  public int MpWinlimit
  { get => Accessor.GetInt32("mp_winlimit"); set => Accessor.SetInt32("mp_winlimit", value); }


  public float MpTimelimit
  { get => Accessor.GetFloat("mp_timelimit"); set => Accessor.SetFloat("mp_timelimit", value); }

}
