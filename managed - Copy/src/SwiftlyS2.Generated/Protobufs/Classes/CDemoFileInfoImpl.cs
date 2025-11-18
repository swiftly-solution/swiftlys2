
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDemoFileInfoImpl : TypedProtobuf<CDemoFileInfo>, CDemoFileInfo
{
  public CDemoFileInfoImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public float PlaybackTime
  { get => Accessor.GetFloat("playback_time"); set => Accessor.SetFloat("playback_time", value); }


  public int PlaybackTicks
  { get => Accessor.GetInt32("playback_ticks"); set => Accessor.SetInt32("playback_ticks", value); }


  public int PlaybackFrames
  { get => Accessor.GetInt32("playback_frames"); set => Accessor.SetInt32("playback_frames", value); }


  public CGameInfo GameInfo
  { get => new CGameInfoImpl(NativeNetMessages.GetNestedMessage(Address, "game_info"), false); }

}
