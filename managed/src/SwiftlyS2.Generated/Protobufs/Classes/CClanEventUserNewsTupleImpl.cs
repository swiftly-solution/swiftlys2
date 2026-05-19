using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CClanEventUserNewsTupleImpl : TypedProtobuf<CClanEventUserNewsTuple>, CClanEventUserNewsTuple
{
    public CClanEventUserNewsTupleImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Clanid
    { get => Accessor.GetUInt32("clanid"); set => Accessor.SetUInt32("clanid", value); }
    public ulong EventGid
    { get => Accessor.GetUInt64("event_gid"); set => Accessor.SetUInt64("event_gid", value); }
    public ulong AnnouncementGid
    { get => Accessor.GetUInt64("announcement_gid"); set => Accessor.SetUInt64("announcement_gid", value); }
    public uint RtimeStart
    { get => Accessor.GetUInt32("rtime_start"); set => Accessor.SetUInt32("rtime_start", value); }
    public uint RtimeEnd
    { get => Accessor.GetUInt32("rtime_end"); set => Accessor.SetUInt32("rtime_end", value); }
    public uint PriorityScore
    { get => Accessor.GetUInt32("priority_score"); set => Accessor.SetUInt32("priority_score", value); }
    public uint Type
    { get => Accessor.GetUInt32("type"); set => Accessor.SetUInt32("type", value); }
    public uint ClampRangeSlot
    { get => Accessor.GetUInt32("clamp_range_slot"); set => Accessor.SetUInt32("clamp_range_slot", value); }
    public uint Appid
    { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }
    public uint Rtime32LastModified
    { get => Accessor.GetUInt32("rtime32_last_modified"); set => Accessor.SetUInt32("rtime32_last_modified", value); }
}