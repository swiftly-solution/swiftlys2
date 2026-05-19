using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPublishedFile_GetUserFiles_Response_App : ITypedProtobuf<CPublishedFile_GetUserFiles_Response_App>
{
    static CPublishedFile_GetUserFiles_Response_App ITypedProtobuf<CPublishedFile_GetUserFiles_Response_App>.Wrap(nint handle, bool isManuallyAllocated) => new CPublishedFile_GetUserFiles_Response_AppImpl(handle, isManuallyAllocated);

    public uint Appid { get; set; }
    public string Name { get; set; }
    public uint Shortcutid { get; set; }
    public bool Private { get; set; }
}