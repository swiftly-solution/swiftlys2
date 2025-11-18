
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CNETMsg_TickImpl : NetMessage<CNETMsg_Tick>, CNETMsg_Tick
{
  public CNETMsg_TickImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint Tick
  { get => Accessor.GetUInt32("tick"); set => Accessor.SetUInt32("tick", value); }


  public uint HostComputationtime
  { get => Accessor.GetUInt32("host_computationtime"); set => Accessor.SetUInt32("host_computationtime", value); }


  public uint HostComputationtimeStdDeviation
  { get => Accessor.GetUInt32("host_computationtime_std_deviation"); set => Accessor.SetUInt32("host_computationtime_std_deviation", value); }


  public uint LegacyHostLoss
  { get => Accessor.GetUInt32("legacy_host_loss"); set => Accessor.SetUInt32("legacy_host_loss", value); }


  public uint HostUnfilteredFrametime
  { get => Accessor.GetUInt32("host_unfiltered_frametime"); set => Accessor.SetUInt32("host_unfiltered_frametime", value); }


  public uint HltvReplayFlags
  { get => Accessor.GetUInt32("hltv_replay_flags"); set => Accessor.SetUInt32("hltv_replay_flags", value); }


  public uint ExpectedLongTick
  { get => Accessor.GetUInt32("expected_long_tick"); set => Accessor.SetUInt32("expected_long_tick", value); }


  public string ExpectedLongTickReason
  { get => Accessor.GetString("expected_long_tick_reason"); set => Accessor.SetString("expected_long_tick_reason", value); }


  public uint HostFrameDroppedPctX10
  { get => Accessor.GetUInt32("host_frame_dropped_pct_x10"); set => Accessor.SetUInt32("host_frame_dropped_pct_x10", value); }


  public uint HostFrameIrregularArrivalPctX10
  { get => Accessor.GetUInt32("host_frame_irregular_arrival_pct_x10"); set => Accessor.SetUInt32("host_frame_irregular_arrival_pct_x10", value); }

}
