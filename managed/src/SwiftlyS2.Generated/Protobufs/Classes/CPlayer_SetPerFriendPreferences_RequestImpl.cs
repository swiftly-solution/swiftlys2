using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_SetPerFriendPreferences_RequestImpl : TypedProtobuf<CPlayer_SetPerFriendPreferences_Request>, CPlayer_SetPerFriendPreferences_Request
{
    public CPlayer_SetPerFriendPreferences_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public PerFriendPreferences Preferences
    { get => new PerFriendPreferencesImpl(NativeNetMessages.GetNestedMessage(Address, "preferences"), false); }
}