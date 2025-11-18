
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_VotePassImpl : NetMessage<CCSUsrMsg_VotePass>, CCSUsrMsg_VotePass
{
  public CCSUsrMsg_VotePassImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Team
  { get => Accessor.GetInt32("team"); set => Accessor.SetInt32("team", value); }


  public int VoteType
  { get => Accessor.GetInt32("vote_type"); set => Accessor.SetInt32("vote_type", value); }


  public string DispStr
  { get => Accessor.GetString("disp_str"); set => Accessor.SetString("disp_str", value); }


  public string DetailsStr
  { get => Accessor.GetString("details_str"); set => Accessor.SetString("details_str", value); }

}
