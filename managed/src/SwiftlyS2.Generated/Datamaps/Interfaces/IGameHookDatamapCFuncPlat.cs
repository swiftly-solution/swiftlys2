namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCFuncPlat
{
    public ICFuncPlatCallGoDownHook CallGoDown { get; }
    public ICFuncPlatCallHitBottomHook CallHitBottom { get; }
    public ICFuncPlatCallHitTopHook CallHitTop { get; }
    public ICFuncPlatPlatUseHook PlatUse { get; }
}