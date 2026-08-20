using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerSndSosOpvar : IGameHookDatamapCTriggerSndSosOpvar
{
    internal readonly CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHook CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHook = new();

    public ICTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHook SndSosTriggerOpvarWaitOver => CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHook;

    internal void UnregisterListeners()
    {
        CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHook.UnregisterListeners();
    }
}