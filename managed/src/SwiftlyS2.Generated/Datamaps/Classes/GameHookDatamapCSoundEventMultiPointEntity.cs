using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundEventMultiPointEntity(GameHooksService owner) : IGameHookDatamapCSoundEventMultiPointEntity
{
    internal readonly CSoundEventMultiPointEntityMultiPointThinkHook CSoundEventMultiPointEntityMultiPointThinkHook = new(owner);

    public ICSoundEventMultiPointEntityMultiPointThinkHook MultiPointThink => CSoundEventMultiPointEntityMultiPointThinkHook;

    internal void UnregisterListeners()
    {
        CSoundEventMultiPointEntityMultiPointThinkHook.UnregisterListeners();
    }
}