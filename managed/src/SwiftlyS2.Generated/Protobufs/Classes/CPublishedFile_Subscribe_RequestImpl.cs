using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPublishedFile_Subscribe_RequestImpl : TypedProtobuf<CPublishedFile_Subscribe_Request>, CPublishedFile_Subscribe_Request
{
    public CPublishedFile_Subscribe_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public ulong Publishedfileid
    { get => Accessor.GetUInt64("publishedfileid"); set => Accessor.SetUInt64("publishedfileid", value); }
    public uint ListType
    { get => Accessor.GetUInt32("list_type"); set => Accessor.SetUInt32("list_type", value); }
    public int Appid
    { get => Accessor.GetInt32("appid"); set => Accessor.SetInt32("appid", value); }
    public bool NotifyClient
    { get => Accessor.GetBool("notify_client"); set => Accessor.SetBool("notify_client", value); }
}