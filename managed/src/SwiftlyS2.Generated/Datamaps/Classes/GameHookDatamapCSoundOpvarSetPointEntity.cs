using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundOpvarSetPointEntity : IGameHookDatamapCSoundOpvarSetPointEntity
{
    internal readonly CSoundOpvarSetPointEntitySetOpvarThinkHook CSoundOpvarSetPointEntitySetOpvarThinkHook = new();

    public ICSoundOpvarSetPointEntitySetOpvarThinkHook SetOpvarThink => CSoundOpvarSetPointEntitySetOpvarThinkHook;

    internal void UnregisterListeners()
    {
        CSoundOpvarSetPointEntitySetOpvarThinkHook.UnregisterListeners();
    }
}