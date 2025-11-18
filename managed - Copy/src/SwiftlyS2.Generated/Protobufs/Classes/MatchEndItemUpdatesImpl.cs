
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class MatchEndItemUpdatesImpl : TypedProtobuf<MatchEndItemUpdates>, MatchEndItemUpdates
{
  public MatchEndItemUpdatesImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong ItemId
  { get => Accessor.GetUInt64("item_id"); set => Accessor.SetUInt64("item_id", value); }


  public uint ItemAttrDefidx
  { get => Accessor.GetUInt32("item_attr_defidx"); set => Accessor.SetUInt32("item_attr_defidx", value); }


  public uint ItemAttrDeltaValue
  { get => Accessor.GetUInt32("item_attr_delta_value"); set => Accessor.SetUInt32("item_attr_delta_value", value); }

}
