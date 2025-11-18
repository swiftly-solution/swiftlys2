using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when a command is executed.
/// </summary>
public interface IOnCommandExecuteHookEvent
{
  /// <summary>
  /// The command.
  /// </summary>
  public ref CCommand Command { get; }

  /// <summary>
  /// The hook mode.
  /// </summary>
  public HookMode HookMode { get; }
}