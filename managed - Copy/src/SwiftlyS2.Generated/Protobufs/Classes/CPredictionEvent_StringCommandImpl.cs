
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPredictionEvent_StringCommandImpl : TypedProtobuf<CPredictionEvent_StringCommand>, CPredictionEvent_StringCommand
{
  public CPredictionEvent_StringCommandImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string Command
  { get => Accessor.GetString("command"); set => Accessor.SetString("command", value); }

}
