using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPhysHinge(GameHooksService owner) : IGameHookDatamapCPhysHinge
{
    internal readonly CPhysHingeLimitThinkHook CPhysHingeLimitThinkHook = new(owner);
    internal readonly CPhysHingeMoveThinkHook CPhysHingeMoveThinkHook = new(owner);
    internal readonly CPhysHingeSoundThinkHook CPhysHingeSoundThinkHook = new(owner);

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