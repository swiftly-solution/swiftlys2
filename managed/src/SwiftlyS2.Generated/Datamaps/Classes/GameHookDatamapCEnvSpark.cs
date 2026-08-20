using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCEnvSpark : IGameHookDatamapCEnvSpark
{
    internal readonly CEnvSparkSparkThinkHook CEnvSparkSparkThinkHook = new();

    public ICEnvSparkSparkThinkHook SparkThink => CEnvSparkSparkThinkHook;

    internal void UnregisterListeners()
    {
        CEnvSparkSparkThinkHook.UnregisterListeners();
    }
}