using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPublishedFile_Subscribe_Request : ITypedProtobuf<CPublishedFile_Subscribe_Request>
{
    static CPublishedFile_Subscribe_Request ITypedProtobuf<CPublishedFile_Subscribe_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CPublishedFile_Subscribe_RequestImpl(handle, isManuallyAllocated);

    public ulong Publishedfileid { get; set; }
    public uint ListType { get; set; }
    public int Appid { get; set; }
    public bool NotifyClient { get; set; }
}