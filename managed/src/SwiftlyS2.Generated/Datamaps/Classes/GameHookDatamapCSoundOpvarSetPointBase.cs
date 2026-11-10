using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundOpvarSetPointBase(GameHooksService owner) : IGameHookDatamapCSoundOpvarSetPointBase
{
    internal readonly CSoundOpvarSetPointBaseSetOpvarThinkHook CSoundOpvarSetPointBaseSetOpvarThinkHook = new(owner);

    public ICSoundOpvarSetPointBaseSetOpvarThinkHook SetOpvarThink => CSoundOpvarSetPointBaseSetOpvarThinkHook;

    internal void UnregisterListeners()
    {
        CSoundOpvarSetPointBaseSetOpvarThinkHook.UnregisterListeners();
    }
}