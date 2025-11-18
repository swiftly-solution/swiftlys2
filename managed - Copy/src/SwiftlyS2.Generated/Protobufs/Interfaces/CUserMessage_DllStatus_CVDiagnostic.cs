
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMessage_DllStatus_CVDiagnostic : ITypedProtobuf<CUserMessage_DllStatus_CVDiagnostic>
{
  static CUserMessage_DllStatus_CVDiagnostic ITypedProtobuf<CUserMessage_DllStatus_CVDiagnostic>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessage_DllStatus_CVDiagnosticImpl(handle, isManuallyAllocated);


  public uint Id { get; set; }


  public uint Extended { get; set; }


  public ulong Value { get; set; }


  public string StringValue { get; set; }

}
