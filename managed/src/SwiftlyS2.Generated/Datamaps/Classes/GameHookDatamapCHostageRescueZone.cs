using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCHostageRescueZone(GameHooksService owner) : IGameHookDatamapCHostageRescueZone
{
    internal readonly CHostageRescueZoneHostageRescueTouchHook CHostageRescueZoneHostageRescueTouchHook = new(owner);

    public ICHostageRescueZoneHostageRescueTouchHook HostageRescueTouch => CHostageRescueZoneHostageRescueTouchHook;

    internal void UnregisterListeners()
    {
        CHostageRescueZoneHostageRescueTouchHook.UnregisterListeners();
    }
}