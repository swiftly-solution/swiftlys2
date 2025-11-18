
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessage_Diagnostic_ResponseImpl : TypedProtobuf<CUserMessage_Diagnostic_Response>, CUserMessage_Diagnostic_Response
{
  public CUserMessage_Diagnostic_ResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CUserMessage_Diagnostic_Response_Diagnostic> Diagnostics
  { get => new ProtobufRepeatedFieldSubMessageType<CUserMessage_Diagnostic_Response_Diagnostic>(Accessor, "diagnostics"); }


  public int BuildVersion
  { get => Accessor.GetInt32("build_version"); set => Accessor.SetInt32("build_version", value); }


  public int Instance
  { get => Accessor.GetInt32("instance"); set => Accessor.SetInt32("instance", value); }


  public long StartTime
  { get => Accessor.GetInt64("start_time"); set => Accessor.SetInt64("start_time", value); }


  public int Osversion
  { get => Accessor.GetInt32("osversion"); set => Accessor.SetInt32("osversion", value); }


  public int Platform
  { get => Accessor.GetInt32("platform"); set => Accessor.SetInt32("platform", value); }

}
