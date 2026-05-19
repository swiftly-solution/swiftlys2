using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CClanEventUserNewsTuple : ITypedProtobuf<CClanEventUserNewsTuple>
{
    static CClanEventUserNewsTuple ITypedProtobuf<CClanEventUserNewsTuple>.Wrap(nint handle, bool isManuallyAllocated) => new CClanEventUserNewsTupleImpl(handle, isManuallyAllocated);

    public uint Clanid { get; set; }
    public ulong EventGid { get; set; }
    public ulong AnnouncementGid { get; set; }
    public uint RtimeStart { get; set; }
    public uint RtimeEnd { get; set; }
    public uint PriorityScore { get; set; }
    public uint Type { get; set; }
    public uint ClampRangeSlot { get; set; }
    public uint Appid { get; set; }
    public uint Rtime32LastModified { get; set; }
}