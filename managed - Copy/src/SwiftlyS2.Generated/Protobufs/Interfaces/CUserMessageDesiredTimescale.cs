
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageDesiredTimescale : ITypedProtobuf<CUserMessageDesiredTimescale>, INetMessage<CUserMessageDesiredTimescale>, IDisposable
{
  static int INetMessage<CUserMessageDesiredTimescale>.MessageId => 105;
  
  static string INetMessage<CUserMessageDesiredTimescale>.MessageName => "CUserMessageDesiredTimescale";

  static CUserMessageDesiredTimescale ITypedProtobuf<CUserMessageDesiredTimescale>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageDesiredTimescaleImpl(handle, isManuallyAllocated);


  public float Desired { get; set; }


  public float Acceleration { get; set; }


  public float Minblendrate { get; set; }


  public float Blenddeltamultiplier { get; set; }

}
