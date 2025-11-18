using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "round_end"
/// </summary>
internal class EventRoundEndImpl : GameEvent<EventRoundEnd>, EventRoundEnd
{

  public EventRoundEndImpl(nint address) : base(address)
  {
  }

  // winner team/user i
  public byte Winner
  { get => (byte)Accessor.GetInt32("winner"); set => Accessor.SetInt32("winner", value); }

  // reson why team won
  public byte Reason
  { get => (byte)Accessor.GetInt32("reason"); set => Accessor.SetInt32("reason", value); }

  // end round message
  public string Message
  { get => Accessor.GetString("message"); set => Accessor.SetString("message", value); }

  public float Time
  { get => Accessor.GetFloat("time"); set => Accessor.SetFloat("time", value); }

  // server-generated legacy value
  public byte Legacy
  { get => (byte)Accessor.GetInt32("legacy"); set => Accessor.SetInt32("legacy", value); }

  // total number of players alive at the end of round, used for statistics gathering, computed on the server in the event client is in replay when receiving this message
  public short PlayerCount
  { get => (short)Accessor.GetInt32("player_count"); set => Accessor.SetInt32("player_count", value); }

  // if set, don't play round end music, because action is still on-going
  public byte NoMusic
  { get => (byte)Accessor.GetInt32("nomusic"); set => Accessor.SetInt32("nomusic", value); }
}
