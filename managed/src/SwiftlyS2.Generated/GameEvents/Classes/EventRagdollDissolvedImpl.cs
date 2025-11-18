using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "ragdoll_dissolved"
/// </summary>
internal class EventRagdollDissolvedImpl : GameEvent<EventRagdollDissolved>, EventRagdollDissolved
{

    public EventRagdollDissolvedImpl( nint address ) : base(address)
    {
    }

    public int EntIndex { get => Accessor.GetInt32("entindex"); set => Accessor.SetInt32("entindex", value); }
}
