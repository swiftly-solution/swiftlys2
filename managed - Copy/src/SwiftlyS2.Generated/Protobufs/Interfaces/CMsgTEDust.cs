
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgTEDust : ITypedProtobuf<CMsgTEDust>, INetMessage<CMsgTEDust>, IDisposable
{
  static int INetMessage<CMsgTEDust>.MessageId => 420;
  
  static string INetMessage<CMsgTEDust>.MessageName => "CMsgTEDust";

  static CMsgTEDust ITypedProtobuf<CMsgTEDust>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTEDustImpl(handle, isManuallyAllocated);


  public Vector Origin { get; set; }


  public float Size { get; set; }


  public float Speed { get; set; }


  public Vector Direction { get; set; }

}
