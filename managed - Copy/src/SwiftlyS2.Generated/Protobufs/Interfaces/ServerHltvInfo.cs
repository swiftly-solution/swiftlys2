
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface ServerHltvInfo : ITypedProtobuf<ServerHltvInfo>
{
  static ServerHltvInfo ITypedProtobuf<ServerHltvInfo>.Wrap(nint handle, bool isManuallyAllocated) => new ServerHltvInfoImpl(handle, isManuallyAllocated);


  public uint TvUdpPort { get; set; }


  public ulong TvWatchKey { get; set; }


  public uint TvSlots { get; set; }


  public uint TvClients { get; set; }


  public uint TvProxies { get; set; }


  public uint TvTime { get; set; }


  public uint GameType { get; set; }


  public string GameMapgroup { get; set; }


  public string GameMap { get; set; }


  public ulong TvMasterSteamid { get; set; }


  public uint TvLocalSlots { get; set; }


  public uint TvLocalClients { get; set; }


  public uint TvLocalProxies { get; set; }


  public uint TvRelaySlots { get; set; }


  public uint TvRelayClients { get; set; }


  public uint TvRelayProxies { get; set; }


  public uint TvRelayAddress { get; set; }


  public uint TvRelayPort { get; set; }


  public ulong TvRelaySteamid { get; set; }


  public uint Flags { get; set; }

}
