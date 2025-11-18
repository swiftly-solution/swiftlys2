
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_XRankGet : ITypedProtobuf<CCSUsrMsg_XRankGet>, INetMessage<CCSUsrMsg_XRankGet>, IDisposable
{
  static int INetMessage<CCSUsrMsg_XRankGet>.MessageId => 340;
  
  static string INetMessage<CCSUsrMsg_XRankGet>.MessageName => "CCSUsrMsg_XRankGet";

  static CCSUsrMsg_XRankGet ITypedProtobuf<CCSUsrMsg_XRankGet>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_XRankGetImpl(handle, isManuallyAllocated);


  public int ModeIdx { get; set; }


  public int Controller { get; set; }

}
