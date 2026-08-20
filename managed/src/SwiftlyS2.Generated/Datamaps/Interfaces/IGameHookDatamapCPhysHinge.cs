namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCPhysHinge
{
    public ICPhysHingeLimitThinkHook LimitThink { get; }
    public ICPhysHingeMoveThinkHook MoveThink { get; }
    public ICPhysHingeSoundThinkHook SoundThink { get; }
}