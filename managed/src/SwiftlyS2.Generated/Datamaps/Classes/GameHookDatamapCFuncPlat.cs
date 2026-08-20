using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCFuncPlat : IGameHookDatamapCFuncPlat
{
    internal readonly CFuncPlatCallGoDownHook CFuncPlatCallGoDownHook = new();
    internal readonly CFuncPlatCallHitBottomHook CFuncPlatCallHitBottomHook = new();
    internal readonly CFuncPlatCallHitTopHook CFuncPlatCallHitTopHook = new();
    internal readonly CFuncPlatPlatUseHook CFuncPlatPlatUseHook = new();

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