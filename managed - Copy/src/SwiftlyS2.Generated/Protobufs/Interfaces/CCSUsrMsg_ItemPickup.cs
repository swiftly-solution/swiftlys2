
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_ItemPickup : ITypedProtobuf<CCSUsrMsg_ItemPickup>, INetMessage<CCSUsrMsg_ItemPickup>, IDisposable
{
  static int INetMessage<CCSUsrMsg_ItemPickup>.MessageId => 353;
  
  static string INetMessage<CCSUsrMsg_ItemPickup>.MessageName => "CCSUsrMsg_ItemPickup";

  static CCSUsrMsg_ItemPickup ITypedProtobuf<CCSUsrMsg_ItemPickup>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_ItemPickupImpl(handle, isManuallyAllocated);


  public string Item { get; set; }

}
