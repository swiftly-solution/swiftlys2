
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_VoteFailedImpl : NetMessage<CCSUsrMsg_VoteFailed>, CCSUsrMsg_VoteFailed
{
  public CCSUsrMsg_VoteFailedImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Team
  { get => Accessor.GetInt32("team"); set => Accessor.SetInt32("team", value); }


  public int Reason
  { get => Accessor.GetInt32("reason"); set => Accessor.SetInt32("reason", value); }

}
