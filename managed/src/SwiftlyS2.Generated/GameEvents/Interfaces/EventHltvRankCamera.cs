using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hltv_rank_camera"
/// a camera ranking
/// </summary>
public interface EventHltvRankCamera : IGameEvent<EventHltvRankCamera>
{

    static EventHltvRankCamera IGameEvent<EventHltvRankCamera>.Create( nint address ) => new EventHltvRankCameraImpl(address);

    static string IGameEvent<EventHltvRankCamera>.GetName() => "hltv_rank_camera";

    static uint IGameEvent<EventHltvRankCamera>.GetHash() => 0x493E49E8u;
    /// <summary>
    /// fixed camera index
    /// <br/>
    /// type: byte
    /// </summary>
    public byte Index { get; set; }

    /// <summary>
    /// ranking, how interesting is this camera view
    /// <br/>
    /// type: float
    /// </summary>
    public float Rank { get; set; }

    /// <summary>
    /// best/closest target entity
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int Target { get; set; }

}
