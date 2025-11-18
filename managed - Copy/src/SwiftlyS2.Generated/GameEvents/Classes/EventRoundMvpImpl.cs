using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "round_mvp"
/// </summary>
internal class EventRoundMvpImpl : GameEvent<EventRoundMvp>, EventRoundMvp
{

  public EventRoundMvpImpl(nint address) : base(address)
  {
  }

  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  public short Reason
  { get => (short)Accessor.GetInt32("reason"); set => Accessor.SetInt32("reason", value); }

  public int Value
  { get => Accessor.GetInt32("value"); set => Accessor.SetInt32("value", value); }

  public int MusickItMvps
  { get => Accessor.GetInt32("musickitmvps"); set => Accessor.SetInt32("musickitmvps", value); }

  public byte NoMusic
  { get => (byte)Accessor.GetInt32("nomusic"); set => Accessor.SetInt32("nomusic", value); }

  public int MusickItID
  { get => Accessor.GetInt32("musickitid"); set => Accessor.SetInt32("musickitid", value); }
}
