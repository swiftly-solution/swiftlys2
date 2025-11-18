using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "open_crate_instr"
/// </summary>
public interface EventOpenCrateInstr : IGameEvent<EventOpenCrateInstr>
{

    static EventOpenCrateInstr IGameEvent<EventOpenCrateInstr>.Create( nint address ) => new EventOpenCrateInstrImpl(address);

    static string IGameEvent<EventOpenCrateInstr>.GetName() => "open_crate_instr";

    static uint IGameEvent<EventOpenCrateInstr>.GetHash() => 0x76409C38u;
    /// <summary>
    /// player entindex
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// player entindex
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // player entindex
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// player entindex
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// crate entindex
    /// <br/>
    /// type: short
    /// </summary>
    public short Subject { get; set; }

    /// <summary>
    /// type of crate (metal, wood, or paradrop)
    /// <br/>
    /// type: string
    /// </summary>
    public string Type { get; set; }

}
