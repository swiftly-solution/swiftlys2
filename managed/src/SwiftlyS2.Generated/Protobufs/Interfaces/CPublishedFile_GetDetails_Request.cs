using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPublishedFile_GetDetails_Request : ITypedProtobuf<CPublishedFile_GetDetails_Request>
{
    static CPublishedFile_GetDetails_Request ITypedProtobuf<CPublishedFile_GetDetails_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CPublishedFile_GetDetails_RequestImpl(handle, isManuallyAllocated);

    public IProtobufRepeatedFieldValueType<ulong> Publishedfileids { get; }
    public bool Includetags { get; set; }
    public bool Includeadditionalpreviews { get; set; }
    public bool Includechildren { get; set; }
    public bool Includekvtags { get; set; }
    public bool Includevotes { get; set; }
    public bool ShortDescription { get; set; }
}