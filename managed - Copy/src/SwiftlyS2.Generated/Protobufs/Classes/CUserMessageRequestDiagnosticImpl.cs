
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageRequestDiagnosticImpl : NetMessage<CUserMessageRequestDiagnostic>, CUserMessageRequestDiagnostic
{
  public CUserMessageRequestDiagnosticImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CUserMessageRequestDiagnostic_Diagnostic> Diagnostics
  { get => new ProtobufRepeatedFieldSubMessageType<CUserMessageRequestDiagnostic_Diagnostic>(Accessor, "diagnostics"); }

}
