using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBaseDoor(GameHooksService owner) : IGameHookDatamapCBaseDoor
{
    internal readonly CBaseDoorCloseAreaPortalsThinkHook CBaseDoorCloseAreaPortalsThinkHook = new(owner);
    internal readonly CBaseDoorDoorGoDownHook CBaseDoorDoorGoDownHook = new(owner);
    internal readonly CBaseDoorDoorGoUpHook CBaseDoorDoorGoUpHook = new(owner);
    internal readonly CBaseDoorDoorHitBottomHook CBaseDoorDoorHitBottomHook = new(owner);
    internal readonly CBaseDoorDoorHitTopHook CBaseDoorDoorHitTopHook = new(owner);
    internal readonly CBaseDoorDoorTouchHook CBaseDoorDoorTouchHook = new(owner);
    internal readonly CBaseDoorMovingSoundThinkHook CBaseDoorMovingSoundThinkHook = new(owner);

    public ICBaseDoorCloseAreaPortalsThinkHook CloseAreaPortalsThink => CBaseDoorCloseAreaPortalsThinkHook;
    public ICBaseDoorDoorGoDownHook DoorGoDown => CBaseDoorDoorGoDownHook;
    public ICBaseDoorDoorGoUpHook DoorGoUp => CBaseDoorDoorGoUpHook;
    public ICBaseDoorDoorHitBottomHook DoorHitBottom => CBaseDoorDoorHitBottomHook;
    public ICBaseDoorDoorHitTopHook DoorHitTop => CBaseDoorDoorHitTopHook;
    public ICBaseDoorDoorTouchHook DoorTouch => CBaseDoorDoorTouchHook;
    public ICBaseDoorMovingSoundThinkHook MovingSoundThink => CBaseDoorMovingSoundThinkHook;

    internal void UnregisterListeners()
    {
        CBaseDoorCloseAreaPortalsThinkHook.UnregisterListeners();
        CBaseDoorDoorGoDownHook.UnregisterListeners();
        CBaseDoorDoorGoUpHook.UnregisterListeners();
        CBaseDoorDoorHitBottomHook.UnregisterListeners();
        CBaseDoorDoorHitTopHook.UnregisterListeners();
        CBaseDoorDoorTouchHook.UnregisterListeners();
        CBaseDoorMovingSoundThinkHook.UnregisterListeners();
    }
}