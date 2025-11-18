
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPreMatchInfoData_TeamStatsImpl : TypedProtobuf<CPreMatchInfoData_TeamStats>, CPreMatchInfoData_TeamStats
{
  public CPreMatchInfoData_TeamStatsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int MatchInfoIdxtxt
  { get => Accessor.GetInt32("match_info_idxtxt"); set => Accessor.SetInt32("match_info_idxtxt", value); }


  public string MatchInfoTxt
  { get => Accessor.GetString("match_info_txt"); set => Accessor.SetString("match_info_txt", value); }


  public IProtobufRepeatedFieldValueType<string> MatchInfoTeams
  { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "match_info_teams"); }

}
