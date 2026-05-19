using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_GetNicknameList_Response_PlayerNicknameImpl : TypedProtobuf<CPlayer_GetNicknameList_Response_PlayerNickname>, CPlayer_GetNicknameList_Response_PlayerNickname
{
    public CPlayer_GetNicknameList_Response_PlayerNicknameImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Accountid
    { get => Accessor.GetUInt32("accountid"); set => Accessor.SetUInt32("accountid", value); }
    public string Nickname
    { get => Accessor.GetString("nickname"); set => Accessor.SetString("nickname", value); }
}