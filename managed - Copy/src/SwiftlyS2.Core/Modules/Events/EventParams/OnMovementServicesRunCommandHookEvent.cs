using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Events;

internal class OnMovementServicesRunCommandHookEvent : IOnMovementServicesRunCommandHookEvent
{
  public required CCSPlayer_MovementServices MovementServices { get; set; }
  public required CInButtonState ButtonState { get; set; }
  public required CSGOUserCmdPB UserCmdPB { get; set; }

}