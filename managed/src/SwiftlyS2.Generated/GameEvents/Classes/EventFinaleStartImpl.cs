using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "finale_start"
/// </summary>
internal class EventFinaleStartImpl : GameEvent<EventFinaleStart>, EventFinaleStart
{

    public EventFinaleStartImpl( nint address ) : base(address)
    {
    }

    public short Rushes { get => (short)Accessor.GetInt32("rushes"); set => Accessor.SetInt32("rushes", value); }
}
