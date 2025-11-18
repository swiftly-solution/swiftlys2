
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDataGCCStrike15_v2_TournamentGroupImpl : TypedProtobuf<CDataGCCStrike15_v2_TournamentGroup>, CDataGCCStrike15_v2_TournamentGroup
{
  public CDataGCCStrike15_v2_TournamentGroupImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Groupid
  { get => Accessor.GetUInt32("groupid"); set => Accessor.SetUInt32("groupid", value); }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public string Desc
  { get => Accessor.GetString("desc"); set => Accessor.SetString("desc", value); }


  public uint PicksDeprecated
  { get => Accessor.GetUInt32("picks__deprecated"); set => Accessor.SetUInt32("picks__deprecated", value); }


  public IProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_TournamentGroupTeam> Teams
  { get => new ProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_TournamentGroupTeam>(Accessor, "teams"); }


  public IProtobufRepeatedFieldValueType<int> StageIds
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "stage_ids"); }


  public uint Picklockuntiltime
  { get => Accessor.GetUInt32("picklockuntiltime"); set => Accessor.SetUInt32("picklockuntiltime", value); }


  public uint Pickableteams
  { get => Accessor.GetUInt32("pickableteams"); set => Accessor.SetUInt32("pickableteams", value); }


  public uint PointsPerPick
  { get => Accessor.GetUInt32("points_per_pick"); set => Accessor.SetUInt32("points_per_pick", value); }


  public IProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_TournamentGroup_Picks> Picks
  { get => new ProtobufRepeatedFieldSubMessageType<CDataGCCStrike15_v2_TournamentGroup_Picks>(Accessor, "picks"); }

}
