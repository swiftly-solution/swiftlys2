
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageRequestInventory : ITypedProtobuf<CUserMessageRequestInventory>, INetMessage<CUserMessageRequestInventory>, IDisposable
{
  static int INetMessage<CUserMessageRequestInventory>.MessageId => 160;
  
  static string INetMessage<CUserMessageRequestInventory>.MessageName => "CUserMessageRequestInventory";

  static CUserMessageRequestInventory ITypedProtobuf<CUserMessageRequestInventory>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageRequestInventoryImpl(handle, isManuallyAllocated);


  public int Inventory { get; set; }


  public int Offset { get; set; }


  public int Options { get; set; }

}
