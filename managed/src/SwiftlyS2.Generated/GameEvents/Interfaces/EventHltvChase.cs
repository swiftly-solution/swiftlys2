using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hltv_chase"
/// shot of a single entity
/// </summary>
public interface EventHltvChase : IGameEvent<EventHltvChase>
{

    static EventHltvChase IGameEvent<EventHltvChase>.Create( nint address ) => new EventHltvChaseImpl(address);

    static string IGameEvent<EventHltvChase>.GetName() => "hltv_chase";

    static uint IGameEvent<EventHltvChase>.GetHash() => 0xEB73303Au;
    /// <summary>
    /// primary traget index
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int Target1 { get; set; }

    /// <summary>
    /// secondary traget index or 0
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int Target2 { get; set; }

    /// <summary>
    /// camera distance
    /// <br/>
    /// type: short
    /// </summary>
    public short Distance { get; set; }

    /// <summary>
    /// view angle horizontal
    /// <br/>
    /// type: short
    /// </summary>
    public short Theta { get; set; }

    /// <summary>
    /// view angle vertical
    /// <br/>
    /// type: short
    /// </summary>
    public short Phi { get; set; }

    /// <summary>
    /// camera inertia
    /// <br/>
    /// type: byte
    /// </summary>
    public byte Inertia { get; set; }

    /// <summary>
    /// diretcor suggests to show ineye
    /// <br/>
    /// type: byte
    /// </summary>
    public byte InEye { get; set; }

}
