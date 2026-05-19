using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCuratorPreferencesImpl : TypedProtobuf<CCuratorPreferences>, CCuratorPreferences
{
    public CCuratorPreferencesImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint SupportedLanguages
    { get => Accessor.GetUInt32("supported_languages"); set => Accessor.SetUInt32("supported_languages", value); }
    public bool PlatformWindows
    { get => Accessor.GetBool("platform_windows"); set => Accessor.SetBool("platform_windows", value); }
    public bool PlatformMac
    { get => Accessor.GetBool("platform_mac"); set => Accessor.SetBool("platform_mac", value); }
    public bool PlatformLinux
    { get => Accessor.GetBool("platform_linux"); set => Accessor.SetBool("platform_linux", value); }
    public bool VrContent
    { get => Accessor.GetBool("vr_content"); set => Accessor.SetBool("vr_content", value); }
    public bool AdultContentViolence
    { get => Accessor.GetBool("adult_content_violence"); set => Accessor.SetBool("adult_content_violence", value); }
    public bool AdultContentSex
    { get => Accessor.GetBool("adult_content_sex"); set => Accessor.SetBool("adult_content_sex", value); }
    public uint TimestampUpdated
    { get => Accessor.GetUInt32("timestamp_updated"); set => Accessor.SetUInt32("timestamp_updated", value); }
    public IProtobufRepeatedFieldValueType<uint> TagidsCurated
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "tagids_curated"); }
    public IProtobufRepeatedFieldValueType<uint> TagidsFiltered
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "tagids_filtered"); }
    public string WebsiteTitle
    { get => Accessor.GetString("website_title"); set => Accessor.SetString("website_title", value); }
    public string WebsiteUrl
    { get => Accessor.GetString("website_url"); set => Accessor.SetString("website_url", value); }
    public string DiscussionUrl
    { get => Accessor.GetString("discussion_url"); set => Accessor.SetString("discussion_url", value); }
    public bool ShowBroadcast
    { get => Accessor.GetBool("show_broadcast"); set => Accessor.SetBool("show_broadcast", value); }
}