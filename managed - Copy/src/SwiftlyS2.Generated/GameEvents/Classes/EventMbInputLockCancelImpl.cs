using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "mb_input_lock_cancel"
/// </summary>
internal class EventMbInputLockCancelImpl : GameEvent<EventMbInputLockCancel>, EventMbInputLockCancel
{

  public EventMbInputLockCancelImpl(nint address) : base(address)
  {
  }
}
