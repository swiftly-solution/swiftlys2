using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "vote_options"
/// </summary>
internal class EventVoteOptionsImpl : GameEvent<EventVoteOptions>, EventVoteOptions
{

  public EventVoteOptionsImpl(nint address) : base(address)
  {
  }

  // Number of options - up to MAX_VOTE_OPTIONS
  public byte Count
  { get => (byte)Accessor.GetInt32("count"); set => Accessor.SetInt32("count", value); }

  public string Option1
  { get => Accessor.GetString("option1"); set => Accessor.SetString("option1", value); }

  public string Option2
  { get => Accessor.GetString("option2"); set => Accessor.SetString("option2", value); }

  public string Option3
  { get => Accessor.GetString("option3"); set => Accessor.SetString("option3", value); }

  public string Option4
  { get => Accessor.GetString("option4"); set => Accessor.SetString("option4", value); }

  public string Option5
  { get => Accessor.GetString("option5"); set => Accessor.SetString("option5", value); }
}
