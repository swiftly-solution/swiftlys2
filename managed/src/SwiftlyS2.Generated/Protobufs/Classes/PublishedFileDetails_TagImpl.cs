using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class PublishedFileDetails_TagImpl : TypedProtobuf<PublishedFileDetails_Tag>, PublishedFileDetails_Tag
{
    public PublishedFileDetails_TagImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public string Tag
    { get => Accessor.GetString("tag"); set => Accessor.SetString("tag", value); }
    public bool Adminonly
    { get => Accessor.GetBool("adminonly"); set => Accessor.SetBool("adminonly", value); }
}