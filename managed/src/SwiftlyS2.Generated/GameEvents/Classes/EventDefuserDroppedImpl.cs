using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "defuser_dropped"
/// </summary>
internal class EventDefuserDroppedImpl : GameEvent<EventDefuserDropped>, EventDefuserDropped
{

    public EventDefuserDroppedImpl( nint address ) : base(address)
    {
    }

    // defuser's entity ID
    public int EntityID { get => Accessor.GetInt32("entityid"); set => Accessor.SetInt32("entityid", value); }
}
