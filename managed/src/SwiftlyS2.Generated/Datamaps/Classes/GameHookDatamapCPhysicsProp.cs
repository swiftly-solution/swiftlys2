using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPhysicsProp : IGameHookDatamapCPhysicsProp
{
    internal readonly CPhysicsPropClearFlagsThinkHook CPhysicsPropClearFlagsThinkHook = new();
    internal readonly CPhysicsPropClearThrownByPlayerThinkHook CPhysicsPropClearThrownByPlayerThinkHook = new();

    public ICPhysicsPropClearFlagsThinkHook ClearFlagsThink => CPhysicsPropClearFlagsThinkHook;
    public ICPhysicsPropClearThrownByPlayerThinkHook ClearThrownByPlayerThink => CPhysicsPropClearThrownByPlayerThinkHook;

    internal void UnregisterListeners()
    {
        CPhysicsPropClearFlagsThinkHook.UnregisterListeners();
        CPhysicsPropClearThrownByPlayerThinkHook.UnregisterListeners();
    }
}