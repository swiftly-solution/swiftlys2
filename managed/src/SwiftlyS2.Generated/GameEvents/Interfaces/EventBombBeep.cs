using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "bomb_beep"
/// </summary>
public interface EventBombBeep : IGameEvent<EventBombBeep>
{

    static EventBombBeep IGameEvent<EventBombBeep>.Create( nint address ) => new EventBombBeepImpl(address);

    static string IGameEvent<EventBombBeep>.GetName() => "bomb_beep";

    static uint IGameEvent<EventBombBeep>.GetHash() => 0x056A0D22u;
    /// <summary>
    /// c4 entity
    /// <br/>
    /// type: long
    /// </summary>
    public int EntIndex { get; set; }

}
