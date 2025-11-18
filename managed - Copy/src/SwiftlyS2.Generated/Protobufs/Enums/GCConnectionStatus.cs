
namespace SwiftlyS2.Shared.ProtobufDefinitions;

public enum GCConnectionStatus
{
  GCConnectionStatus_HAVE_SESSION = 0,
  GCConnectionStatus_GC_GOING_DOWN = 1,
  GCConnectionStatus_NO_SESSION = 2,
  GCConnectionStatus_NO_SESSION_IN_LOGON_QUEUE = 3,
  GCConnectionStatus_NO_STEAM = 4,
}
