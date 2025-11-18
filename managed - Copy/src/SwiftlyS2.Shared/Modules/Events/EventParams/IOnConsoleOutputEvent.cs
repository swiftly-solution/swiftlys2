namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when a console output is received.
/// </summary>
public interface IOnConsoleOutputEvent {

  /// <summary>
  /// The message of the console output.
  /// </summary>
  public string Message { get; }
}