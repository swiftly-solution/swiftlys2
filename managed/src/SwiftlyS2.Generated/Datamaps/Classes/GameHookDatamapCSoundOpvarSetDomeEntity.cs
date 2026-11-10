using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundOpvarSetDomeEntity(GameHooksService owner) : IGameHookDatamapCSoundOpvarSetDomeEntity
{
    internal readonly CSoundOpvarSetDomeEntitySetOpvarThinkHook CSoundOpvarSetDomeEntitySetOpvarThinkHook = new(owner);

    public ICSoundOpvarSetDomeEntitySetOpvarThinkHook SetOpvarThink => CSoundOpvarSetDomeEntitySetOpvarThinkHook;

    internal void UnregisterListeners()
    {
        CSoundOpvarSetDomeEntitySetOpvarThinkHook.UnregisterListeners();
    }
}