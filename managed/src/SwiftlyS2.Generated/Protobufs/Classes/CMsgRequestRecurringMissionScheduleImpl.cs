using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgRequestRecurringMissionScheduleImpl : TypedProtobuf<CMsgRequestRecurringMissionSchedule>, CMsgRequestRecurringMissionSchedule
{
    public CMsgRequestRecurringMissionScheduleImpl( nint handle, bool isManuallyAllocated ) : base(handle)
    {
    }


}
