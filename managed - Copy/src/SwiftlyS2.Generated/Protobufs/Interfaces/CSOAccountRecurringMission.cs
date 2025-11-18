
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOAccountRecurringMission : ITypedProtobuf<CSOAccountRecurringMission>
{
  static CSOAccountRecurringMission ITypedProtobuf<CSOAccountRecurringMission>.Wrap(nint handle, bool isManuallyAllocated) => new CSOAccountRecurringMissionImpl(handle, isManuallyAllocated);


  public uint AccountId { get; set; }


  public uint MissionId { get; set; }


  public uint Period { get; set; }


  public uint Progress { get; set; }

}
