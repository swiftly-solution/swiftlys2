using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "round_end"
/// </summary>
public interface EventRoundEnd : IGameEvent<EventRoundEnd>
{

    static EventRoundEnd IGameEvent<EventRoundEnd>.Create( nint address ) => new EventRoundEndImpl(address);

    static string IGameEvent<EventRoundEnd>.GetName() => "round_end";

    static uint IGameEvent<EventRoundEnd>.GetHash() => 0x3ABA4E21u;
    /// <summary>
    /// winner team/user i
    /// <br/>
    /// type: byte
    /// </summary>
    public byte Winner { get; set; }

    /// <summary>
    /// reson why team won
    /// <br/>
    /// type: byte
    /// </summary>
    public byte Reason { get; set; }

    /// <summary>
    /// end round message
    /// <br/>
    /// type: string
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// type: float
    /// </summary>
    public float Time { get; set; }

    /// <summary>
    /// server-generated legacy value
    /// <br/>
    /// type: byte
    /// </summary>
    public byte Legacy { get; set; }

    /// <summary>
    /// total number of players alive at the end of round, used for statistics gathering, computed on the server in the event client is in replay when receiving this message
    /// <br/>
    /// type: short
    /// </summary>
    public short PlayerCount { get; set; }

    /// <summary>
    /// if set, don't play round end music, because action is still on-going
    /// <br/>
    /// type: byte
    /// </summary>
    public byte NoMusic { get; set; }

}
