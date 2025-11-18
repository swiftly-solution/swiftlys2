
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgTESparks : ITypedProtobuf<CMsgTESparks>, INetMessage<CMsgTESparks>, IDisposable
{
  static int INetMessage<CMsgTESparks>.MessageId => 422;
  
  static string INetMessage<CMsgTESparks>.MessageName => "CMsgTESparks";

  static CMsgTESparks ITypedProtobuf<CMsgTESparks>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTESparksImpl(handle, isManuallyAllocated);


  public Vector Origin { get; set; }


  public uint Magnitude { get; set; }


  public uint Length { get; set; }


  public Vector Direction { get; set; }

}
