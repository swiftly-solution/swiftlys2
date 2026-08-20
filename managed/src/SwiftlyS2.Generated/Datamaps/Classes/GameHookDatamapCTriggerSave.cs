using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerSave : IGameHookDatamapCTriggerSave
{
    internal readonly CTriggerSaveRetriggerWaitOverHook CTriggerSaveRetriggerWaitOverHook = new();

    public ICTriggerSaveRetriggerWaitOverHook RetriggerWaitOver => CTriggerSaveRetriggerWaitOverHook;

    internal void UnregisterListeners()
    {
        CTriggerSaveRetriggerWaitOverHook.UnregisterListeners();
    }
}