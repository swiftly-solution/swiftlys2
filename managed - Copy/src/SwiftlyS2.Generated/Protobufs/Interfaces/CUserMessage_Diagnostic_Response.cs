
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMessage_Diagnostic_Response : ITypedProtobuf<CUserMessage_Diagnostic_Response>
{
  static CUserMessage_Diagnostic_Response ITypedProtobuf<CUserMessage_Diagnostic_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessage_Diagnostic_ResponseImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CUserMessage_Diagnostic_Response_Diagnostic> Diagnostics { get; }


  public int BuildVersion { get; set; }


  public int Instance { get; set; }


  public long StartTime { get; set; }


  public int Osversion { get; set; }


  public int Platform { get; set; }

}
