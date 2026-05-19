using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_GetNicknameList_ResponseImpl : TypedProtobuf<CPlayer_GetNicknameList_Response>, CPlayer_GetNicknameList_Response
{
    public CPlayer_GetNicknameList_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public IProtobufRepeatedFieldSubMessageType<CPlayer_GetNicknameList_Response_PlayerNickname> Nicknames
    { get => new ProtobufRepeatedFieldSubMessageType<CPlayer_GetNicknameList_Response_PlayerNickname>(Accessor, "nicknames"); }
}