using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_CommunityPreferencesImpl : TypedProtobuf<CPlayer_CommunityPreferences>, CPlayer_CommunityPreferences
{
    public CPlayer_CommunityPreferencesImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public bool HideAdultContentViolence
    { get => Accessor.GetBool("hide_adult_content_violence"); set => Accessor.SetBool("hide_adult_content_violence", value); }
    public bool HideAdultContentSex
    { get => Accessor.GetBool("hide_adult_content_sex"); set => Accessor.SetBool("hide_adult_content_sex", value); }
    public bool ParenthesizeNicknames
    { get => Accessor.GetBool("parenthesize_nicknames"); set => Accessor.SetBool("parenthesize_nicknames", value); }
    public uint TimestampUpdated
    { get => Accessor.GetUInt32("timestamp_updated"); set => Accessor.SetUInt32("timestamp_updated", value); }
}