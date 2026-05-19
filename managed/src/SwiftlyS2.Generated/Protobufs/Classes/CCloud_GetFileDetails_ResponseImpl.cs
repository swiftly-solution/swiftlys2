using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCloud_GetFileDetails_ResponseImpl : TypedProtobuf<CCloud_GetFileDetails_Response>, CCloud_GetFileDetails_Response
{
    public CCloud_GetFileDetails_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public CCloud_UserFile Details
    { get => new CCloud_UserFileImpl(NativeNetMessages.GetNestedMessage(Address, "details"), false); }
}