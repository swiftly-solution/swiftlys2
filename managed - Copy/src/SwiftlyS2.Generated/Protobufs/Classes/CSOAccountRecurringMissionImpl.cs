
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOAccountRecurringMissionImpl : TypedProtobuf<CSOAccountRecurringMission>, CSOAccountRecurringMission
{
  public CSOAccountRecurringMissionImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public uint MissionId
  { get => Accessor.GetUInt32("mission_id"); set => Accessor.SetUInt32("mission_id", value); }


  public uint Period
  { get => Accessor.GetUInt32("period"); set => Accessor.SetUInt32("period", value); }


  public uint Progress
  { get => Accessor.GetUInt32("progress"); set => Accessor.SetUInt32("progress", value); }

}
