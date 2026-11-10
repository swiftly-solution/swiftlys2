using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCTriggerSndSosOpvar(GameHooksService owner) : IGameHookDatamapCTriggerSndSosOpvar
{
    internal readonly CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHook CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHook = new(owner);

    public ICTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHook SndSosTriggerOpvarWaitOver => CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHook;

    internal void UnregisterListeners()
    {
        CTriggerSndSosOpvarSndSosTriggerOpvarWaitOverHook.UnregisterListeners();
    }
}