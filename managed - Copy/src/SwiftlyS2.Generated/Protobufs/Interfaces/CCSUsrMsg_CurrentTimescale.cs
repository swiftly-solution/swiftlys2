
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_CurrentTimescale : ITypedProtobuf<CCSUsrMsg_CurrentTimescale>, INetMessage<CCSUsrMsg_CurrentTimescale>, IDisposable
{
  static int INetMessage<CCSUsrMsg_CurrentTimescale>.MessageId => 332;
  
  static string INetMessage<CCSUsrMsg_CurrentTimescale>.MessageName => "CCSUsrMsg_CurrentTimescale";

  static CCSUsrMsg_CurrentTimescale ITypedProtobuf<CCSUsrMsg_CurrentTimescale>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_CurrentTimescaleImpl(handle, isManuallyAllocated);


  public float CurTimescale { get; set; }

}
