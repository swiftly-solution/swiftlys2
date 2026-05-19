using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface PublishedFileDetails_Tag : ITypedProtobuf<PublishedFileDetails_Tag>
{
    static PublishedFileDetails_Tag ITypedProtobuf<PublishedFileDetails_Tag>.Wrap(nint handle, bool isManuallyAllocated) => new PublishedFileDetails_TagImpl(handle, isManuallyAllocated);

    public string Tag { get; set; }
    public bool Adminonly { get; set; }
}