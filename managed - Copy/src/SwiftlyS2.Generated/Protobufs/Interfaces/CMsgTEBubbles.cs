
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgTEBubbles : ITypedProtobuf<CMsgTEBubbles>, INetMessage<CMsgTEBubbles>, IDisposable
{
  static int INetMessage<CMsgTEBubbles>.MessageId => 408;
  
  static string INetMessage<CMsgTEBubbles>.MessageName => "CMsgTEBubbles";

  static CMsgTEBubbles ITypedProtobuf<CMsgTEBubbles>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTEBubblesImpl(handle, isManuallyAllocated);


  public Vector Mins { get; set; }


  public Vector Maxs { get; set; }


  public float Height { get; set; }


  public uint Count { get; set; }


  public float Speed { get; set; }

}
