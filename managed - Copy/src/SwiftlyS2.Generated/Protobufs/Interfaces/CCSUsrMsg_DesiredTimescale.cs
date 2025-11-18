
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_DesiredTimescale : ITypedProtobuf<CCSUsrMsg_DesiredTimescale>, INetMessage<CCSUsrMsg_DesiredTimescale>, IDisposable
{
  static int INetMessage<CCSUsrMsg_DesiredTimescale>.MessageId => 331;
  
  static string INetMessage<CCSUsrMsg_DesiredTimescale>.MessageName => "CCSUsrMsg_DesiredTimescale";

  static CCSUsrMsg_DesiredTimescale ITypedProtobuf<CCSUsrMsg_DesiredTimescale>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_DesiredTimescaleImpl(handle, isManuallyAllocated);


  public float DesiredTimescale { get; set; }


  public float DurationRealtimeSec { get; set; }


  public int InterpolatorType { get; set; }


  public float StartBlendTime { get; set; }

}
