
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgServerUserCmd : ITypedProtobuf<CMsgServerUserCmd>
{
  static CMsgServerUserCmd ITypedProtobuf<CMsgServerUserCmd>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgServerUserCmdImpl(handle, isManuallyAllocated);


  public byte[] Data { get; set; }


  public int CmdNumber { get; set; }


  public int PlayerSlot { get; set; }


  public int ServerTickExecuted { get; set; }


  public int ClientTick { get; set; }

}
