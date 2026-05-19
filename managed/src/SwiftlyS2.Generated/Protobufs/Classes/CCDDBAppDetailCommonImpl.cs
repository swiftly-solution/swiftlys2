using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCDDBAppDetailCommonImpl : TypedProtobuf<CCDDBAppDetailCommon>, CCDDBAppDetailCommon
{
    public CCDDBAppDetailCommonImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Appid
    { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }
    public string Name
    { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }
    public string Icon
    { get => Accessor.GetString("icon"); set => Accessor.SetString("icon", value); }
    public bool Tool
    { get => Accessor.GetBool("tool"); set => Accessor.SetBool("tool", value); }
    public bool Demo
    { get => Accessor.GetBool("demo"); set => Accessor.SetBool("demo", value); }
    public bool Media
    { get => Accessor.GetBool("media"); set => Accessor.SetBool("media", value); }
    public bool CommunityVisibleStats
    { get => Accessor.GetBool("community_visible_stats"); set => Accessor.SetBool("community_visible_stats", value); }
    public string FriendlyName
    { get => Accessor.GetString("friendly_name"); set => Accessor.SetString("friendly_name", value); }
    public string Propagation
    { get => Accessor.GetString("propagation"); set => Accessor.SetString("propagation", value); }
    public bool HasAdultContent
    { get => Accessor.GetBool("has_adult_content"); set => Accessor.SetBool("has_adult_content", value); }
    public bool IsVisibleInSteamChina
    { get => Accessor.GetBool("is_visible_in_steam_china"); set => Accessor.SetBool("is_visible_in_steam_china", value); }
    public uint AppType
    { get => Accessor.GetUInt32("app_type"); set => Accessor.SetUInt32("app_type", value); }
}