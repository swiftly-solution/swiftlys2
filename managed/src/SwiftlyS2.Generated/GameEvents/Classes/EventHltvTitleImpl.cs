using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "hltv_title"
/// </summary>
internal class EventHltvTitleImpl : GameEvent<EventHltvTitle>, EventHltvTitle
{

    public EventHltvTitleImpl( nint address ) : base(address)
    {
    }

    public string Text { get => Accessor.GetString("text"); set => Accessor.SetString("text", value); }
}
