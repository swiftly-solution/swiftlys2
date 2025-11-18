
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageLagCompensationError : ITypedProtobuf<CUserMessageLagCompensationError>, INetMessage<CUserMessageLagCompensationError>, IDisposable
{
  static int INetMessage<CUserMessageLagCompensationError>.MessageId => 155;
  
  static string INetMessage<CUserMessageLagCompensationError>.MessageName => "CUserMessageLagCompensationError";

  static CUserMessageLagCompensationError ITypedProtobuf<CUserMessageLagCompensationError>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageLagCompensationErrorImpl(handle, isManuallyAllocated);


  public float Distance { get; set; }

}
