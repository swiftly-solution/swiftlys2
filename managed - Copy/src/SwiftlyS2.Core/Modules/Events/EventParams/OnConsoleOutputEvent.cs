using SwiftlyS2.Shared.Events;

namespace SwiftlyS2.Core.Events;

internal class OnConsoleOutputEvent : IOnConsoleOutputEvent
{

  public required string Message { get; set; }
}