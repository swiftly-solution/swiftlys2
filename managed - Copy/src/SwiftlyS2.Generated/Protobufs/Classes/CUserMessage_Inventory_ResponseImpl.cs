
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessage_Inventory_ResponseImpl : TypedProtobuf<CUserMessage_Inventory_Response>, CUserMessage_Inventory_Response
{
  public CUserMessage_Inventory_ResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Crc
  { get => Accessor.GetUInt32("crc"); set => Accessor.SetUInt32("crc", value); }


  public int ItemCount
  { get => Accessor.GetInt32("item_count"); set => Accessor.SetInt32("item_count", value); }


  public int Osversion
  { get => Accessor.GetInt32("osversion"); set => Accessor.SetInt32("osversion", value); }


  public int PerfTime
  { get => Accessor.GetInt32("perf_time"); set => Accessor.SetInt32("perf_time", value); }


  public int ClientTimestamp
  { get => Accessor.GetInt32("client_timestamp"); set => Accessor.SetInt32("client_timestamp", value); }


  public int Platform
  { get => Accessor.GetInt32("platform"); set => Accessor.SetInt32("platform", value); }


  public IProtobufRepeatedFieldSubMessageType<CUserMessage_Inventory_Response_InventoryDetail> Inventories
  { get => new ProtobufRepeatedFieldSubMessageType<CUserMessage_Inventory_Response_InventoryDetail>(Accessor, "inventories"); }


  public IProtobufRepeatedFieldSubMessageType<CUserMessage_Inventory_Response_InventoryDetail> Inventories2
  { get => new ProtobufRepeatedFieldSubMessageType<CUserMessage_Inventory_Response_InventoryDetail>(Accessor, "inventories2"); }


  public IProtobufRepeatedFieldSubMessageType<CUserMessage_Inventory_Response_InventoryDetail> Inventories3
  { get => new ProtobufRepeatedFieldSubMessageType<CUserMessage_Inventory_Response_InventoryDetail>(Accessor, "inventories3"); }


  public int InvType
  { get => Accessor.GetInt32("inv_type"); set => Accessor.SetInt32("inv_type", value); }


  public int BuildVersion
  { get => Accessor.GetInt32("build_version"); set => Accessor.SetInt32("build_version", value); }


  public int Instance
  { get => Accessor.GetInt32("instance"); set => Accessor.SetInt32("instance", value); }


  public long StartTime
  { get => Accessor.GetInt64("start_time"); set => Accessor.SetInt64("start_time", value); }

}
