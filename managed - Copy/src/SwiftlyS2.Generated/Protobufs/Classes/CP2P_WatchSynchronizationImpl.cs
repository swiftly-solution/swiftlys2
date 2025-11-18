
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CP2P_WatchSynchronizationImpl : TypedProtobuf<CP2P_WatchSynchronization>, CP2P_WatchSynchronization
{
  public CP2P_WatchSynchronizationImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int DemoTick
  { get => Accessor.GetInt32("demo_tick"); set => Accessor.SetInt32("demo_tick", value); }


  public bool Paused
  { get => Accessor.GetBool("paused"); set => Accessor.SetBool("paused", value); }


  public ulong TvListenVoiceIndices
  { get => Accessor.GetUInt64("tv_listen_voice_indices"); set => Accessor.SetUInt64("tv_listen_voice_indices", value); }


  public int DotaSpectatorMode
  { get => Accessor.GetInt32("dota_spectator_mode"); set => Accessor.SetInt32("dota_spectator_mode", value); }


  public bool DotaSpectatorWatchingBroadcaster
  { get => Accessor.GetBool("dota_spectator_watching_broadcaster"); set => Accessor.SetBool("dota_spectator_watching_broadcaster", value); }


  public int DotaSpectatorHeroIndex
  { get => Accessor.GetInt32("dota_spectator_hero_index"); set => Accessor.SetInt32("dota_spectator_hero_index", value); }


  public int DotaSpectatorAutospeed
  { get => Accessor.GetInt32("dota_spectator_autospeed"); set => Accessor.SetInt32("dota_spectator_autospeed", value); }


  public int DotaReplaySpeed
  { get => Accessor.GetInt32("dota_replay_speed"); set => Accessor.SetInt32("dota_replay_speed", value); }

}
