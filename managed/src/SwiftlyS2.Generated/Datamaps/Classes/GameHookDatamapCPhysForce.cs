using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPhysForce(GameHooksService owner) : IGameHookDatamapCPhysForce
{
    internal readonly CPhysForceForceOffHook CPhysForceForceOffHook = new(owner);
    internal readonly CPhysForceInitialThinkHook CPhysForceInitialThinkHook = new(owner);

    public ICPhysForceForceOffHook ForceOff => CPhysForceForceOffHook;
    public ICPhysForceInitialThinkHook InitialThink => CPhysForceInitialThinkHook;

    internal void UnregisterListeners()
    {
        CPhysForceForceOffHook.UnregisterListeners();
        CPhysForceInitialThinkHook.UnregisterListeners();
    }
}