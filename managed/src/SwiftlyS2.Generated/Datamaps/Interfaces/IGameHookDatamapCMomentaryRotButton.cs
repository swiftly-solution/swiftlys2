namespace SwiftlyS2.Shared.GameHooks;

public interface IGameHookDatamapCMomentaryRotButton
{
    public ICMomentaryRotButtonReturnMoveDoneHook ReturnMoveDone { get; }
    public ICMomentaryRotButtonSetPositionMoveDoneHook SetPositionMoveDone { get; }
    public ICMomentaryRotButtonUpdateThinkHook UpdateThink { get; }
    public ICMomentaryRotButtonUseMoveDoneHook UseMoveDone { get; }
}