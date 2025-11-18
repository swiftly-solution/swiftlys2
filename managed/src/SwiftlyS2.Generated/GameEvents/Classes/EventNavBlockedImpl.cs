using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "nav_blocked"
/// </summary>
internal class EventNavBlockedImpl : GameEvent<EventNavBlocked>, EventNavBlocked
{

    public EventNavBlockedImpl( nint address ) : base(address)
    {
    }

    public int Area { get => Accessor.GetInt32("area"); set => Accessor.SetInt32("area", value); }

    public bool Blocked { get => Accessor.GetBool("blocked"); set => Accessor.SetBool("blocked", value); }
}
