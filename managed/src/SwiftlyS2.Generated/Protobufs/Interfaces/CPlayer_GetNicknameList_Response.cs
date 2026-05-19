using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_GetNicknameList_Response : ITypedProtobuf<CPlayer_GetNicknameList_Response>
{
    static CPlayer_GetNicknameList_Response ITypedProtobuf<CPlayer_GetNicknameList_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_GetNicknameList_ResponseImpl(handle, isManuallyAllocated);

    public IProtobufRepeatedFieldSubMessageType<CPlayer_GetNicknameList_Response_PlayerNickname> Nicknames { get; }
}