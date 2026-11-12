using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerSave(GameHooksService owner) : IGameHookDatamapCTriggerSave
{
    internal readonly CTriggerSaveRetriggerWaitOverHook CTriggerSaveRetriggerWaitOverHook = new(owner);

    public ICTriggerSaveRetriggerWaitOverHook RetriggerWaitOver => CTriggerSaveRetriggerWaitOverHook;

    internal void UnregisterListeners()
    {
        CTriggerSaveRetriggerWaitOverHook.UnregisterListeners();
    }
}