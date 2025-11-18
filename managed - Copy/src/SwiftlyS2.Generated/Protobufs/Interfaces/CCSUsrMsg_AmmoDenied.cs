
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_AmmoDenied : ITypedProtobuf<CCSUsrMsg_AmmoDenied>, INetMessage<CCSUsrMsg_AmmoDenied>, IDisposable
{
  static int INetMessage<CCSUsrMsg_AmmoDenied>.MessageId => 356;
  
  static string INetMessage<CCSUsrMsg_AmmoDenied>.MessageName => "CCSUsrMsg_AmmoDenied";

  static CCSUsrMsg_AmmoDenied ITypedProtobuf<CCSUsrMsg_AmmoDenied>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_AmmoDeniedImpl(handle, isManuallyAllocated);


  public int Ammoidx { get; set; }

}
