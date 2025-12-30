using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Events;

internal class OnCommandExecuteHookEvent : IOnCommandExecuteHookEvent
{
    private CCommand command;

    public ref CCommand Command => ref command;

    public HookMode HookMode { get; init; }
    public HookResult Result { get; set; } = HookResult.Continue;

    public OnCommandExecuteHookEvent( ref CCommand command, HookMode hookMode )
    {
        this.command = command;
        this.HookMode = hookMode;
    }
}