using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "cs_win_panel_round"
/// </summary>
internal class EventCsWinPanelRoundImpl : GameEvent<EventCsWinPanelRound>, EventCsWinPanelRound
{

  public EventCsWinPanelRoundImpl(nint address) : base(address)
  {
  }

  public bool ShowTimerDefend
  { get => Accessor.GetBool("show_timer_defend"); set => Accessor.SetBool("show_timer_defend", value); }

  public bool ShowTimerAttack
  { get => Accessor.GetBool("show_timer_attack"); set => Accessor.SetBool("show_timer_attack", value); }

  public short TimerTime
  { get => (short)Accessor.GetInt32("timer_time"); set => Accessor.SetInt32("timer_time", value); }

  // define in cs_gamerules.h
  public byte FinalEvent
  { get => (byte)Accessor.GetInt32("final_event"); set => Accessor.SetInt32("final_event", value); }

  public string FunfactToken
  { get => Accessor.GetString("funfact_token"); set => Accessor.SetString("funfact_token", value); }

  public int FunfactPlayer
  { get => Accessor.GetPlayerSlot("funfact_player"); set => Accessor.SetPlayerSlot("funfact_player", value); }

  public int FunfactData1
  { get => Accessor.GetInt32("funfact_data1"); set => Accessor.SetInt32("funfact_data1", value); }

  public int FunfactData2
  { get => Accessor.GetInt32("funfact_data2"); set => Accessor.SetInt32("funfact_data2", value); }

  public int FunfactData3
  { get => Accessor.GetInt32("funfact_data3"); set => Accessor.SetInt32("funfact_data3", value); }
}
