namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCPhysicsProp
{
    public ICPhysicsPropClearFlagsThinkHook ClearFlagsThink { get; }
    public ICPhysicsPropClearThrownByPlayerThinkHook ClearThrownByPlayerThink { get; }
}