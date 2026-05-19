using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_GetNicknameList_Response_PlayerNickname : ITypedProtobuf<CPlayer_GetNicknameList_Response_PlayerNickname>
{
    static CPlayer_GetNicknameList_Response_PlayerNickname ITypedProtobuf<CPlayer_GetNicknameList_Response_PlayerNickname>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_GetNicknameList_Response_PlayerNicknameImpl(handle, isManuallyAllocated);

    public uint Accountid { get; set; }
    public string Nickname { get; set; }
}