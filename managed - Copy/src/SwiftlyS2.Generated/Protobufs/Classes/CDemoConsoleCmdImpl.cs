
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDemoConsoleCmdImpl : TypedProtobuf<CDemoConsoleCmd>, CDemoConsoleCmd
{
  public CDemoConsoleCmdImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string Cmdstring
  { get => Accessor.GetString("cmdstring"); set => Accessor.SetString("cmdstring", value); }

}
