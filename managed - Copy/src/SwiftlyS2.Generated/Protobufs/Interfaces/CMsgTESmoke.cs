
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgTESmoke : ITypedProtobuf<CMsgTESmoke>, INetMessage<CMsgTESmoke>, IDisposable
{
  static int INetMessage<CMsgTESmoke>.MessageId => 426;
  
  static string INetMessage<CMsgTESmoke>.MessageName => "CMsgTESmoke";

  static CMsgTESmoke ITypedProtobuf<CMsgTESmoke>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTESmokeImpl(handle, isManuallyAllocated);


  public Vector Origin { get; set; }


  public float Scale { get; set; }

}
