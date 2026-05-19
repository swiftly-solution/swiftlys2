using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CClanEventDataImpl : TypedProtobuf<CClanEventData>, CClanEventData
{
    public CClanEventDataImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public ulong Gid
    { get => Accessor.GetUInt64("gid"); set => Accessor.SetUInt64("gid", value); }
    public ulong ClanSteamid
    { get => Accessor.GetUInt64("clan_steamid"); set => Accessor.SetUInt64("clan_steamid", value); }
    public string EventName
    { get => Accessor.GetString("event_name"); set => Accessor.SetString("event_name", value); }
    public EProtoClanEventType EventType
    { get => (EProtoClanEventType)Accessor.GetInt32("event_type"); set => Accessor.SetInt32("event_type", (int)value); }
    public uint Appid
    { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }
    public string ServerAddress
    { get => Accessor.GetString("server_address"); set => Accessor.SetString("server_address", value); }
    public string ServerPassword
    { get => Accessor.GetString("server_password"); set => Accessor.SetString("server_password", value); }
    public uint Rtime32StartTime
    { get => Accessor.GetUInt32("rtime32_start_time"); set => Accessor.SetUInt32("rtime32_start_time", value); }
    public uint Rtime32EndTime
    { get => Accessor.GetUInt32("rtime32_end_time"); set => Accessor.SetUInt32("rtime32_end_time", value); }
    public int CommentCount
    { get => Accessor.GetInt32("comment_count"); set => Accessor.SetInt32("comment_count", value); }
    public ulong CreatorSteamid
    { get => Accessor.GetUInt64("creator_steamid"); set => Accessor.SetUInt64("creator_steamid", value); }
    public ulong LastUpdateSteamid
    { get => Accessor.GetUInt64("last_update_steamid"); set => Accessor.SetUInt64("last_update_steamid", value); }
    public string EventNotes
    { get => Accessor.GetString("event_notes"); set => Accessor.SetString("event_notes", value); }
    public string Jsondata
    { get => Accessor.GetString("jsondata"); set => Accessor.SetString("jsondata", value); }
    public CCommunity_ClanAnnouncementInfo AnnouncementBody
    { get => new CCommunity_ClanAnnouncementInfoImpl(NativeNetMessages.GetNestedMessage(Address, "announcement_body"), false); }
    public bool Published
    { get => Accessor.GetBool("published"); set => Accessor.SetBool("published", value); }
    public bool Hidden
    { get => Accessor.GetBool("hidden"); set => Accessor.SetBool("hidden", value); }
    public uint Rtime32VisibilityStart
    { get => Accessor.GetUInt32("rtime32_visibility_start"); set => Accessor.SetUInt32("rtime32_visibility_start", value); }
    public uint Rtime32VisibilityEnd
    { get => Accessor.GetUInt32("rtime32_visibility_end"); set => Accessor.SetUInt32("rtime32_visibility_end", value); }
    public uint BroadcasterAccountid
    { get => Accessor.GetUInt32("broadcaster_accountid"); set => Accessor.SetUInt32("broadcaster_accountid", value); }
    public uint FollowerCount
    { get => Accessor.GetUInt32("follower_count"); set => Accessor.SetUInt32("follower_count", value); }
    public uint IgnoreCount
    { get => Accessor.GetUInt32("ignore_count"); set => Accessor.SetUInt32("ignore_count", value); }
    public ulong ForumTopicId
    { get => Accessor.GetUInt64("forum_topic_id"); set => Accessor.SetUInt64("forum_topic_id", value); }
    public uint Rtime32LastModified
    { get => Accessor.GetUInt32("rtime32_last_modified"); set => Accessor.SetUInt32("rtime32_last_modified", value); }
    public ulong NewsPostGid
    { get => Accessor.GetUInt64("news_post_gid"); set => Accessor.SetUInt64("news_post_gid", value); }
    public uint RtimeModReviewed
    { get => Accessor.GetUInt32("rtime_mod_reviewed"); set => Accessor.SetUInt32("rtime_mod_reviewed", value); }
    public uint FeaturedAppTagid
    { get => Accessor.GetUInt32("featured_app_tagid"); set => Accessor.SetUInt32("featured_app_tagid", value); }
    public IProtobufRepeatedFieldValueType<uint> ReferencedAppids
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "referenced_appids"); }
    public uint BuildId
    { get => Accessor.GetUInt32("build_id"); set => Accessor.SetUInt32("build_id", value); }
    public string BuildBranch
    { get => Accessor.GetString("build_branch"); set => Accessor.SetString("build_branch", value); }
}