
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_HltvReplayImpl : TypedProtobuf<CSVCMsg_HltvReplay>, CSVCMsg_HltvReplay
{
  public CSVCMsg_HltvReplayImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Delay
  { get => Accessor.GetInt32("delay"); set => Accessor.SetInt32("delay", value); }


  public int PrimaryTarget
  { get => Accessor.GetInt32("primary_target"); set => Accessor.SetInt32("primary_target", value); }


  public int ReplayStopAt
  { get => Accessor.GetInt32("replay_stop_at"); set => Accessor.SetInt32("replay_stop_at", value); }


  public int ReplayStartAt
  { get => Accessor.GetInt32("replay_start_at"); set => Accessor.SetInt32("replay_start_at", value); }


  public int ReplaySlowdownBegin
  { get => Accessor.GetInt32("replay_slowdown_begin"); set => Accessor.SetInt32("replay_slowdown_begin", value); }


  public int ReplaySlowdownEnd
  { get => Accessor.GetInt32("replay_slowdown_end"); set => Accessor.SetInt32("replay_slowdown_end", value); }


  public float ReplaySlowdownRate
  { get => Accessor.GetFloat("replay_slowdown_rate"); set => Accessor.SetFloat("replay_slowdown_rate", value); }


  public int Reason
  { get => Accessor.GetInt32("reason"); set => Accessor.SetInt32("reason", value); }

}
