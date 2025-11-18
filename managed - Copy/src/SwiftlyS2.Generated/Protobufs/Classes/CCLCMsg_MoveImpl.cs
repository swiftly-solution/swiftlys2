
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCLCMsg_MoveImpl : NetMessage<CCLCMsg_Move>, CCLCMsg_Move
{
  public CCLCMsg_MoveImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public byte[] Data
  { get => Accessor.GetBytes("data"); set => Accessor.SetBytes("data", value); }


  public uint LastCommandNumber
  { get => Accessor.GetUInt32("last_command_number"); set => Accessor.SetUInt32("last_command_number", value); }

}
