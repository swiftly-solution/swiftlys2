
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientReportValidationImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientReportValidation>, CMsgGCCStrike15_v2_ClientReportValidation
{
  public CMsgGCCStrike15_v2_ClientReportValidationImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string FileReport
  { get => Accessor.GetString("file_report"); set => Accessor.SetString("file_report", value); }


  public string CommandLine
  { get => Accessor.GetString("command_line"); set => Accessor.SetString("command_line", value); }


  public uint TotalFiles
  { get => Accessor.GetUInt32("total_files"); set => Accessor.SetUInt32("total_files", value); }


  public uint InternalError
  { get => Accessor.GetUInt32("internal_error"); set => Accessor.SetUInt32("internal_error", value); }


  public uint TrustTime
  { get => Accessor.GetUInt32("trust_time"); set => Accessor.SetUInt32("trust_time", value); }


  public uint CountPending
  { get => Accessor.GetUInt32("count_pending"); set => Accessor.SetUInt32("count_pending", value); }


  public uint CountCompleted
  { get => Accessor.GetUInt32("count_completed"); set => Accessor.SetUInt32("count_completed", value); }


  public uint ProcessId
  { get => Accessor.GetUInt32("process_id"); set => Accessor.SetUInt32("process_id", value); }


  public int Osversion
  { get => Accessor.GetInt32("osversion"); set => Accessor.SetInt32("osversion", value); }


  public uint Clientreportversion
  { get => Accessor.GetUInt32("clientreportversion"); set => Accessor.SetUInt32("clientreportversion", value); }


  public uint StatusId
  { get => Accessor.GetUInt32("status_id"); set => Accessor.SetUInt32("status_id", value); }


  public uint Diagnostic1
  { get => Accessor.GetUInt32("diagnostic1"); set => Accessor.SetUInt32("diagnostic1", value); }


  public ulong Diagnostic2
  { get => Accessor.GetUInt64("diagnostic2"); set => Accessor.SetUInt64("diagnostic2", value); }


  public ulong Diagnostic3
  { get => Accessor.GetUInt64("diagnostic3"); set => Accessor.SetUInt64("diagnostic3", value); }


  public string LastLaunchData
  { get => Accessor.GetString("last_launch_data"); set => Accessor.SetString("last_launch_data", value); }


  public uint ReportCount
  { get => Accessor.GetUInt32("report_count"); set => Accessor.SetUInt32("report_count", value); }


  public ulong ClientTime
  { get => Accessor.GetUInt64("client_time"); set => Accessor.SetUInt64("client_time", value); }


  public ulong Diagnostic4
  { get => Accessor.GetUInt64("diagnostic4"); set => Accessor.SetUInt64("diagnostic4", value); }


  public ulong Diagnostic5
  { get => Accessor.GetUInt64("diagnostic5"); set => Accessor.SetUInt64("diagnostic5", value); }


  public IProtobufRepeatedFieldSubMessageType<CVDiagnostic> Diagnostics
  { get => new ProtobufRepeatedFieldSubMessageType<CVDiagnostic>(Accessor, "diagnostics"); }

}
