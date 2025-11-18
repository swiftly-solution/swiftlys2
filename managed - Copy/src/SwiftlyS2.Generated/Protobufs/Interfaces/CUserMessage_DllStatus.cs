
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMessage_DllStatus : ITypedProtobuf<CUserMessage_DllStatus>
{
  static CUserMessage_DllStatus ITypedProtobuf<CUserMessage_DllStatus>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessage_DllStatusImpl(handle, isManuallyAllocated);


  public string FileReport { get; set; }


  public string CommandLine { get; set; }


  public uint TotalFiles { get; set; }


  public uint ProcessId { get; set; }


  public int Osversion { get; set; }


  public ulong ClientTime { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CUserMessage_DllStatus_CVDiagnostic> Diagnostics { get; }


  public IProtobufRepeatedFieldSubMessageType<CUserMessage_DllStatus_CModule> Modules { get; }

}
