
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_ClientPerfReportImpl : TypedProtobuf<CMsgGCCStrike15_v2_ClientPerfReport>, CMsgGCCStrike15_v2_ClientPerfReport
{
  public CMsgGCCStrike15_v2_ClientPerfReportImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_ClientPerfReport_Entry> Entries
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_ClientPerfReport_Entry>(Accessor, "entries"); }

}
