using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "endmatch_mapvote_selecting_map"
/// </summary>
internal class EventEndmatchMapvoteSelectingMapImpl : GameEvent<EventEndmatchMapvoteSelectingMap>, EventEndmatchMapvoteSelectingMap
{

  public EventEndmatchMapvoteSelectingMapImpl(nint address) : base(address)
  {
  }

  // Number of "ties"
  public byte Count
  { get => (byte)Accessor.GetInt32("count"); set => Accessor.SetInt32("count", value); }

  public byte Slot1
  { get => (byte)Accessor.GetInt32("slot1"); set => Accessor.SetInt32("slot1", value); }

  public byte Slot2
  { get => (byte)Accessor.GetInt32("slot2"); set => Accessor.SetInt32("slot2", value); }

  public byte Slot3
  { get => (byte)Accessor.GetInt32("slot3"); set => Accessor.SetInt32("slot3", value); }

  public byte Slot4
  { get => (byte)Accessor.GetInt32("slot4"); set => Accessor.SetInt32("slot4", value); }

  public byte Slot5
  { get => (byte)Accessor.GetInt32("slot5"); set => Accessor.SetInt32("slot5", value); }

  public byte Slot6
  { get => (byte)Accessor.GetInt32("slot6"); set => Accessor.SetInt32("slot6", value); }

  public byte Slot7
  { get => (byte)Accessor.GetInt32("slot7"); set => Accessor.SetInt32("slot7", value); }

  public byte Slot8
  { get => (byte)Accessor.GetInt32("slot8"); set => Accessor.SetInt32("slot8", value); }

  public byte Slot9
  { get => (byte)Accessor.GetInt32("slot9"); set => Accessor.SetInt32("slot9", value); }

  public byte Slot10
  { get => (byte)Accessor.GetInt32("slot10"); set => Accessor.SetInt32("slot10", value); }
}
