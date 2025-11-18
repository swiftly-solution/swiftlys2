using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "mb_input_lock_success"
/// </summary>
internal class EventMbInputLockSuccessImpl : GameEvent<EventMbInputLockSuccess>, EventMbInputLockSuccess
{

  public EventMbInputLockSuccessImpl(nint address) : base(address)
  {
  }
}
