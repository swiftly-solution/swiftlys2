using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_GetNicknameList_RequestImpl : TypedProtobuf<CPlayer_GetNicknameList_Request>, CPlayer_GetNicknameList_Request
{
    public CPlayer_GetNicknameList_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

}