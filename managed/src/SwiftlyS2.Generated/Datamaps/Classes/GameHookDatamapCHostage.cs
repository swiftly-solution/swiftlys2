using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCHostage(GameHooksService owner) : IGameHookDatamapCHostage
{
    internal readonly CHostageHostageThinkHook CHostageHostageThinkHook = new(owner);
    internal readonly CHostageHostageUseHook CHostageHostageUseHook = new(owner);

    public ICHostageHostageThinkHook HostageThink => CHostageHostageThinkHook;
    public ICHostageHostageUseHook HostageUse => CHostageHostageUseHook;

    internal void UnregisterListeners()
    {
        CHostageHostageThinkHook.UnregisterListeners();
        CHostageHostageUseHook.UnregisterListeners();
    }
}