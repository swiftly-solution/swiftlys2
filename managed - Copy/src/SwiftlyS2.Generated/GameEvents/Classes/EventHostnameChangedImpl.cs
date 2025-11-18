using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "hostname_changed"
/// </summary>
internal class EventHostnameChangedImpl : GameEvent<EventHostnameChanged>, EventHostnameChanged
{

  public EventHostnameChangedImpl(nint address) : base(address)
  {
  }

  public string Hostname
  { get => Accessor.GetString("hostname"); set => Accessor.SetString("hostname", value); }
}
