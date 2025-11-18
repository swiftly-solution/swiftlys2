
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessage_DllStatusImpl : TypedProtobuf<CUserMessage_DllStatus>, CUserMessage_DllStatus
{
  public CUserMessage_DllStatusImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string FileReport
  { get => Accessor.GetString("file_report"); set => Accessor.SetString("file_report", value); }


  public string CommandLine
  { get => Accessor.GetString("command_line"); set => Accessor.SetString("command_line", value); }


  public uint TotalFiles
  { get => Accessor.GetUInt32("total_files"); set => Accessor.SetUInt32("total_files", value); }


  public uint ProcessId
  { get => Accessor.GetUInt32("process_id"); set => Accessor.SetUInt32("process_id", value); }


  public int Osversion
  { get => Accessor.GetInt32("osversion"); set => Accessor.SetInt32("osversion", value); }


  public ulong ClientTime
  { get => Accessor.GetUInt64("client_time"); set => Accessor.SetUInt64("client_time", value); }


  public IProtobufRepeatedFieldSubMessageType<CUserMessage_DllStatus_CVDiagnostic> Diagnostics
  { get => new ProtobufRepeatedFieldSubMessageType<CUserMessage_DllStatus_CVDiagnostic>(Accessor, "diagnostics"); }


  public IProtobufRepeatedFieldSubMessageType<CUserMessage_DllStatus_CModule> Modules
  { get => new ProtobufRepeatedFieldSubMessageType<CUserMessage_DllStatus_CModule>(Accessor, "modules"); }

}
