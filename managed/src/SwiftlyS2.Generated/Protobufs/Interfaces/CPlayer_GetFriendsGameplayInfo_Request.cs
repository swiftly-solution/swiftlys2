using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_GetFriendsGameplayInfo_Request : ITypedProtobuf<CPlayer_GetFriendsGameplayInfo_Request>
{
    static CPlayer_GetFriendsGameplayInfo_Request ITypedProtobuf<CPlayer_GetFriendsGameplayInfo_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_GetFriendsGameplayInfo_RequestImpl(handle, isManuallyAllocated);

    public uint Appid { get; set; }
}