using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPublishedFile_Update_Request : ITypedProtobuf<CPublishedFile_Update_Request>
{
    static CPublishedFile_Update_Request ITypedProtobuf<CPublishedFile_Update_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CPublishedFile_Update_RequestImpl(handle, isManuallyAllocated);

    public uint Appid { get; set; }
    public ulong Publishedfileid { get; set; }
    public string Title { get; set; }
    public string FileDescription { get; set; }
    public uint Visibility { get; set; }
    public IProtobufRepeatedFieldValueType<string> Tags { get; }
    public string Filename { get; set; }
    public string PreviewFilename { get; set; }
}