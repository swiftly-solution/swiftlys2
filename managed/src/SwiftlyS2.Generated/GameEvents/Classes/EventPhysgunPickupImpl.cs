using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "physgun_pickup"
/// </summary>
internal class EventPhysgunPickupImpl : GameEvent<EventPhysgunPickup>, EventPhysgunPickup
{

    public EventPhysgunPickupImpl( nint address ) : base(address)
    {
    }

    // entity picked up
    public nint Target { get => Accessor.GetPtr("target"); set => Accessor.SetPtr("target", value); }
}
