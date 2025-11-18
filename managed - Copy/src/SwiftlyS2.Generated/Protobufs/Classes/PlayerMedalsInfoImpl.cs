
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class PlayerMedalsInfoImpl : TypedProtobuf<PlayerMedalsInfo>, PlayerMedalsInfo
{
  public PlayerMedalsInfoImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldValueType<uint> DisplayItemsDefidx
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "display_items_defidx"); }


  public uint FeaturedDisplayItemDefidx
  { get => Accessor.GetUInt32("featured_display_item_defidx"); set => Accessor.SetUInt32("featured_display_item_defidx", value); }

}
