using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgCraftItemsRequestImpl : TypedProtobuf<CMsgCraftItemsRequest>, CMsgCraftItemsRequest
{
    public CMsgCraftItemsRequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint RecipeDef
    { get => Accessor.GetUInt32("recipe_def"); set => Accessor.SetUInt32("recipe_def", value); }
    public IProtobufRepeatedFieldValueType<ulong> CraftItems
    { get => new ProtobufRepeatedFieldValueType<ulong>(Accessor, "craft_items"); }
}