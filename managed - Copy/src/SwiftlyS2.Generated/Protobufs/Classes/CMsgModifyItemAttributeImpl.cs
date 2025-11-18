
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgModifyItemAttributeImpl : TypedProtobuf<CMsgModifyItemAttribute>, CMsgModifyItemAttribute
{
  public CMsgModifyItemAttributeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong ItemId
  { get => Accessor.GetUInt64("item_id"); set => Accessor.SetUInt64("item_id", value); }


  public uint AttrDefidx
  { get => Accessor.GetUInt32("attr_defidx"); set => Accessor.SetUInt32("attr_defidx", value); }


  public uint AttrValue
  { get => Accessor.GetUInt32("attr_value"); set => Accessor.SetUInt32("attr_value", value); }

}
