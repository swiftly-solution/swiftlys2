using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPhysImpact(GameHooksService owner) : IGameHookDatamapCPhysImpact
{
    internal readonly CPhysImpactPointAtEntityHook CPhysImpactPointAtEntityHook = new(owner);

    public ICPhysImpactPointAtEntityHook PointAtEntity => CPhysImpactPointAtEntityHook;

    internal void UnregisterListeners()
    {
        CPhysImpactPointAtEntityHook.UnregisterListeners();
    }
}