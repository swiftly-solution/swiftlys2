using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface PublishedFileDetails_Preview : ITypedProtobuf<PublishedFileDetails_Preview>
{
    static PublishedFileDetails_Preview ITypedProtobuf<PublishedFileDetails_Preview>.Wrap(nint handle, bool isManuallyAllocated) => new PublishedFileDetails_PreviewImpl(handle, isManuallyAllocated);

    public ulong Previewid { get; set; }
    public uint Sortorder { get; set; }
    public string Url { get; set; }
    public uint Size { get; set; }
    public string Filename { get; set; }
    public string Youtubevideoid { get; set; }
}