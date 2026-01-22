
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CBaseUserCmdExecutionNotesImpl : TypedProtobuf<CBaseUserCmdExecutionNotes>, CBaseUserCmdExecutionNotes
{
  public CBaseUserCmdExecutionNotesImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string IgnoredReason
  { get => Accessor.GetString("ignored_reason"); set => Accessor.SetString("ignored_reason", value); }

}
