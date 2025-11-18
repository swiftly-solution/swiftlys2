using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "difficulty_changed"
/// </summary>
public interface EventDifficultyChanged : IGameEvent<EventDifficultyChanged>
{

    static EventDifficultyChanged IGameEvent<EventDifficultyChanged>.Create( nint address ) => new EventDifficultyChangedImpl(address);

    static string IGameEvent<EventDifficultyChanged>.GetName() => "difficulty_changed";

    static uint IGameEvent<EventDifficultyChanged>.GetHash() => 0xB261D803u;
    /// <summary>
    /// type: short
    /// </summary>
    public short NewDifficulty { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short OldDifficulty { get; set; }

    /// <summary>
    /// new difficulty as string
    /// <br/>
    /// type: string
    /// </summary>
    public string StrDifficulty { get; set; }

}
