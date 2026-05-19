using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPublishedFile_GetUserFiles_Request : ITypedProtobuf<CPublishedFile_GetUserFiles_Request>
{
    static CPublishedFile_GetUserFiles_Request ITypedProtobuf<CPublishedFile_GetUserFiles_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CPublishedFile_GetUserFiles_RequestImpl(handle, isManuallyAllocated);

    public uint Appid { get; set; }
    public uint Page { get; set; }
    public uint Numperpage { get; set; }
    public string Sortmethod { get; set; }
    public bool Totalonly { get; set; }
    public uint Privacy { get; set; }
    public bool IdsOnly { get; set; }
    public IProtobufRepeatedFieldValueType<string> Requiredtags { get; }
    public IProtobufRepeatedFieldValueType<string> Excludedtags { get; }
}