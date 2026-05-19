using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface PublishedFileDetails_Child : ITypedProtobuf<PublishedFileDetails_Child>
{
    static PublishedFileDetails_Child ITypedProtobuf<PublishedFileDetails_Child>.Wrap(nint handle, bool isManuallyAllocated) => new PublishedFileDetails_ChildImpl(handle, isManuallyAllocated);

    public ulong Publishedfileid { get; set; }
    public uint Sortorder { get; set; }
    public uint FileType { get; set; }
}