
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface GameServerPing : ITypedProtobuf<GameServerPing>
{
  static GameServerPing ITypedProtobuf<GameServerPing>.Wrap(nint handle, bool isManuallyAllocated) => new GameServerPingImpl(handle, isManuallyAllocated);


  public int Ping { get; set; }


  public uint Ip { get; set; }


  public uint Instances { get; set; }

}
