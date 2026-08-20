namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCPhysForce
{
    public ICPhysForceForceOffHook ForceOff { get; }
    public ICPhysForceInitialThinkHook InitialThink { get; }
}