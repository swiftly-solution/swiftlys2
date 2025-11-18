
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_CurrentRoundOddsImpl : NetMessage<CCSUsrMsg_CurrentRoundOdds>, CCSUsrMsg_CurrentRoundOdds
{
  public CCSUsrMsg_CurrentRoundOddsImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Odds
  { get => Accessor.GetInt32("odds"); set => Accessor.SetInt32("odds", value); }

}
