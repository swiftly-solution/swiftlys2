
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageRequestDiagnostic : ITypedProtobuf<CUserMessageRequestDiagnostic>, INetMessage<CUserMessageRequestDiagnostic>, IDisposable
{
  static int INetMessage<CUserMessageRequestDiagnostic>.MessageId => 162;
  
  static string INetMessage<CUserMessageRequestDiagnostic>.MessageName => "CUserMessageRequestDiagnostic";

  static CUserMessageRequestDiagnostic ITypedProtobuf<CUserMessageRequestDiagnostic>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageRequestDiagnosticImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CUserMessageRequestDiagnostic_Diagnostic> Diagnostics { get; }

}
