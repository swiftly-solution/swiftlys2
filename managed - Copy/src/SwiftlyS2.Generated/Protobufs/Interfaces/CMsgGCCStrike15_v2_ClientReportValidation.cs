
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientReportValidation : ITypedProtobuf<CMsgGCCStrike15_v2_ClientReportValidation>
{
  static CMsgGCCStrike15_v2_ClientReportValidation ITypedProtobuf<CMsgGCCStrike15_v2_ClientReportValidation>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientReportValidationImpl(handle, isManuallyAllocated);


  public string FileReport { get; set; }


  public string CommandLine { get; set; }


  public uint TotalFiles { get; set; }


  public uint InternalError { get; set; }


  public uint TrustTime { get; set; }


  public uint CountPending { get; set; }


  public uint CountCompleted { get; set; }


  public uint ProcessId { get; set; }


  public int Osversion { get; set; }


  public uint Clientreportversion { get; set; }


  public uint StatusId { get; set; }


  public uint Diagnostic1 { get; set; }


  public ulong Diagnostic2 { get; set; }


  public ulong Diagnostic3 { get; set; }


  public string LastLaunchData { get; set; }


  public uint ReportCount { get; set; }


  public ulong ClientTime { get; set; }


  public ulong Diagnostic4 { get; set; }


  public ulong Diagnostic5 { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CVDiagnostic> Diagnostics { get; }

}
