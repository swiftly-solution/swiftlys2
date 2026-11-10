using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCMomentaryRotButton(GameHooksService owner) : IGameHookDatamapCMomentaryRotButton
{
    internal readonly CMomentaryRotButtonReturnMoveDoneHook CMomentaryRotButtonReturnMoveDoneHook = new(owner);
    internal readonly CMomentaryRotButtonSetPositionMoveDoneHook CMomentaryRotButtonSetPositionMoveDoneHook = new(owner);
    internal readonly CMomentaryRotButtonUpdateThinkHook CMomentaryRotButtonUpdateThinkHook = new(owner);
    internal readonly CMomentaryRotButtonUseMoveDoneHook CMomentaryRotButtonUseMoveDoneHook = new(owner);

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