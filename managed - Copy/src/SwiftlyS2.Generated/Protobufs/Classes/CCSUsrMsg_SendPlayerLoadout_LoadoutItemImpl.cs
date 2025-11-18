
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_SendPlayerLoadout_LoadoutItemImpl : TypedProtobuf<CCSUsrMsg_SendPlayerLoadout_LoadoutItem>, CCSUsrMsg_SendPlayerLoadout_LoadoutItem
{
  public CCSUsrMsg_SendPlayerLoadout_LoadoutItemImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public CEconItemPreviewDataBlock EconItem
  { get => new CEconItemPreviewDataBlockImpl(NativeNetMessages.GetNestedMessage(Address, "econ_item"), false); }


  public int Team
  { get => Accessor.GetInt32("team"); set => Accessor.SetInt32("team", value); }


  public int Slot
  { get => Accessor.GetInt32("slot"); set => Accessor.SetInt32("slot", value); }

}
