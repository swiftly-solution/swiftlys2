using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_CommunityPreferences : ITypedProtobuf<CPlayer_CommunityPreferences>
{
    static CPlayer_CommunityPreferences ITypedProtobuf<CPlayer_CommunityPreferences>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_CommunityPreferencesImpl(handle, isManuallyAllocated);

    public bool HideAdultContentViolence { get; set; }
    public bool HideAdultContentSex { get; set; }
    public bool ParenthesizeNicknames { get; set; }
    public uint TimestampUpdated { get; set; }
}