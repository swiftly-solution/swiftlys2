using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "achievement_event"
/// </summary>
internal class EventAchievementEventImpl : GameEvent<EventAchievementEvent>, EventAchievementEvent
{

  public EventAchievementEventImpl(nint address) : base(address)
  {
  }

  // non-localized name of achievement
  public string AchievementName
  { get => Accessor.GetString("achievement_name"); set => Accessor.SetString("achievement_name", value); }

  // # of steps toward achievement
  public short CurVal
  { get => (short)Accessor.GetInt32("cur_val"); set => Accessor.SetInt32("cur_val", value); }

  // total # of steps in achievement
  public short MaxVal
  { get => (short)Accessor.GetInt32("max_val"); set => Accessor.SetInt32("max_val", value); }
}
