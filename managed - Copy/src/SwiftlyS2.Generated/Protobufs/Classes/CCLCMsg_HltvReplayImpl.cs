
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCLCMsg_HltvReplayImpl : NetMessage<CCLCMsg_HltvReplay>, CCLCMsg_HltvReplay
{
  public CCLCMsg_HltvReplayImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Request
  { get => Accessor.GetInt32("request"); set => Accessor.SetInt32("request", value); }


  public float SlowdownLength
  { get => Accessor.GetFloat("slowdown_length"); set => Accessor.SetFloat("slowdown_length", value); }


  public float SlowdownRate
  { get => Accessor.GetFloat("slowdown_rate"); set => Accessor.SetFloat("slowdown_rate", value); }


  public int PrimaryTarget
  { get => Accessor.GetInt32("primary_target"); set => Accessor.SetInt32("primary_target", value); }


  public float EventTime
  { get => Accessor.GetFloat("event_time"); set => Accessor.SetFloat("event_time", value); }

}
