
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGameServerInfo : ITypedProtobuf<CMsgGameServerInfo>
{
  static CMsgGameServerInfo ITypedProtobuf<CMsgGameServerInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGameServerInfoImpl(handle, isManuallyAllocated);


  public uint ServerPublicIpAddr { get; set; }


  public uint ServerPrivateIpAddr { get; set; }


  public uint ServerPort { get; set; }


  public uint ServerTvPort { get; set; }


  public string ServerKey { get; set; }


  public bool ServerHibernation { get; set; }


  public CMsgGameServerInfo_ServerType ServerType { get; set; }


  public uint ServerRegion { get; set; }


  public float ServerLoadavg { get; set; }


  public float ServerTvBroadcastTime { get; set; }


  public float ServerGameTime { get; set; }


  public ulong ServerRelayConnectedSteamId { get; set; }


  public uint RelaySlotsMax { get; set; }


  public int RelaysConnected { get; set; }


  public int RelayClientsConnected { get; set; }


  public ulong RelayedGameServerSteamId { get; set; }


  public uint ParentRelayCount { get; set; }


  public ulong TvSecretCode { get; set; }

}
