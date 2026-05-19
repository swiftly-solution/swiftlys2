using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCloud_GetUploadServerInfo_RequestImpl : TypedProtobuf<CCloud_GetUploadServerInfo_Request>, CCloud_GetUploadServerInfo_Request
{
    public CCloud_GetUploadServerInfo_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Appid
    { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }
}