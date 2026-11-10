using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCLogicGameStateReport(GameHooksService owner) : IGameHookDatamapCLogicGameStateReport
{
    internal readonly CLogicGameStateReportSetGameStateReportThinkHook CLogicGameStateReportSetGameStateReportThinkHook = new(owner);

    public ICLogicGameStateReportSetGameStateReportThinkHook SetGameStateReportThink => CLogicGameStateReportSetGameStateReportThinkHook;

    internal void UnregisterListeners()
    {
        CLogicGameStateReportSetGameStateReportThinkHook.UnregisterListeners();
    }
}