using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_GetFriendsGameplayInfo_ResponseImpl : TypedProtobuf<CPlayer_GetFriendsGameplayInfo_Response>, CPlayer_GetFriendsGameplayInfo_Response
{
    public CPlayer_GetFriendsGameplayInfo_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public CPlayer_GetFriendsGameplayInfo_Response_OwnGameplayInfo YourInfo
    { get => new CPlayer_GetFriendsGameplayInfo_Response_OwnGameplayInfoImpl(NativeNetMessages.GetNestedMessage(Address, "your_info"), false); }
    public IProtobufRepeatedFieldSubMessageType<CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo> InGame
    { get => new ProtobufRepeatedFieldSubMessageType<CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo>(Accessor, "in_game"); }
    public IProtobufRepeatedFieldSubMessageType<CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo> PlayedRecently
    { get => new ProtobufRepeatedFieldSubMessageType<CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo>(Accessor, "played_recently"); }
    public IProtobufRepeatedFieldSubMessageType<CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo> PlayedEver
    { get => new ProtobufRepeatedFieldSubMessageType<CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo>(Accessor, "played_ever"); }
    public IProtobufRepeatedFieldSubMessageType<CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo> Owns
    { get => new ProtobufRepeatedFieldSubMessageType<CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo>(Accessor, "owns"); }
    public IProtobufRepeatedFieldSubMessageType<CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo> InWishlist
    { get => new ProtobufRepeatedFieldSubMessageType<CPlayer_GetFriendsGameplayInfo_Response_FriendsGameplayInfo>(Accessor, "in_wishlist"); }
}