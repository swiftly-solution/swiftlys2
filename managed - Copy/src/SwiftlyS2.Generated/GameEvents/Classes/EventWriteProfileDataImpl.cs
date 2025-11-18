using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "write_profile_data"
/// </summary>
internal class EventWriteProfileDataImpl : GameEvent<EventWriteProfileData>, EventWriteProfileData
{

  public EventWriteProfileDataImpl(nint address) : base(address)
  {
  }
}
