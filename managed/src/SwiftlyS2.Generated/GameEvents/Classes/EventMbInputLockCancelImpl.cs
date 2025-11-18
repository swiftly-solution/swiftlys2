using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "mb_input_lock_cancel"
/// </summary>
internal class EventMbInputLockCancelImpl : GameEvent<EventMbInputLockCancel>, EventMbInputLockCancel
{

    public EventMbInputLockCancelImpl( nint address ) : base(address)
    {
    }
}
