namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCBasePropDoor
{
    public ICBasePropDoorDisableAreaPortalThinkHook DisableAreaPortalThink { get; }
    public ICBasePropDoorDoorAutoCloseThinkHook DoorAutoCloseThink { get; }
    public ICBasePropDoorDoorCloseMoveDoneHook DoorCloseMoveDone { get; }
    public ICBasePropDoorDoorOpenMoveDoneHook DoorOpenMoveDone { get; }
}