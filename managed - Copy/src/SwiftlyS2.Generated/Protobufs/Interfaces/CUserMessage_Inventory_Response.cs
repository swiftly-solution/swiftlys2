
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMessage_Inventory_Response : ITypedProtobuf<CUserMessage_Inventory_Response>
{
  static CUserMessage_Inventory_Response ITypedProtobuf<CUserMessage_Inventory_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessage_Inventory_ResponseImpl(handle, isManuallyAllocated);


  public uint Crc { get; set; }


  public int ItemCount { get; set; }


  public int Osversion { get; set; }


  public int PerfTime { get; set; }


  public int ClientTimestamp { get; set; }


  public int Platform { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CUserMessage_Inventory_Response_InventoryDetail> Inventories { get; }


  public IProtobufRepeatedFieldSubMessageType<CUserMessage_Inventory_Response_InventoryDetail> Inventories2 { get; }


  public IProtobufRepeatedFieldSubMessageType<CUserMessage_Inventory_Response_InventoryDetail> Inventories3 { get; }


  public int InvType { get; set; }


  public int BuildVersion { get; set; }


  public int Instance { get; set; }


  public long StartTime { get; set; }

}
