using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class PublishedFileDetails_PreviewImpl : TypedProtobuf<PublishedFileDetails_Preview>, PublishedFileDetails_Preview
{
    public PublishedFileDetails_PreviewImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public ulong Previewid
    { get => Accessor.GetUInt64("previewid"); set => Accessor.SetUInt64("previewid", value); }
    public uint Sortorder
    { get => Accessor.GetUInt32("sortorder"); set => Accessor.SetUInt32("sortorder", value); }
    public string Url
    { get => Accessor.GetString("url"); set => Accessor.SetString("url", value); }
    public uint Size
    { get => Accessor.GetUInt32("size"); set => Accessor.SetUInt32("size", value); }
    public string Filename
    { get => Accessor.GetString("filename"); set => Accessor.SetString("filename", value); }
    public string Youtubevideoid
    { get => Accessor.GetString("youtubevideoid"); set => Accessor.SetString("youtubevideoid", value); }
}