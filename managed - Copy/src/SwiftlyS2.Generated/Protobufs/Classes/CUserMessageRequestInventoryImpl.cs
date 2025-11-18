
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageRequestInventoryImpl : NetMessage<CUserMessageRequestInventory>, CUserMessageRequestInventory
{
  public CUserMessageRequestInventoryImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Inventory
  { get => Accessor.GetInt32("inventory"); set => Accessor.SetInt32("inventory", value); }


  public int Offset
  { get => Accessor.GetInt32("offset"); set => Accessor.SetInt32("offset", value); }


  public int Options
  { get => Accessor.GetInt32("options"); set => Accessor.SetInt32("options", value); }

}
