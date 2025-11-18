using SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when the movement services run command hook is triggered.
/// </summary>
public interface IOnMovementServicesRunCommandHookEvent
{
  /// <summary>
  /// The movement services.
  /// </summary>
  public CCSPlayer_MovementServices MovementServices { get; }
  /// <summary>
  /// The button state.
  /// </summary>
  public CInButtonState ButtonState { get;  }
  /// <summary>
  /// The user command protobuf.
  /// </summary>
  public CSGOUserCmdPB UserCmdPB { get; }
}