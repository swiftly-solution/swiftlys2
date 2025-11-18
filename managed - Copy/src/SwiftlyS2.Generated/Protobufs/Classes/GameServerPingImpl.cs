
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class GameServerPingImpl : TypedProtobuf<GameServerPing>, GameServerPing
{
  public GameServerPingImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Ping
  { get => Accessor.GetInt32("ping"); set => Accessor.SetInt32("ping", value); }


  public uint Ip
  { get => Accessor.GetUInt32("ip"); set => Accessor.SetUInt32("ip", value); }


  public uint Instances
  { get => Accessor.GetUInt32("instances"); set => Accessor.SetUInt32("instances", value); }

}
