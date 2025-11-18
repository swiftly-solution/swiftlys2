using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "endmatch_mapvote_selecting_map"
/// </summary>
public interface EventEndmatchMapvoteSelectingMap : IGameEvent<EventEndmatchMapvoteSelectingMap>
{

    static EventEndmatchMapvoteSelectingMap IGameEvent<EventEndmatchMapvoteSelectingMap>.Create( nint address ) => new EventEndmatchMapvoteSelectingMapImpl(address);

    static string IGameEvent<EventEndmatchMapvoteSelectingMap>.GetName() => "endmatch_mapvote_selecting_map";

    static uint IGameEvent<EventEndmatchMapvoteSelectingMap>.GetHash() => 0x9694B570u;
    /// <summary>
    /// Number of "ties"
    /// <br/>
    /// type: byte
    /// </summary>
    public byte Count { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte Slot1 { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte Slot2 { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte Slot3 { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte Slot4 { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte Slot5 { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte Slot6 { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte Slot7 { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte Slot8 { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte Slot9 { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte Slot10 { get; set; }

}
