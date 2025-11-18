
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_Broadcast_CommandImpl : NetMessage<CSVCMsg_Broadcast_Command>, CSVCMsg_Broadcast_Command
{
  public CSVCMsg_Broadcast_CommandImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public string Cmd
  { get => Accessor.GetString("cmd"); set => Accessor.SetString("cmd", value); }

}
