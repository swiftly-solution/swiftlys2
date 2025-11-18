
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgTEBubbleTrail : ITypedProtobuf<CMsgTEBubbleTrail>, INetMessage<CMsgTEBubbleTrail>, IDisposable
{
  static int INetMessage<CMsgTEBubbleTrail>.MessageId => 409;
  
  static string INetMessage<CMsgTEBubbleTrail>.MessageName => "CMsgTEBubbleTrail";

  static CMsgTEBubbleTrail ITypedProtobuf<CMsgTEBubbleTrail>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTEBubbleTrailImpl(handle, isManuallyAllocated);


  public Vector Mins { get; set; }


  public Vector Maxs { get; set; }


  public float Waterz { get; set; }


  public uint Count { get; set; }


  public float Speed { get; set; }

}
