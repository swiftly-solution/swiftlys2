
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageCurrentTimescale : ITypedProtobuf<CUserMessageCurrentTimescale>, INetMessage<CUserMessageCurrentTimescale>, IDisposable
{
  static int INetMessage<CUserMessageCurrentTimescale>.MessageId => 104;
  
  static string INetMessage<CUserMessageCurrentTimescale>.MessageName => "CUserMessageCurrentTimescale";

  static CUserMessageCurrentTimescale ITypedProtobuf<CUserMessageCurrentTimescale>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageCurrentTimescaleImpl(handle, isManuallyAllocated);


  public float Current { get; set; }

}
