
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgTELargeFunnel : ITypedProtobuf<CMsgTELargeFunnel>, INetMessage<CMsgTELargeFunnel>, IDisposable
{
  static int INetMessage<CMsgTELargeFunnel>.MessageId => 421;
  
  static string INetMessage<CMsgTELargeFunnel>.MessageName => "CMsgTELargeFunnel";

  static CMsgTELargeFunnel ITypedProtobuf<CMsgTELargeFunnel>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTELargeFunnelImpl(handle, isManuallyAllocated);


  public Vector Origin { get; set; }


  public uint Reversed { get; set; }

}
