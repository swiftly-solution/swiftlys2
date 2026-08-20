using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundOpvarSetBoxEntity : IGameHookDatamapCSoundOpvarSetBoxEntity
{
    internal readonly CSoundOpvarSetBoxEntitySetOpvarThinkHook CSoundOpvarSetBoxEntitySetOpvarThinkHook = new();

    public ICSoundOpvarSetBoxEntitySetOpvarThinkHook SetOpvarThink => CSoundOpvarSetBoxEntitySetOpvarThinkHook;

    internal void UnregisterListeners()
    {
        CSoundOpvarSetBoxEntitySetOpvarThinkHook.UnregisterListeners();
    }
}