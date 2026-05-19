using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class PublishedFileDetails_ChildImpl : TypedProtobuf<PublishedFileDetails_Child>, PublishedFileDetails_Child
{
    public PublishedFileDetails_ChildImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public ulong Publishedfileid
    { get => Accessor.GetUInt64("publishedfileid"); set => Accessor.SetUInt64("publishedfileid", value); }
    public uint Sortorder
    { get => Accessor.GetUInt32("sortorder"); set => Accessor.SetUInt32("sortorder", value); }
    public uint FileType
    { get => Accessor.GetUInt32("file_type"); set => Accessor.SetUInt32("file_type", value); }
}