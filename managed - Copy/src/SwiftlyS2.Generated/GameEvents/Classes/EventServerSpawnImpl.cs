using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "server_spawn"
/// send once a server starts
/// </summary>
internal class EventServerSpawnImpl : GameEvent<EventServerSpawn>, EventServerSpawn
{

  public EventServerSpawnImpl(nint address) : base(address)
  {
  }

  // public host name
  public string Hostname
  { get => Accessor.GetString("hostname"); set => Accessor.SetString("hostname", value); }

  // hostame, IP or DNS name
  public string Address
  { get => Accessor.GetString("address"); set => Accessor.SetString("address", value); }

  // server port
  public short Port
  { get => (short)Accessor.GetInt32("port"); set => Accessor.SetInt32("port", value); }

  // game dir
  public string Game
  { get => Accessor.GetString("game"); set => Accessor.SetString("game", value); }

  // map name
  public string MapName
  { get => Accessor.GetString("mapname"); set => Accessor.SetString("mapname", value); }

  // addon name
  public string AddonName
  { get => Accessor.GetString("addonname"); set => Accessor.SetString("addonname", value); }

  // max players
  public int MaxPlayers
  { get => Accessor.GetInt32("maxplayers"); set => Accessor.SetInt32("maxplayers", value); }

  // WIN32, LINUX
  public string Os
  { get => Accessor.GetString("os"); set => Accessor.SetString("os", value); }

  // true if dedicated server
  public bool Dedicated
  { get => Accessor.GetBool("dedicated"); set => Accessor.SetBool("dedicated", value); }

  // true if password protected
  public bool Password
  { get => Accessor.GetBool("password"); set => Accessor.SetBool("password", value); }
}
