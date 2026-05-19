using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPublishedFile_Publish_Request : ITypedProtobuf<CPublishedFile_Publish_Request>
{
    static CPublishedFile_Publish_Request ITypedProtobuf<CPublishedFile_Publish_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CPublishedFile_Publish_RequestImpl(handle, isManuallyAllocated);

    public uint Appid { get; set; }
    public uint ConsumerAppid { get; set; }
    public string Cloudfilename { get; set; }
    public string PreviewCloudfilename { get; set; }
    public string Title { get; set; }
    public string FileDescription { get; set; }
    public uint FileType { get; set; }
    public string ConsumerShortcutName { get; set; }
    public string YoutubeUsername { get; set; }
    public string YoutubeVideoid { get; set; }
    public uint Visibility { get; set; }
    public string RedirectUri { get; set; }
    public IProtobufRepeatedFieldValueType<string> Tags { get; }
    public string CollectionType { get; set; }
    public string GameType { get; set; }
    public string Url { get; set; }
}