using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CClanMatchEventByRangeImpl : TypedProtobuf<CClanMatchEventByRange>, CClanMatchEventByRange
{
    public CClanMatchEventByRangeImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint RtimeBefore
    { get => Accessor.GetUInt32("rtime_before"); set => Accessor.SetUInt32("rtime_before", value); }
    public uint RtimeAfter
    { get => Accessor.GetUInt32("rtime_after"); set => Accessor.SetUInt32("rtime_after", value); }
    public uint Qualified
    { get => Accessor.GetUInt32("qualified"); set => Accessor.SetUInt32("qualified", value); }
    public IProtobufRepeatedFieldSubMessageType<CClanEventUserNewsTuple> Events
    { get => new ProtobufRepeatedFieldSubMessageType<CClanEventUserNewsTuple>(Accessor, "events"); }
}