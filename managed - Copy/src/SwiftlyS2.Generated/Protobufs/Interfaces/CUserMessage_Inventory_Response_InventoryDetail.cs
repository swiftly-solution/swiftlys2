
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMessage_Inventory_Response_InventoryDetail : ITypedProtobuf<CUserMessage_Inventory_Response_InventoryDetail>
{
  static CUserMessage_Inventory_Response_InventoryDetail ITypedProtobuf<CUserMessage_Inventory_Response_InventoryDetail>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessage_Inventory_Response_InventoryDetailImpl(handle, isManuallyAllocated);


  public int Index { get; set; }


  public long Primary { get; set; }


  public long Offset { get; set; }


  public long First { get; set; }


  public long Base { get; set; }


  public string Name { get; set; }


  public string BaseName { get; set; }


  public int BaseDetail { get; set; }


  public int BaseTime { get; set; }


  public int BaseHash { get; set; }

}
