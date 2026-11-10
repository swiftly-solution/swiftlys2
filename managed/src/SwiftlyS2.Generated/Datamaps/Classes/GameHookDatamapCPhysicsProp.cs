using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPhysicsProp(GameHooksService owner) : IGameHookDatamapCPhysicsProp
{
    internal readonly CPhysicsPropClearFlagsThinkHook CPhysicsPropClearFlagsThinkHook = new(owner);
    internal readonly CPhysicsPropClearThrownByPlayerThinkHook CPhysicsPropClearThrownByPlayerThinkHook = new(owner);

    public ICPhysicsPropClearFlagsThinkHook ClearFlagsThink => CPhysicsPropClearFlagsThinkHook;
    public ICPhysicsPropClearThrownByPlayerThinkHook ClearThrownByPlayerThink => CPhysicsPropClearThrownByPlayerThinkHook;

    internal void UnregisterListeners()
    {
        CPhysicsPropClearFlagsThinkHook.UnregisterListeners();
        CPhysicsPropClearThrownByPlayerThinkHook.UnregisterListeners();
    }
}