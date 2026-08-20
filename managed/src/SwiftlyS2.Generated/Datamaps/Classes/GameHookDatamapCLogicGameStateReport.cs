using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GameHookDatamapCLogicGameStateReport : IGameHookDatamapCLogicGameStateReport
{
    internal readonly CLogicGameStateReportSetGameStateReportThinkHook CLogicGameStateReportSetGameStateReportThinkHook = new();

    public ICLogicGameStateReportSetGameStateReportThinkHook SetGameStateReportThink => CLogicGameStateReportSetGameStateReportThinkHook;

    internal void UnregisterListeners()
    {
        CLogicGameStateReportSetGameStateReportThinkHook.UnregisterListeners();
    }
}