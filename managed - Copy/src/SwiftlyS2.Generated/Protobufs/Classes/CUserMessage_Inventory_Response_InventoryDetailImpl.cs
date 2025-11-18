
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessage_Inventory_Response_InventoryDetailImpl : TypedProtobuf<CUserMessage_Inventory_Response_InventoryDetail>, CUserMessage_Inventory_Response_InventoryDetail
{
  public CUserMessage_Inventory_Response_InventoryDetailImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Index
  { get => Accessor.GetInt32("index"); set => Accessor.SetInt32("index", value); }


  public long Primary
  { get => Accessor.GetInt64("primary"); set => Accessor.SetInt64("primary", value); }


  public long Offset
  { get => Accessor.GetInt64("offset"); set => Accessor.SetInt64("offset", value); }


  public long First
  { get => Accessor.GetInt64("first"); set => Accessor.SetInt64("first", value); }


  public long Base
  { get => Accessor.GetInt64("base"); set => Accessor.SetInt64("base", value); }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public string BaseName
  { get => Accessor.GetString("base_name"); set => Accessor.SetString("base_name", value); }


  public int BaseDetail
  { get => Accessor.GetInt32("base_detail"); set => Accessor.SetInt32("base_detail", value); }


  public int BaseTime
  { get => Accessor.GetInt32("base_time"); set => Accessor.SetInt32("base_time", value); }


  public int BaseHash
  { get => Accessor.GetInt32("base_hash"); set => Accessor.SetInt32("base_hash", value); }

}
