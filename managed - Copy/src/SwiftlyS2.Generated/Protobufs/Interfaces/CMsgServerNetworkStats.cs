
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgServerNetworkStats : ITypedProtobuf<CMsgServerNetworkStats>
{
  static CMsgServerNetworkStats ITypedProtobuf<CMsgServerNetworkStats>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgServerNetworkStatsImpl(handle, isManuallyAllocated);


  public bool Dedicated { get; set; }


  public int CpuUsage { get; set; }


  public int MemoryUsedMb { get; set; }


  public int MemoryFreeMb { get; set; }


  public int Uptime { get; set; }


  public int SpawnCount { get; set; }


  public int NumClients { get; set; }


  public int NumBots { get; set; }


  public int NumSpectators { get; set; }


  public int NumTvRelays { get; set; }


  public float Fps { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CMsgServerNetworkStats_Port> Ports { get; }


  public float AvgPingMs { get; set; }


  public float AvgEngineLatencyOut { get; set; }


  public float AvgPacketsOut { get; set; }


  public float AvgPacketsIn { get; set; }


  public float AvgLossOut { get; set; }


  public float AvgLossIn { get; set; }


  public float AvgDataOut { get; set; }


  public float AvgDataIn { get; set; }


  public ulong TotalDataIn { get; set; }


  public ulong TotalPacketsIn { get; set; }


  public ulong TotalDataOut { get; set; }


  public ulong TotalPacketsOut { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CMsgServerNetworkStats_Player> Players { get; }

}
