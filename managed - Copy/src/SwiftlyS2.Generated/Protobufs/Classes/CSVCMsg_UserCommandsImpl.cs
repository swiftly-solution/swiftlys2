
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_UserCommandsImpl : TypedProtobuf<CSVCMsg_UserCommands>, CSVCMsg_UserCommands
{
  public CSVCMsg_UserCommandsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CMsgServerUserCmd> Commands
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgServerUserCmd>(Accessor, "commands"); }

}
