
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class DeepPlayerMatchEventImpl : TypedProtobuf<DeepPlayerMatchEvent>, DeepPlayerMatchEvent
{
  public DeepPlayerMatchEventImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Accountid
  { get => Accessor.GetUInt32("accountid"); set => Accessor.SetUInt32("accountid", value); }


  public ulong MatchId
  { get => Accessor.GetUInt64("match_id"); set => Accessor.SetUInt64("match_id", value); }


  public uint EventId
  { get => Accessor.GetUInt32("event_id"); set => Accessor.SetUInt32("event_id", value); }


  public uint EventType
  { get => Accessor.GetUInt32("event_type"); set => Accessor.SetUInt32("event_type", value); }


  public bool BPlayingCt
  { get => Accessor.GetBool("b_playing_ct"); set => Accessor.SetBool("b_playing_ct", value); }


  public int UserPosX
  { get => Accessor.GetInt32("user_pos_x"); set => Accessor.SetInt32("user_pos_x", value); }


  public int UserPosY
  { get => Accessor.GetInt32("user_pos_y"); set => Accessor.SetInt32("user_pos_y", value); }


  public int UserPosZ
  { get => Accessor.GetInt32("user_pos_z"); set => Accessor.SetInt32("user_pos_z", value); }


  public uint UserDefidx
  { get => Accessor.GetUInt32("user_defidx"); set => Accessor.SetUInt32("user_defidx", value); }


  public int OtherPosX
  { get => Accessor.GetInt32("other_pos_x"); set => Accessor.SetInt32("other_pos_x", value); }


  public int OtherPosY
  { get => Accessor.GetInt32("other_pos_y"); set => Accessor.SetInt32("other_pos_y", value); }


  public int OtherPosZ
  { get => Accessor.GetInt32("other_pos_z"); set => Accessor.SetInt32("other_pos_z", value); }


  public uint OtherDefidx
  { get => Accessor.GetUInt32("other_defidx"); set => Accessor.SetUInt32("other_defidx", value); }


  public int EventData
  { get => Accessor.GetInt32("event_data"); set => Accessor.SetInt32("event_data", value); }

}
