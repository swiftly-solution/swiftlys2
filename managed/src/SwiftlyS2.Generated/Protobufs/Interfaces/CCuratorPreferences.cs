using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCuratorPreferences : ITypedProtobuf<CCuratorPreferences>
{
    static CCuratorPreferences ITypedProtobuf<CCuratorPreferences>.Wrap(nint handle, bool isManuallyAllocated) => new CCuratorPreferencesImpl(handle, isManuallyAllocated);

    public uint SupportedLanguages { get; set; }
    public bool PlatformWindows { get; set; }
    public bool PlatformMac { get; set; }
    public bool PlatformLinux { get; set; }
    public bool VrContent { get; set; }
    public bool AdultContentViolence { get; set; }
    public bool AdultContentSex { get; set; }
    public uint TimestampUpdated { get; set; }
    public IProtobufRepeatedFieldValueType<uint> TagidsCurated { get; }
    public IProtobufRepeatedFieldValueType<uint> TagidsFiltered { get; }
    public string WebsiteTitle { get; set; }
    public string WebsiteUrl { get; set; }
    public string DiscussionUrl { get; set; }
    public bool ShowBroadcast { get; set; }
}