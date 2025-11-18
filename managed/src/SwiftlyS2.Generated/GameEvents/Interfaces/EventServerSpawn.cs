using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "server_spawn"
/// send once a server starts
/// </summary>
public interface EventServerSpawn : IGameEvent<EventServerSpawn>
{

    static EventServerSpawn IGameEvent<EventServerSpawn>.Create( nint address ) => new EventServerSpawnImpl(address);

    static string IGameEvent<EventServerSpawn>.GetName() => "server_spawn";

    static uint IGameEvent<EventServerSpawn>.GetHash() => 0x7039CD72u;
    /// <summary>
    /// public host name
    /// <br/>
    /// type: string
    /// </summary>
    public string Hostname { get; set; }

    /// <summary>
    /// hostame, IP or DNS name
    /// <br/>
    /// type: string
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// server port
    /// <br/>
    /// type: short
    /// </summary>
    public short Port { get; set; }

    /// <summary>
    /// game dir
    /// <br/>
    /// type: string
    /// </summary>
    public string Game { get; set; }

    /// <summary>
    /// map name
    /// <br/>
    /// type: string
    /// </summary>
    public string MapName { get; set; }

    /// <summary>
    /// addon name
    /// <br/>
    /// type: string
    /// </summary>
    public string AddonName { get; set; }

    /// <summary>
    /// max players
    /// <br/>
    /// type: long
    /// </summary>
    public int MaxPlayers { get; set; }

    /// <summary>
    /// WIN32, LINUX
    /// <br/>
    /// type: string
    /// </summary>
    public string Os { get; set; }

    /// <summary>
    /// true if dedicated server
    /// <br/>
    /// type: bool
    /// </summary>
    public bool Dedicated { get; set; }

    /// <summary>
    /// true if password protected
    /// <br/>
    /// type: bool
    /// </summary>
    public bool Password { get; set; }

}
