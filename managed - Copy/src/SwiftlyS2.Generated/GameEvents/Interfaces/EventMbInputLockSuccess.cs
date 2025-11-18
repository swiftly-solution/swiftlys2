using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "mb_input_lock_success"
/// </summary>
public interface EventMbInputLockSuccess : IGameEvent<EventMbInputLockSuccess> {

  static EventMbInputLockSuccess IGameEvent<EventMbInputLockSuccess>.Create(nint address) => new EventMbInputLockSuccessImpl(address);

  static string IGameEvent<EventMbInputLockSuccess>.GetName() => "mb_input_lock_success";

  static uint IGameEvent<EventMbInputLockSuccess>.GetHash() => 0xEB082439u;
}
