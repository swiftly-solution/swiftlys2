
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_MatchStatsUpdateImpl : NetMessage<CCSUsrMsg_MatchStatsUpdate>, CCSUsrMsg_MatchStatsUpdate
{
  public CCSUsrMsg_MatchStatsUpdateImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public string Update
  { get => Accessor.GetString("update"); set => Accessor.SetString("update", value); }

}
