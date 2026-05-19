using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_SetCommunityPreferences_RequestImpl : TypedProtobuf<CPlayer_SetCommunityPreferences_Request>, CPlayer_SetCommunityPreferences_Request
{
    public CPlayer_SetCommunityPreferences_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public CPlayer_CommunityPreferences Preferences
    { get => new CPlayer_CommunityPreferencesImpl(NativeNetMessages.GetNestedMessage(Address, "preferences"), false); }
}