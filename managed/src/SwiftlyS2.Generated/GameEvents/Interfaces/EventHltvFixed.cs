using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hltv_fixed"
/// show from fixed view
/// </summary>
public interface EventHltvFixed : IGameEvent<EventHltvFixed>
{

    static EventHltvFixed IGameEvent<EventHltvFixed>.Create( nint address ) => new EventHltvFixedImpl(address);

    static string IGameEvent<EventHltvFixed>.GetName() => "hltv_fixed";

    static uint IGameEvent<EventHltvFixed>.GetHash() => 0xCA86FB76u;
    /// <summary>
    /// camera position in world
    /// <br/>
    /// type: long
    /// </summary>
    public int PosX { get; set; }

    /// <summary>
    /// type: long
    /// </summary>
    public int Posy { get; set; }

    /// <summary>
    /// type: long
    /// </summary>
    public int PosZ { get; set; }

    /// <summary>
    /// camera angles
    /// <br/>
    /// type: short
    /// </summary>
    public short Theta { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short Phi { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short Offset { get; set; }

    /// <summary>
    /// type: float
    /// </summary>
    public float FOv { get; set; }

    /// <summary>
    /// follow this player
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int Target { get; set; }

}
