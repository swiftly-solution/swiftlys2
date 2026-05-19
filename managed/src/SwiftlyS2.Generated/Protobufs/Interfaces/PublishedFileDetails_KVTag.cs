using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface PublishedFileDetails_KVTag : ITypedProtobuf<PublishedFileDetails_KVTag>
{
    static PublishedFileDetails_KVTag ITypedProtobuf<PublishedFileDetails_KVTag>.Wrap(nint handle, bool isManuallyAllocated) => new PublishedFileDetails_KVTagImpl(handle, isManuallyAllocated);

    public string Key { get; set; }
    public string Value { get; set; }
}