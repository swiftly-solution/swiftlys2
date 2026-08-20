using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCPhysImpact : IGameHookDatamapCPhysImpact
{
    internal readonly CPhysImpactPointAtEntityHook CPhysImpactPointAtEntityHook = new();

    public ICPhysImpactPointAtEntityHook PointAtEntity => CPhysImpactPointAtEntityHook;

    internal void UnregisterListeners()
    {
        CPhysImpactPointAtEntityHook.UnregisterListeners();
    }
}