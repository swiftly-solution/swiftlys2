using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPhysicsPropRespawnable(GameHooksService owner) : IGameHookDatamapCPhysicsPropRespawnable
{
    internal readonly CPhysicsPropRespawnableMaterializeHook CPhysicsPropRespawnableMaterializeHook = new(owner);

    public ICPhysicsPropRespawnableMaterializeHook Materialize => CPhysicsPropRespawnableMaterializeHook;

    internal void UnregisterListeners()
    {
        CPhysicsPropRespawnableMaterializeHook.UnregisterListeners();
    }
}