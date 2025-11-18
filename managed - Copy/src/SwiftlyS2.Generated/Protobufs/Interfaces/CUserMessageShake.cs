
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageShake : ITypedProtobuf<CUserMessageShake>, INetMessage<CUserMessageShake>, IDisposable
{
  static int INetMessage<CUserMessageShake>.MessageId => 120;
  
  static string INetMessage<CUserMessageShake>.MessageName => "CUserMessageShake";

  static CUserMessageShake ITypedProtobuf<CUserMessageShake>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageShakeImpl(handle, isManuallyAllocated);


  public uint Command { get; set; }


  public float Amplitude { get; set; }


  public float Frequency { get; set; }


  public float Duration { get; set; }

}
