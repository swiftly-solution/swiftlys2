using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundOpvarSetPointEntity(GameHooksService owner) : IGameHookDatamapCSoundOpvarSetPointEntity
{
    internal readonly CSoundOpvarSetPointEntitySetOpvarThinkHook CSoundOpvarSetPointEntitySetOpvarThinkHook = new(owner);

    public ICSoundOpvarSetPointEntitySetOpvarThinkHook SetOpvarThink => CSoundOpvarSetPointEntitySetOpvarThinkHook;

    internal void UnregisterListeners()
    {
        CSoundOpvarSetPointEntitySetOpvarThinkHook.UnregisterListeners();
    }
}