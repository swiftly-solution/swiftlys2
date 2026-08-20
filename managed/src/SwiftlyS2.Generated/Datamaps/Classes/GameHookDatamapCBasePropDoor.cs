using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBasePropDoor : IGameHookDatamapCBasePropDoor
{
    internal readonly CBasePropDoorDisableAreaPortalThinkHook CBasePropDoorDisableAreaPortalThinkHook = new();
    internal readonly CBasePropDoorDoorAutoCloseThinkHook CBasePropDoorDoorAutoCloseThinkHook = new();
    internal readonly CBasePropDoorDoorCloseMoveDoneHook CBasePropDoorDoorCloseMoveDoneHook = new();
    internal readonly CBasePropDoorDoorOpenMoveDoneHook CBasePropDoorDoorOpenMoveDoneHook = new();

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