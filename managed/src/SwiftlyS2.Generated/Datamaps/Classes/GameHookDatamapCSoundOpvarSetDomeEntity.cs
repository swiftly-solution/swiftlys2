using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCSoundOpvarSetDomeEntity : IGameHookDatamapCSoundOpvarSetDomeEntity
{
    internal readonly CSoundOpvarSetDomeEntitySetOpvarThinkHook CSoundOpvarSetDomeEntitySetOpvarThinkHook = new();

    public ICSoundOpvarSetDomeEntitySetOpvarThinkHook SetOpvarThink => CSoundOpvarSetDomeEntitySetOpvarThinkHook;

    internal void UnregisterListeners()
    {
        CSoundOpvarSetDomeEntitySetOpvarThinkHook.UnregisterListeners();
    }
}