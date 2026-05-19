using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CClanMatchEventByRange : ITypedProtobuf<CClanMatchEventByRange>
{
    static CClanMatchEventByRange ITypedProtobuf<CClanMatchEventByRange>.Wrap(nint handle, bool isManuallyAllocated) => new CClanMatchEventByRangeImpl(handle, isManuallyAllocated);

    public uint RtimeBefore { get; set; }
    public uint RtimeAfter { get; set; }
    public uint Qualified { get; set; }
    public IProtobufRepeatedFieldSubMessageType<CClanEventUserNewsTuple> Events { get; }
}