using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPhysicsPropRespawnable : IGameHookDatamapCPhysicsPropRespawnable
{
    internal readonly CPhysicsPropRespawnableMaterializeHook CPhysicsPropRespawnableMaterializeHook = new();

    public ICPhysicsPropRespawnableMaterializeHook Materialize => CPhysicsPropRespawnableMaterializeHook;

    internal void UnregisterListeners()
    {
        CPhysicsPropRespawnableMaterializeHook.UnregisterListeners();
    }
}