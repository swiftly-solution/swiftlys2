using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "mb_input_lock_cancel"
/// </summary>
public interface EventMbInputLockCancel : IGameEvent<EventMbInputLockCancel> {

  static EventMbInputLockCancel IGameEvent<EventMbInputLockCancel>.Create(nint address) => new EventMbInputLockCancelImpl(address);

  static string IGameEvent<EventMbInputLockCancel>.GetName() => "mb_input_lock_cancel";

  static uint IGameEvent<EventMbInputLockCancel>.GetHash() => 0x79A46A94u;
}
