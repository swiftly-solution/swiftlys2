
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCToGCBroadcastConsoleCommandImpl : TypedProtobuf<CMsgGCToGCBroadcastConsoleCommand>, CMsgGCToGCBroadcastConsoleCommand
{
  public CMsgGCToGCBroadcastConsoleCommandImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string ConCommand
  { get => Accessor.GetString("con_command"); set => Accessor.SetString("con_command", value); }

}
