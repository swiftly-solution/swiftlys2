
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class TournamentEventImpl : TypedProtobuf<TournamentEvent>, TournamentEvent
{
  public TournamentEventImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int EventId
  { get => Accessor.GetInt32("event_id"); set => Accessor.SetInt32("event_id", value); }


  public string EventTag
  { get => Accessor.GetString("event_tag"); set => Accessor.SetString("event_tag", value); }


  public string EventName
  { get => Accessor.GetString("event_name"); set => Accessor.SetString("event_name", value); }


  public uint EventTimeStart
  { get => Accessor.GetUInt32("event_time_start"); set => Accessor.SetUInt32("event_time_start", value); }


  public uint EventTimeEnd
  { get => Accessor.GetUInt32("event_time_end"); set => Accessor.SetUInt32("event_time_end", value); }


  public int EventPublic
  { get => Accessor.GetInt32("event_public"); set => Accessor.SetInt32("event_public", value); }


  public int EventStageId
  { get => Accessor.GetInt32("event_stage_id"); set => Accessor.SetInt32("event_stage_id", value); }


  public string EventStageName
  { get => Accessor.GetString("event_stage_name"); set => Accessor.SetString("event_stage_name", value); }


  public uint ActiveSectionId
  { get => Accessor.GetUInt32("active_section_id"); set => Accessor.SetUInt32("active_section_id", value); }

}
