using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_GetFriendsGameplayInfo_RequestImpl : TypedProtobuf<CPlayer_GetFriendsGameplayInfo_Request>, CPlayer_GetFriendsGameplayInfo_Request
{
    public CPlayer_GetFriendsGameplayInfo_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Appid
    { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }
}