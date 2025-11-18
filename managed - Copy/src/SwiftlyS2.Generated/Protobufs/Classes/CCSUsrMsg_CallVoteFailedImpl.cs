
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_CallVoteFailedImpl : NetMessage<CCSUsrMsg_CallVoteFailed>, CCSUsrMsg_CallVoteFailed
{
  public CCSUsrMsg_CallVoteFailedImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Reason
  { get => Accessor.GetInt32("reason"); set => Accessor.SetInt32("reason", value); }


  public int Time
  { get => Accessor.GetInt32("time"); set => Accessor.SetInt32("time", value); }

}
