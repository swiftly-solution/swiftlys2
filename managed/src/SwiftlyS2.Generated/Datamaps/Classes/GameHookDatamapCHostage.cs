using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCHostage : IGameHookDatamapCHostage
{
    internal readonly CHostageHostageThinkHook CHostageHostageThinkHook = new();
    internal readonly CHostageHostageUseHook CHostageHostageUseHook = new();

    public ICHostageHostageThinkHook HostageThink => CHostageHostageThinkHook;
    public ICHostageHostageUseHook HostageUse => CHostageHostageUseHook;

    internal void UnregisterListeners()
    {
        CHostageHostageThinkHook.UnregisterListeners();
        CHostageHostageUseHook.UnregisterListeners();
    }
}