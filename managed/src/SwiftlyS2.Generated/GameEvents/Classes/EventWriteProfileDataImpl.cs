using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "write_profile_data"
/// </summary>
internal class EventWriteProfileDataImpl : GameEvent<EventWriteProfileData>, EventWriteProfileData
{

    public EventWriteProfileDataImpl( nint address ) : base(address)
    {
    }
}
