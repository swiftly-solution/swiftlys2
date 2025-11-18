
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgServerNetworkStats_Player : ITypedProtobuf<CMsgServerNetworkStats_Player>
{
  static CMsgServerNetworkStats_Player ITypedProtobuf<CMsgServerNetworkStats_Player>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgServerNetworkStats_PlayerImpl(handle, isManuallyAllocated);


  public ulong Steamid { get; set; }


  public string RemoteAddr { get; set; }


  public int PingAvgMs { get; set; }


  public float PacketLossPct { get; set; }


  public bool IsBot { get; set; }


  public float LossIn { get; set; }


  public float LossOut { get; set; }


  public int EngineLatencyMs { get; set; }

}
