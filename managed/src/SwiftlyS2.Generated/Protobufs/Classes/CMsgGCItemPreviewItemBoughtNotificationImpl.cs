using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCItemPreviewItemBoughtNotificationImpl : TypedProtobuf<CMsgGCItemPreviewItemBoughtNotification>, CMsgGCItemPreviewItemBoughtNotification
{
    public CMsgGCItemPreviewItemBoughtNotificationImpl( nint handle, bool isManuallyAllocated ) : base(handle)
    {
    }


    public uint ItemDefIndex { get => Accessor.GetUInt32("item_def_index"); set => Accessor.SetUInt32("item_def_index", value); }

}
