
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgServerNetworkStatsImpl : TypedProtobuf<CMsgServerNetworkStats>, CMsgServerNetworkStats
{
  public CMsgServerNetworkStatsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool Dedicated
  { get => Accessor.GetBool("dedicated"); set => Accessor.SetBool("dedicated", value); }


  public int CpuUsage
  { get => Accessor.GetInt32("cpu_usage"); set => Accessor.SetInt32("cpu_usage", value); }


  public int MemoryUsedMb
  { get => Accessor.GetInt32("memory_used_mb"); set => Accessor.SetInt32("memory_used_mb", value); }


  public int MemoryFreeMb
  { get => Accessor.GetInt32("memory_free_mb"); set => Accessor.SetInt32("memory_free_mb", value); }


  public int Uptime
  { get => Accessor.GetInt32("uptime"); set => Accessor.SetInt32("uptime", value); }


  public int SpawnCount
  { get => Accessor.GetInt32("spawn_count"); set => Accessor.SetInt32("spawn_count", value); }


  public int NumClients
  { get => Accessor.GetInt32("num_clients"); set => Accessor.SetInt32("num_clients", value); }


  public int NumBots
  { get => Accessor.GetInt32("num_bots"); set => Accessor.SetInt32("num_bots", value); }


  public int NumSpectators
  { get => Accessor.GetInt32("num_spectators"); set => Accessor.SetInt32("num_spectators", value); }


  public int NumTvRelays
  { get => Accessor.GetInt32("num_tv_relays"); set => Accessor.SetInt32("num_tv_relays", value); }


  public float Fps
  { get => Accessor.GetFloat("fps"); set => Accessor.SetFloat("fps", value); }


  public IProtobufRepeatedFieldSubMessageType<CMsgServerNetworkStats_Port> Ports
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgServerNetworkStats_Port>(Accessor, "ports"); }


  public float AvgPingMs
  { get => Accessor.GetFloat("avg_ping_ms"); set => Accessor.SetFloat("avg_ping_ms", value); }


  public float AvgEngineLatencyOut
  { get => Accessor.GetFloat("avg_engine_latency_out"); set => Accessor.SetFloat("avg_engine_latency_out", value); }


  public float AvgPacketsOut
  { get => Accessor.GetFloat("avg_packets_out"); set => Accessor.SetFloat("avg_packets_out", value); }


  public float AvgPacketsIn
  { get => Accessor.GetFloat("avg_packets_in"); set => Accessor.SetFloat("avg_packets_in", value); }


  public float AvgLossOut
  { get => Accessor.GetFloat("avg_loss_out"); set => Accessor.SetFloat("avg_loss_out", value); }


  public float AvgLossIn
  { get => Accessor.GetFloat("avg_loss_in"); set => Accessor.SetFloat("avg_loss_in", value); }


  public float AvgDataOut
  { get => Accessor.GetFloat("avg_data_out"); set => Accessor.SetFloat("avg_data_out", value); }


  public float AvgDataIn
  { get => Accessor.GetFloat("avg_data_in"); set => Accessor.SetFloat("avg_data_in", value); }


  public ulong TotalDataIn
  { get => Accessor.GetUInt64("total_data_in"); set => Accessor.SetUInt64("total_data_in", value); }


  public ulong TotalPacketsIn
  { get => Accessor.GetUInt64("total_packets_in"); set => Accessor.SetUInt64("total_packets_in", value); }


  public ulong TotalDataOut
  { get => Accessor.GetUInt64("total_data_out"); set => Accessor.SetUInt64("total_data_out", value); }


  public ulong TotalPacketsOut
  { get => Accessor.GetUInt64("total_packets_out"); set => Accessor.SetUInt64("total_packets_out", value); }


  public IProtobufRepeatedFieldSubMessageType<CMsgServerNetworkStats_Player> Players
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgServerNetworkStats_Player>(Accessor, "players"); }

}
