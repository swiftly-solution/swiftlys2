using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface PublishedFileDetails : ITypedProtobuf<PublishedFileDetails>
{
    static PublishedFileDetails ITypedProtobuf<PublishedFileDetails>.Wrap(nint handle, bool isManuallyAllocated) => new PublishedFileDetailsImpl(handle, isManuallyAllocated);

    public uint Result { get; set; }
    public ulong Publishedfileid { get; set; }
    public ulong Creator { get; set; }
    public uint CreatorAppid { get; set; }
    public uint ConsumerAppid { get; set; }
    public uint ConsumerShortcutid { get; set; }
    public string Filename { get; set; }
    public ulong FileSize { get; set; }
    public ulong PreviewFileSize { get; set; }
    public string FileUrl { get; set; }
    public string PreviewUrl { get; set; }
    public string Youtubevideoid { get; set; }
    public string Url { get; set; }
    public ulong HcontentFile { get; set; }
    public ulong HcontentPreview { get; set; }
    public string Title { get; set; }
    public string FileDescription { get; set; }
    public string ShortDescription { get; set; }
    public uint TimeCreated { get; set; }
    public uint TimeUpdated { get; set; }
    public uint Visibility { get; set; }
    public uint Flags { get; set; }
    public bool WorkshopFile { get; set; }
    public bool WorkshopAccepted { get; set; }
    public bool ShowSubscribeAll { get; set; }
    public int NumCommentsDeveloper { get; set; }
    public int NumCommentsPublic { get; set; }
    public bool Banned { get; set; }
    public string BanReason { get; set; }
    public ulong Banner { get; set; }
    public bool CanBeDeleted { get; set; }
    public bool Incompatible { get; set; }
    public string AppName { get; set; }
    public uint FileType { get; set; }
    public bool CanSubscribe { get; set; }
    public uint Subscriptions { get; set; }
    public uint Favorited { get; set; }
    public uint Followers { get; set; }
    public uint LifetimeSubscriptions { get; set; }
    public uint LifetimeFavorited { get; set; }
    public uint LifetimeFollowers { get; set; }
    public uint Views { get; set; }
    public uint ImageWidth { get; set; }
    public uint ImageHeight { get; set; }
    public string ImageUrl { get; set; }
    public bool SpoilerTag { get; set; }
    public uint Shortcutid { get; set; }
    public string Shortcutname { get; set; }
    public uint NumChildren { get; set; }
    public uint NumReports { get; set; }
    public IProtobufRepeatedFieldSubMessageType<PublishedFileDetails_Preview> Previews { get; }
    public IProtobufRepeatedFieldSubMessageType<PublishedFileDetails_Tag> Tags { get; }
    public IProtobufRepeatedFieldSubMessageType<PublishedFileDetails_Child> Children { get; }
    public IProtobufRepeatedFieldSubMessageType<PublishedFileDetails_KVTag> Kvtags { get; }
    public PublishedFileDetails_VoteData VoteData { get; }
    public uint TimeSubscribed { get; set; }
}