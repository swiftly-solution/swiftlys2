
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_ItemDrop : ITypedProtobuf<CCSUsrMsg_ItemDrop>, INetMessage<CCSUsrMsg_ItemDrop>, IDisposable
{
  static int INetMessage<CCSUsrMsg_ItemDrop>.MessageId => 359;
  
  static string INetMessage<CCSUsrMsg_ItemDrop>.MessageName => "CCSUsrMsg_ItemDrop";

  static CCSUsrMsg_ItemDrop ITypedProtobuf<CCSUsrMsg_ItemDrop>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_ItemDropImpl(handle, isManuallyAllocated);


  public long Itemid { get; set; }


  public bool Death { get; set; }

}
