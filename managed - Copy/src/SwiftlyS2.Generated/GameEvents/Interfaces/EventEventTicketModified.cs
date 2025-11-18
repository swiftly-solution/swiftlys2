using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "event_ticket_modified"
/// </summary>
public interface EventEventTicketModified : IGameEvent<EventEventTicketModified> {

  static EventEventTicketModified IGameEvent<EventEventTicketModified>.Create(nint address) => new EventEventTicketModifiedImpl(address);

  static string IGameEvent<EventEventTicketModified>.GetName() => "event_ticket_modified";

  static uint IGameEvent<EventEventTicketModified>.GetHash() => 0xAD893DFEu;
}
