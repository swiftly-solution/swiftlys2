
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgServerNetworkStats_PlayerImpl : TypedProtobuf<CMsgServerNetworkStats_Player>, CMsgServerNetworkStats_Player
{
  public CMsgServerNetworkStats_PlayerImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Steamid
  { get => Accessor.GetUInt64("steamid"); set => Accessor.SetUInt64("steamid", value); }


  public string RemoteAddr
  { get => Accessor.GetString("remote_addr"); set => Accessor.SetString("remote_addr", value); }


  public int PingAvgMs
  { get => Accessor.GetInt32("ping_avg_ms"); set => Accessor.SetInt32("ping_avg_ms", value); }


  public float PacketLossPct
  { get => Accessor.GetFloat("packet_loss_pct"); set => Accessor.SetFloat("packet_loss_pct", value); }


  public bool IsBot
  { get => Accessor.GetBool("is_bot"); set => Accessor.SetBool("is_bot", value); }


  public float LossIn
  { get => Accessor.GetFloat("loss_in"); set => Accessor.SetFloat("loss_in", value); }


  public float LossOut
  { get => Accessor.GetFloat("loss_out"); set => Accessor.SetFloat("loss_out", value); }


  public int EngineLatencyMs
  { get => Accessor.GetInt32("engine_latency_ms"); set => Accessor.SetInt32("engine_latency_ms", value); }

}
