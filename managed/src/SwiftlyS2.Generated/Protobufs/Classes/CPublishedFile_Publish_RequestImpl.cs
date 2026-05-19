using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPublishedFile_Publish_RequestImpl : TypedProtobuf<CPublishedFile_Publish_Request>, CPublishedFile_Publish_Request
{
    public CPublishedFile_Publish_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Appid
    { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }
    public uint ConsumerAppid
    { get => Accessor.GetUInt32("consumer_appid"); set => Accessor.SetUInt32("consumer_appid", value); }
    public string Cloudfilename
    { get => Accessor.GetString("cloudfilename"); set => Accessor.SetString("cloudfilename", value); }
    public string PreviewCloudfilename
    { get => Accessor.GetString("preview_cloudfilename"); set => Accessor.SetString("preview_cloudfilename", value); }
    public string Title
    { get => Accessor.GetString("title"); set => Accessor.SetString("title", value); }
    public string FileDescription
    { get => Accessor.GetString("file_description"); set => Accessor.SetString("file_description", value); }
    public uint FileType
    { get => Accessor.GetUInt32("file_type"); set => Accessor.SetUInt32("file_type", value); }
    public string ConsumerShortcutName
    { get => Accessor.GetString("consumer_shortcut_name"); set => Accessor.SetString("consumer_shortcut_name", value); }
    public string YoutubeUsername
    { get => Accessor.GetString("youtube_username"); set => Accessor.SetString("youtube_username", value); }
    public string YoutubeVideoid
    { get => Accessor.GetString("youtube_videoid"); set => Accessor.SetString("youtube_videoid", value); }
    public uint Visibility
    { get => Accessor.GetUInt32("visibility"); set => Accessor.SetUInt32("visibility", value); }
    public string RedirectUri
    { get => Accessor.GetString("redirect_uri"); set => Accessor.SetString("redirect_uri", value); }
    public IProtobufRepeatedFieldValueType<string> Tags
    { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "tags"); }
    public string CollectionType
    { get => Accessor.GetString("collection_type"); set => Accessor.SetString("collection_type", value); }
    public string GameType
    { get => Accessor.GetString("game_type"); set => Accessor.SetString("game_type", value); }
    public string Url
    { get => Accessor.GetString("url"); set => Accessor.SetString("url", value); }
}