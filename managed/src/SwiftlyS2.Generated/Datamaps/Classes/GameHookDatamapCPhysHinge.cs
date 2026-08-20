using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPhysHinge : IGameHookDatamapCPhysHinge
{
    internal readonly CPhysHingeLimitThinkHook CPhysHingeLimitThinkHook = new();
    internal readonly CPhysHingeMoveThinkHook CPhysHingeMoveThinkHook = new();
    internal readonly CPhysHingeSoundThinkHook CPhysHingeSoundThinkHook = new();

    public ICPhysHingeLimitThinkHook LimitThink => CPhysHingeLimitThinkHook;
    public ICPhysHingeMoveThinkHook MoveThink => CPhysHingeMoveThinkHook;
    public ICPhysHingeSoundThinkHook SoundThink => CPhysHingeSoundThinkHook;

    internal void UnregisterListeners()
    {
        CPhysHingeLimitThinkHook.UnregisterListeners();
        CPhysHingeMoveThinkHook.UnregisterListeners();
        CPhysHingeSoundThinkHook.UnregisterListeners();
    }
}