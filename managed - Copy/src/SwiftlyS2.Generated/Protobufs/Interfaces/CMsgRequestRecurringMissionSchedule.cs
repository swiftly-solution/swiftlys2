
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgRequestRecurringMissionSchedule : ITypedProtobuf<CMsgRequestRecurringMissionSchedule>
{
  static CMsgRequestRecurringMissionSchedule ITypedProtobuf<CMsgRequestRecurringMissionSchedule>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgRequestRecurringMissionScheduleImpl(handle, isManuallyAllocated);


}
