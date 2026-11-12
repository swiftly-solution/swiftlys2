using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundOpvarSetBoxEntity(GameHooksService owner) : IGameHookDatamapCSoundOpvarSetBoxEntity
{
    internal readonly CSoundOpvarSetBoxEntitySetOpvarThinkHook CSoundOpvarSetBoxEntitySetOpvarThinkHook = new(owner);

    public ICSoundOpvarSetBoxEntitySetOpvarThinkHook SetOpvarThink => CSoundOpvarSetBoxEntitySetOpvarThinkHook;

    internal void UnregisterListeners()
    {
        CSoundOpvarSetBoxEntitySetOpvarThinkHook.UnregisterListeners();
    }
}