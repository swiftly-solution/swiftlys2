namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCBaseDoor
{
    public ICBaseDoorCloseAreaPortalsThinkHook CloseAreaPortalsThink { get; }
    public ICBaseDoorDoorGoDownHook DoorGoDown { get; }
    public ICBaseDoorDoorGoUpHook DoorGoUp { get; }
    public ICBaseDoorDoorHitBottomHook DoorHitBottom { get; }
    public ICBaseDoorDoorHitTopHook DoorHitTop { get; }
    public ICBaseDoorDoorTouchHook DoorTouch { get; }
    public ICBaseDoorMovingSoundThinkHook MovingSoundThink { get; }
}