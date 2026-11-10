using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBasePropDoor(GameHooksService owner) : IGameHookDatamapCBasePropDoor
{
    internal readonly CBasePropDoorDisableAreaPortalThinkHook CBasePropDoorDisableAreaPortalThinkHook = new(owner);
    internal readonly CBasePropDoorDoorAutoCloseThinkHook CBasePropDoorDoorAutoCloseThinkHook = new(owner);
    internal readonly CBasePropDoorDoorCloseMoveDoneHook CBasePropDoorDoorCloseMoveDoneHook = new(owner);
    internal readonly CBasePropDoorDoorOpenMoveDoneHook CBasePropDoorDoorOpenMoveDoneHook = new(owner);

    public ICBasePropDoorDisableAreaPortalThinkHook DisableAreaPortalThink => CBasePropDoorDisableAreaPortalThinkHook;
    public ICBasePropDoorDoorAutoCloseThinkHook DoorAutoCloseThink => CBasePropDoorDoorAutoCloseThinkHook;
    public ICBasePropDoorDoorCloseMoveDoneHook DoorCloseMoveDone => CBasePropDoorDoorCloseMoveDoneHook;
    public ICBasePropDoorDoorOpenMoveDoneHook DoorOpenMoveDone => CBasePropDoorDoorOpenMoveDoneHook;

    internal void UnregisterListeners()
    {
        CBasePropDoorDisableAreaPortalThinkHook.UnregisterListeners();
        CBasePropDoorDoorAutoCloseThinkHook.UnregisterListeners();
        CBasePropDoorDoorCloseMoveDoneHook.UnregisterListeners();
        CBasePropDoorDoorOpenMoveDoneHook.UnregisterListeners();
    }
}