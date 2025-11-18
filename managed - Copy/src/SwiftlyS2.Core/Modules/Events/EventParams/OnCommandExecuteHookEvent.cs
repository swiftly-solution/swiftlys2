using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Events;

internal class OnCommandExecuteHookEvent : IOnCommandExecuteHookEvent
{
  private CCommand _command;

  public ref CCommand Command => ref _command;

  public HookMode HookMode { get; init; }

  public OnCommandExecuteHookEvent(ref CCommand command, HookMode hookMode)
  {
    _command = command;
    HookMode = hookMode;
  }
}