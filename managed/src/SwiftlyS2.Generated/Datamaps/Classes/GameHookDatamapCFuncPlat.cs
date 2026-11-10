using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCFuncPlat(GameHooksService owner) : IGameHookDatamapCFuncPlat
{
    internal readonly CFuncPlatCallGoDownHook CFuncPlatCallGoDownHook = new(owner);
    internal readonly CFuncPlatCallHitBottomHook CFuncPlatCallHitBottomHook = new(owner);
    internal readonly CFuncPlatCallHitTopHook CFuncPlatCallHitTopHook = new(owner);
    internal readonly CFuncPlatPlatUseHook CFuncPlatPlatUseHook = new(owner);

    public ICFuncPlatCallGoDownHook CallGoDown => CFuncPlatCallGoDownHook;
    public ICFuncPlatCallHitBottomHook CallHitBottom => CFuncPlatCallHitBottomHook;
    public ICFuncPlatCallHitTopHook CallHitTop => CFuncPlatCallHitTopHook;
    public ICFuncPlatPlatUseHook PlatUse => CFuncPlatPlatUseHook;

    internal void UnregisterListeners()
    {
        CFuncPlatCallGoDownHook.UnregisterListeners();
        CFuncPlatCallHitBottomHook.UnregisterListeners();
        CFuncPlatCallHitTopHook.UnregisterListeners();
        CFuncPlatPlatUseHook.UnregisterListeners();
    }
}