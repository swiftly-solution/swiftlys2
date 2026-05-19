using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_GetPerFriendPreferences_ResponseImpl : TypedProtobuf<CPlayer_GetPerFriendPreferences_Response>, CPlayer_GetPerFriendPreferences_Response
{
    public CPlayer_GetPerFriendPreferences_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public IProtobufRepeatedFieldSubMessageType<PerFriendPreferences> Preferences
    { get => new ProtobufRepeatedFieldSubMessageType<PerFriendPreferences>(Accessor, "preferences"); }
}