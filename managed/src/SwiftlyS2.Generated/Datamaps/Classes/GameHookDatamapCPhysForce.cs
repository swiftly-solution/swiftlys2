using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPhysForce : IGameHookDatamapCPhysForce
{
    internal readonly CPhysForceForceOffHook CPhysForceForceOffHook = new();
    internal readonly CPhysForceInitialThinkHook CPhysForceInitialThinkHook = new();

    public ICPhysForceForceOffHook ForceOff => CPhysForceForceOffHook;
    public ICPhysForceInitialThinkHook InitialThink => CPhysForceInitialThinkHook;

    internal void UnregisterListeners()
    {
        CPhysForceForceOffHook.UnregisterListeners();
        CPhysForceInitialThinkHook.UnregisterListeners();
    }
}