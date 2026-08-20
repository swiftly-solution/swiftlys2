using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCMomentaryRotButton : IGameHookDatamapCMomentaryRotButton
{
    internal readonly CMomentaryRotButtonReturnMoveDoneHook CMomentaryRotButtonReturnMoveDoneHook = new();
    internal readonly CMomentaryRotButtonSetPositionMoveDoneHook CMomentaryRotButtonSetPositionMoveDoneHook = new();
    internal readonly CMomentaryRotButtonUpdateThinkHook CMomentaryRotButtonUpdateThinkHook = new();
    internal readonly CMomentaryRotButtonUseMoveDoneHook CMomentaryRotButtonUseMoveDoneHook = new();

    public ICMomentaryRotButtonReturnMoveDoneHook ReturnMoveDone => CMomentaryRotButtonReturnMoveDoneHook;
    public ICMomentaryRotButtonSetPositionMoveDoneHook SetPositionMoveDone => CMomentaryRotButtonSetPositionMoveDoneHook;
    public ICMomentaryRotButtonUpdateThinkHook UpdateThink => CMomentaryRotButtonUpdateThinkHook;
    public ICMomentaryRotButtonUseMoveDoneHook UseMoveDone => CMomentaryRotButtonUseMoveDoneHook;

    internal void UnregisterListeners()
    {
        CMomentaryRotButtonReturnMoveDoneHook.UnregisterListeners();
        CMomentaryRotButtonSetPositionMoveDoneHook.UnregisterListeners();
        CMomentaryRotButtonUpdateThinkHook.UnregisterListeners();
        CMomentaryRotButtonUseMoveDoneHook.UnregisterListeners();
    }
}