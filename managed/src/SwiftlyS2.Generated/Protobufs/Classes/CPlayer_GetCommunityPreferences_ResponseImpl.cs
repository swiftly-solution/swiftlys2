using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_GetCommunityPreferences_ResponseImpl : TypedProtobuf<CPlayer_GetCommunityPreferences_Response>, CPlayer_GetCommunityPreferences_Response
{
    public CPlayer_GetCommunityPreferences_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public CPlayer_CommunityPreferences Preferences
    { get => new CPlayer_CommunityPreferencesImpl(NativeNetMessages.GetNestedMessage(Address, "preferences"), false); }
}