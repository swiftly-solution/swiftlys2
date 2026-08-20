using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPlantedC4 : IGameHookDatamapCPlantedC4
{
    internal readonly CPlantedC4C4ThinkHook CPlantedC4C4ThinkHook = new();

    public ICPlantedC4C4ThinkHook C4Think => CPlantedC4C4ThinkHook;

    internal void UnregisterListeners()
    {
        CPlantedC4C4ThinkHook.UnregisterListeners();
    }
}