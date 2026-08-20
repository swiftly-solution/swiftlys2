using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCBaseDoor : IGameHookDatamapCBaseDoor
{
    internal readonly CBaseDoorCloseAreaPortalsThinkHook CBaseDoorCloseAreaPortalsThinkHook = new();
    internal readonly CBaseDoorDoorGoDownHook CBaseDoorDoorGoDownHook = new();
    internal readonly CBaseDoorDoorGoUpHook CBaseDoorDoorGoUpHook = new();
    internal readonly CBaseDoorDoorHitBottomHook CBaseDoorDoorHitBottomHook = new();
    internal readonly CBaseDoorDoorHitTopHook CBaseDoorDoorHitTopHook = new();
    internal readonly CBaseDoorDoorTouchHook CBaseDoorDoorTouchHook = new();
    internal readonly CBaseDoorMovingSoundThinkHook CBaseDoorMovingSoundThinkHook = new();

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