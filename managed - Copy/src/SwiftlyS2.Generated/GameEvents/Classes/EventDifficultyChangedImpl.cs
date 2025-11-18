using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "difficulty_changed"
/// </summary>
internal class EventDifficultyChangedImpl : GameEvent<EventDifficultyChanged>, EventDifficultyChanged
{

  public EventDifficultyChangedImpl(nint address) : base(address)
  {
  }

  public short NewDifficulty
  { get => (short)Accessor.GetInt32("newDifficulty"); set => Accessor.SetInt32("newDifficulty", value); }

  public short OldDifficulty
  { get => (short)Accessor.GetInt32("oldDifficulty"); set => Accessor.SetInt32("oldDifficulty", value); }

  // new difficulty as string
  public string StrDifficulty
  { get => Accessor.GetString("strDifficulty"); set => Accessor.SetString("strDifficulty", value); }
}
