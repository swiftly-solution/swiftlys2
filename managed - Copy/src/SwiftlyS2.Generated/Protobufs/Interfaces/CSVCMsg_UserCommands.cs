
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSVCMsg_UserCommands : ITypedProtobuf<CSVCMsg_UserCommands>
{
  static CSVCMsg_UserCommands ITypedProtobuf<CSVCMsg_UserCommands>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_UserCommandsImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CMsgServerUserCmd> Commands { get; }

}
