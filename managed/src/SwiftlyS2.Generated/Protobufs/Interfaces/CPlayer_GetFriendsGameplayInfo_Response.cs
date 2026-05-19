using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_GetFriendsGameplayInfo_Response : ITypedProtobuf<CPlayer_GetFriendsGameplayInfo_Response>
{
    static CPlayer_GetFriendsGameplayInfo_Response ITypedProtobuf<CPlayer_GetFriendsGameplayInfo_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_GetFriendsGameplayInfo_ResponseImpl(handle, isManuallyAllocated);

    public CPlayer_GetFriendsGameplayInfo_Response_OwnGameplayInfo YourInfo { get; }
    public IProtobufRepeatedFieldSubMessageType<CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo> InGame { get; }
    public IProtobufRepeatedFieldSubMessageType<CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo> PlayedRecently { get; }
    public IProtobufRepeatedFieldSubMessageType<CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo> PlayedEver { get; }
    public IProtobufRepeatedFieldSubMessageType<CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo> Owns { get; }
    public IProtobufRepeatedFieldSubMessageType<CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo> InWishlist { get; }
}