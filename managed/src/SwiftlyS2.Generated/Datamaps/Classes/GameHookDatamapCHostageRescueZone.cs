using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCHostageRescueZone : IGameHookDatamapCHostageRescueZone
{
    internal readonly CHostageRescueZoneHostageRescueTouchHook CHostageRescueZoneHostageRescueTouchHook = new();

    public ICHostageRescueZoneHostageRescueTouchHook HostageRescueTouch => CHostageRescueZoneHostageRescueTouchHook;

    internal void UnregisterListeners()
    {
        CHostageRescueZoneHostageRescueTouchHook.UnregisterListeners();
    }
}