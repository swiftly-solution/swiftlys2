
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_Predictions_GroupMatchTeamPickImpl : TypedProtobuf<CMsgGCCStrike15_v2_Predictions_GroupMatchTeamPick>, CMsgGCCStrike15_v2_Predictions_GroupMatchTeamPick
{
  public CMsgGCCStrike15_v2_Predictions_GroupMatchTeamPickImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Sectionid
  { get => Accessor.GetInt32("sectionid"); set => Accessor.SetInt32("sectionid", value); }


  public int Groupid
  { get => Accessor.GetInt32("groupid"); set => Accessor.SetInt32("groupid", value); }


  public int Index
  { get => Accessor.GetInt32("index"); set => Accessor.SetInt32("index", value); }


  public int Teamid
  { get => Accessor.GetInt32("teamid"); set => Accessor.SetInt32("teamid", value); }


  public ulong Itemid
  { get => Accessor.GetUInt64("itemid"); set => Accessor.SetUInt64("itemid", value); }

}
