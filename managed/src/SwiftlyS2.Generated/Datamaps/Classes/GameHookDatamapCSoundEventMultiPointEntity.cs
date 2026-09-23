using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundEventMultiPointEntity : IGameHookDatamapCSoundEventMultiPointEntity
{
    internal readonly CSoundEventMultiPointEntityMultiPointThinkHook CSoundEventMultiPointEntityMultiPointThinkHook = new();

    public ICSoundEventMultiPointEntityMultiPointThinkHook MultiPointThink => CSoundEventMultiPointEntityMultiPointThinkHook;

    internal void UnregisterListeners()
    {
        CSoundEventMultiPointEntityMultiPointThinkHook.UnregisterListeners();
    }
}