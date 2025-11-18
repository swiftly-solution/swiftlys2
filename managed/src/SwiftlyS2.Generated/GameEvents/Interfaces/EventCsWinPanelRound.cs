using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "cs_win_panel_round"
/// </summary>
public interface EventCsWinPanelRound : IGameEvent<EventCsWinPanelRound>
{

    static EventCsWinPanelRound IGameEvent<EventCsWinPanelRound>.Create( nint address ) => new EventCsWinPanelRoundImpl(address);

    static string IGameEvent<EventCsWinPanelRound>.GetName() => "cs_win_panel_round";

    static uint IGameEvent<EventCsWinPanelRound>.GetHash() => 0xFF5D5EC0u;
    /// <summary>
    /// type: bool
    /// </summary>
    public bool ShowTimerDefend { get; set; }

    /// <summary>
    /// type: bool
    /// </summary>
    public bool ShowTimerAttack { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short TimerTime { get; set; }

    /// <summary>
    /// define in cs_gamerules.h
    /// <br/>
    /// type: byte
    /// </summary>
    public byte FinalEvent { get; set; }

    /// <summary>
    /// type: string
    /// </summary>
    public string FunfactToken { get; set; }

    /// <summary>
    /// type: player_controller
    /// </summary>
    public int FunfactPlayer { get; set; }

    /// <summary>
    /// type: long
    /// </summary>
    public int FunfactData1 { get; set; }

    /// <summary>
    /// type: long
    /// </summary>
    public int FunfactData2 { get; set; }

    /// <summary>
    /// type: long
    /// </summary>
    public int FunfactData3 { get; set; }

}
