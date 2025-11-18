
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CP2P_WatchSynchronization : ITypedProtobuf<CP2P_WatchSynchronization>
{
  static CP2P_WatchSynchronization ITypedProtobuf<CP2P_WatchSynchronization>.Wrap(nint handle, bool isManuallyAllocated) => new CP2P_WatchSynchronizationImpl(handle, isManuallyAllocated);


  public int DemoTick { get; set; }


  public bool Paused { get; set; }


  public ulong TvListenVoiceIndices { get; set; }


  public int DotaSpectatorMode { get; set; }


  public bool DotaSpectatorWatchingBroadcaster { get; set; }


  public int DotaSpectatorHeroIndex { get; set; }


  public int DotaSpectatorAutospeed { get; set; }


  public int DotaReplaySpeed { get; set; }

}
