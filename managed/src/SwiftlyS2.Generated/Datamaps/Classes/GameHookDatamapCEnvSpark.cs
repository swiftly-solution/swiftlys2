using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCEnvSpark(GameHooksService owner) : IGameHookDatamapCEnvSpark
{
    internal readonly CEnvSparkSparkThinkHook CEnvSparkSparkThinkHook = new(owner);

    public ICEnvSparkSparkThinkHook SparkThink => CEnvSparkSparkThinkHook;

    internal void UnregisterListeners()
    {
        CEnvSparkSparkThinkHook.UnregisterListeners();
    }
}