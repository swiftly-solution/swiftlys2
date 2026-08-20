using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundOpvarSetPointBase : IGameHookDatamapCSoundOpvarSetPointBase
{
    internal readonly CSoundOpvarSetPointBaseSetOpvarThinkHook CSoundOpvarSetPointBaseSetOpvarThinkHook = new();

    public ICSoundOpvarSetPointBaseSetOpvarThinkHook SetOpvarThink => CSoundOpvarSetPointBaseSetOpvarThinkHook;

    internal void UnregisterListeners()
    {
        CSoundOpvarSetPointBaseSetOpvarThinkHook.UnregisterListeners();
    }
}