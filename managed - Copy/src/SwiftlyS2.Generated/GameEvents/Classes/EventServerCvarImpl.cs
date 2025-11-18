using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "server_cvar"
/// a server console var has changed
/// </summary>
internal class EventServerCvarImpl : GameEvent<EventServerCvar>, EventServerCvar
{

  public EventServerCvarImpl(nint address) : base(address)
  {
  }

  // cvar name, eg "mp_roundtime"
  public string CVarName
  { get => Accessor.GetString("cvarname"); set => Accessor.SetString("cvarname", value); }

  // new cvar value
  public string CVarValue
  { get => Accessor.GetString("cvarvalue"); set => Accessor.SetString("cvarvalue", value); }
}
