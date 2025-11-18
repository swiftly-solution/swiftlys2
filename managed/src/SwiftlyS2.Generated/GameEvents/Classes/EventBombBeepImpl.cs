using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "bomb_beep"
/// </summary>
internal class EventBombBeepImpl : GameEvent<EventBombBeep>, EventBombBeep
{

    public EventBombBeepImpl( nint address ) : base(address)
    {
    }

    // c4 entity
    public int EntIndex { get => Accessor.GetInt32("entindex"); set => Accessor.SetInt32("entindex", value); }
}
