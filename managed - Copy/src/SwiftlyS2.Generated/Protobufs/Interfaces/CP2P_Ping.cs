
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CP2P_Ping : ITypedProtobuf<CP2P_Ping>
{
  static CP2P_Ping ITypedProtobuf<CP2P_Ping>.Wrap(nint handle, bool isManuallyAllocated) => new CP2P_PingImpl(handle, isManuallyAllocated);


  public ulong SendTime { get; set; }


  public bool IsReply { get; set; }

}
