
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgTEImpact : ITypedProtobuf<CMsgTEImpact>, INetMessage<CMsgTEImpact>, IDisposable
{
  static int INetMessage<CMsgTEImpact>.MessageId => 416;
  
  static string INetMessage<CMsgTEImpact>.MessageName => "CMsgTEImpact";

  static CMsgTEImpact ITypedProtobuf<CMsgTEImpact>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTEImpactImpl(handle, isManuallyAllocated);


  public Vector Origin { get; set; }


  public Vector Normal { get; set; }


  public uint Type { get; set; }

}
