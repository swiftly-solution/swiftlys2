
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_Fantasy_FantasySlotImpl : TypedProtobuf<CMsgGCCStrike15_v2_Fantasy_FantasySlot>, CMsgGCCStrike15_v2_Fantasy_FantasySlot
{
  public CMsgGCCStrike15_v2_Fantasy_FantasySlotImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Type
  { get => Accessor.GetInt32("type"); set => Accessor.SetInt32("type", value); }


  public int Pick
  { get => Accessor.GetInt32("pick"); set => Accessor.SetInt32("pick", value); }


  public ulong Itemid
  { get => Accessor.GetUInt64("itemid"); set => Accessor.SetUInt64("itemid", value); }

}
