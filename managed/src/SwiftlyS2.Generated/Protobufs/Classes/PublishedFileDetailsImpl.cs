using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class PublishedFileDetailsImpl : TypedProtobuf<PublishedFileDetails>, PublishedFileDetails
{
    public PublishedFileDetailsImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Result
    { get => Accessor.GetUInt32("result"); set => Accessor.SetUInt32("result", value); }
    public ulong Publishedfileid
    { get => Accessor.GetUInt64("publishedfileid"); set => Accessor.SetUInt64("publishedfileid", value); }
    public ulong Creator
    { get => Accessor.GetUInt64("creator"); set => Accessor.SetUInt64("creator", value); }
    public uint CreatorAppid
    { get => Accessor.GetUInt32("creator_appid"); set => Accessor.SetUInt32("creator_appid", value); }
    public uint ConsumerAppid
    { get => Accessor.GetUInt32("consumer_appid"); set => Accessor.SetUInt32("consumer_appid", value); }
    public uint ConsumerShortcutid
    { get => Accessor.GetUInt32("consumer_shortcutid"); set => Accessor.SetUInt32("consumer_shortcutid", value); }
    public string Filename
    { get => Accessor.GetString("filename"); set => Accessor.SetString("filename", value); }
    public ulong FileSize
    { get => Accessor.GetUInt64("file_size"); set => Accessor.SetUInt64("file_size", value); }
    public ulong PreviewFileSize
    { get => Accessor.GetUInt64("preview_file_size"); set => Accessor.SetUInt64("preview_file_size", value); }
    public string FileUrl
    { get => Accessor.GetString("file_url"); set => Accessor.SetString("file_url", value); }
    public string PreviewUrl
    { get => Accessor.GetString("preview_url"); set => Accessor.SetString("preview_url", value); }
    public string Youtubevideoid
    { get => Accessor.GetString("youtubevideoid"); set => Accessor.SetString("youtubevideoid", value); }
    public string Url
    { get => Accessor.GetString("url"); set => Accessor.SetString("url", value); }
    public ulong HcontentFile
    { get => Accessor.GetUInt64("hcontent_file"); set => Accessor.SetUInt64("hcontent_file", value); }
    public ulong HcontentPreview
    { get => Accessor.GetUInt64("hcontent_preview"); set => Accessor.SetUInt64("hcontent_preview", value); }
    public string Title
    { get => Accessor.GetString("title"); set => Accessor.SetString("title", value); }
    public string FileDescription
    { get => Accessor.GetString("file_description"); set => Accessor.SetString("file_description", value); }
    public string ShortDescription
    { get => Accessor.GetString("short_description"); set => Accessor.SetString("short_description", value); }
    public uint TimeCreated
    { get => Accessor.GetUInt32("time_created"); set => Accessor.SetUInt32("time_created", value); }
    public uint TimeUpdated
    { get => Accessor.GetUInt32("time_updated"); set => Accessor.SetUInt32("time_updated", value); }
    public uint Visibility
    { get => Accessor.GetUInt32("visibility"); set => Accessor.SetUInt32("visibility", value); }
    public uint Flags
    { get => Accessor.GetUInt32("flags"); set => Accessor.SetUInt32("flags", value); }
    public bool WorkshopFile
    { get => Accessor.GetBool("workshop_file"); set => Accessor.SetBool("workshop_file", value); }
    public bool WorkshopAccepted
    { get => Accessor.GetBool("workshop_accepted"); set => Accessor.SetBool("workshop_accepted", value); }
    public bool ShowSubscribeAll
    { get => Accessor.GetBool("show_subscribe_all"); set => Accessor.SetBool("show_subscribe_all", value); }
    public int NumCommentsDeveloper
    { get => Accessor.GetInt32("num_comments_developer"); set => Accessor.SetInt32("num_comments_developer", value); }
    public int NumCommentsPublic
    { get => Accessor.GetInt32("num_comments_public"); set => Accessor.SetInt32("num_comments_public", value); }
    public bool Banned
    { get => Accessor.GetBool("banned"); set => Accessor.SetBool("banned", value); }
    public string BanReason
    { get => Accessor.GetString("ban_reason"); set => Accessor.SetString("ban_reason", value); }
    public ulong Banner
    { get => Accessor.GetUInt64("banner"); set => Accessor.SetUInt64("banner", value); }
    public bool CanBeDeleted
    { get => Accessor.GetBool("can_be_deleted"); set => Accessor.SetBool("can_be_deleted", value); }
    public bool Incompatible
    { get => Accessor.GetBool("incompatible"); set => Accessor.SetBool("incompatible", value); }
    public string AppName
    { get => Accessor.GetString("app_name"); set => Accessor.SetString("app_name", value); }
    public uint FileType
    { get => Accessor.GetUInt32("file_type"); set => Accessor.SetUInt32("file_type", value); }
    public bool CanSubscribe
    { get => Accessor.GetBool("can_subscribe"); set => Accessor.SetBool("can_subscribe", value); }
    public uint Subscriptions
    { get => Accessor.GetUInt32("subscriptions"); set => Accessor.SetUInt32("subscriptions", value); }
    public uint Favorited
    { get => Accessor.GetUInt32("favorited"); set => Accessor.SetUInt32("favorited", value); }
    public uint Followers
    { get => Accessor.GetUInt32("followers"); set => Accessor.SetUInt32("followers", value); }
    public uint LifetimeSubscriptions
    { get => Accessor.GetUInt32("lifetime_subscriptions"); set => Accessor.SetUInt32("lifetime_subscriptions", value); }
    public uint LifetimeFavorited
    { get => Accessor.GetUInt32("lifetime_favorited"); set => Accessor.SetUInt32("lifetime_favorited", value); }
    public uint LifetimeFollowers
    { get => Accessor.GetUInt32("lifetime_followers"); set => Accessor.SetUInt32("lifetime_followers", value); }
    public uint Views
    { get => Accessor.GetUInt32("views"); set => Accessor.SetUInt32("views", value); }
    public uint ImageWidth
    { get => Accessor.GetUInt32("image_width"); set => Accessor.SetUInt32("image_width", value); }
    public uint ImageHeight
    { get => Accessor.GetUInt32("image_height"); set => Accessor.SetUInt32("image_height", value); }
    public string ImageUrl
    { get => Accessor.GetString("image_url"); set => Accessor.SetString("image_url", value); }
    public bool SpoilerTag
    { get => Accessor.GetBool("spoiler_tag"); set => Accessor.SetBool("spoiler_tag", value); }
    public uint Shortcutid
    { get => Accessor.GetUInt32("shortcutid"); set => Accessor.SetUInt32("shortcutid", value); }
    public string Shortcutname
    { get => Accessor.GetString("shortcutname"); set => Accessor.SetString("shortcutname", value); }
    public uint NumChildren
    { get => Accessor.GetUInt32("num_children"); set => Accessor.SetUInt32("num_children", value); }
    public uint NumReports
    { get => Accessor.GetUInt32("num_reports"); set => Accessor.SetUInt32("num_reports", value); }
    public IProtobufRepeatedFieldSubMessageType<PublishedFileDetails_Preview> Previews
    { get => new ProtobufRepeatedFieldSubMessageType<PublishedFileDetails_Preview>(Accessor, "previews"); }
    public IProtobufRepeatedFieldSubMessageType<PublishedFileDetails_Tag> Tags
    { get => new ProtobufRepeatedFieldSubMessageType<PublishedFileDetails_Tag>(Accessor, "tags"); }
    public IProtobufRepeatedFieldSubMessageType<PublishedFileDetails_Child> Children
    { get => new ProtobufRepeatedFieldSubMessageType<PublishedFileDetails_Child>(Accessor, "children"); }
    public IProtobufRepeatedFieldSubMessageType<PublishedFileDetails_KVTag> Kvtags
    { get => new ProtobufRepeatedFieldSubMessageType<PublishedFileDetails_KVTag>(Accessor, "kvtags"); }
    public PublishedFileDetails_VoteData VoteData
    { get => new PublishedFileDetails_VoteDataImpl(NativeNetMessages.GetNestedMessage(Address, "vote_data"), false); }
    public uint TimeSubscribed
    { get => Accessor.GetUInt32("time_subscribed"); set => Accessor.SetUInt32("time_subscribed", value); }
}