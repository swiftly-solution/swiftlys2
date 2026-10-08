using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgDeleteItemImpl : TypedProtobuf<CMsgDeleteItem>, CMsgDeleteItem
{
    public CMsgDeleteItemImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public ulong ItemId
    { get => Accessor.GetUInt64("item_id"); set => Accessor.SetUInt64("item_id", value); }
}