using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgCraftItemsRequest : ITypedProtobuf<CMsgCraftItemsRequest>
{
    static CMsgCraftItemsRequest ITypedProtobuf<CMsgCraftItemsRequest>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgCraftItemsRequestImpl(handle, isManuallyAllocated);

    public uint RecipeDef { get; set; }
    public IProtobufRepeatedFieldValueType<ulong> CraftItems { get; }
}