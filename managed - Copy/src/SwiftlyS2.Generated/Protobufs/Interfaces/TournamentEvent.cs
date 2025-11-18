
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface TournamentEvent : ITypedProtobuf<TournamentEvent>
{
  static TournamentEvent ITypedProtobuf<TournamentEvent>.Wrap(nint handle, bool isManuallyAllocated) => new TournamentEventImpl(handle, isManuallyAllocated);


  public int EventId { get; set; }


  public string EventTag { get; set; }


  public string EventName { get; set; }


  public uint EventTimeStart { get; set; }


  public uint EventTimeEnd { get; set; }


  public int EventPublic { get; set; }


  public int EventStageId { get; set; }


  public string EventStageName { get; set; }


  public uint ActiveSectionId { get; set; }

}
