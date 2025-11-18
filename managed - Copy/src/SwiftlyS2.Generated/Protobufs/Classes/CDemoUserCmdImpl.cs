
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDemoUserCmdImpl : TypedProtobuf<CDemoUserCmd>, CDemoUserCmd
{
  public CDemoUserCmdImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int CmdNumber
  { get => Accessor.GetInt32("cmd_number"); set => Accessor.SetInt32("cmd_number", value); }


  public byte[] Data
  { get => Accessor.GetBytes("data"); set => Accessor.SetBytes("data", value); }

}
