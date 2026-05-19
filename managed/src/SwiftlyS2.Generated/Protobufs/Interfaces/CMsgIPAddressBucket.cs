using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgIPAddressBucket : ITypedProtobuf<CMsgIPAddressBucket>
{
    static CMsgIPAddressBucket ITypedProtobuf<CMsgIPAddressBucket>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgIPAddressBucketImpl(handle, isManuallyAllocated);

    public CMsgIPAddress OriginalIpAddress { get; }
    public ulong Bucket { get; set; }
}