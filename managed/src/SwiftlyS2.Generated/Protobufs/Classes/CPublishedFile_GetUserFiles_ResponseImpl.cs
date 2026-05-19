using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPublishedFile_GetUserFiles_ResponseImpl : TypedProtobuf<CPublishedFile_GetUserFiles_Response>, CPublishedFile_GetUserFiles_Response
{
    public CPublishedFile_GetUserFiles_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Total
    { get => Accessor.GetUInt32("total"); set => Accessor.SetUInt32("total", value); }
    public uint Startindex
    { get => Accessor.GetUInt32("startindex"); set => Accessor.SetUInt32("startindex", value); }
    public IProtobufRepeatedFieldSubMessageType<PublishedFileDetails> Publishedfiledetails
    { get => new ProtobufRepeatedFieldSubMessageType<PublishedFileDetails>(Accessor, "publishedfiledetails"); }
    public IProtobufRepeatedFieldSubMessageType<CPublishedFile_GetUserFiles_Response_App> Apps
    { get => new ProtobufRepeatedFieldSubMessageType<CPublishedFile_GetUserFiles_Response_App>(Accessor, "apps"); }
}