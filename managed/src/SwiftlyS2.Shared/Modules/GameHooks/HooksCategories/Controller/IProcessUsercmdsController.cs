using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public interface IProcessUsercmdsController
{
    /// <summary>
    /// The player who received usercmds.
    /// </summary>
    public IPlayer Player { get; set; }

    /// <summary>
    /// The user commands that the client processed.
    /// </summary>
    public List<CSGOUserCmdPB> Usercmds { get; }

    /// <summary>
    /// Whether the client is paused.
    /// </summary>
    public bool Paused { get; }

    /// <summary>
    /// The margin of the client, milliseconds.
    /// </summary>
    public float Margin { get; }

    /// <summary>
    /// The result of the hook, used to determine whether to block the original function or not.
    /// </summary>
    public HookResult Result { get; set; }
}

public delegate void OnProcessUsercmdsDelegate( ref IProcessUsercmdsController controller );

public interface IProcessUsercmdsEvents
{
    /// <summary>
    /// Event triggered before an item can be acquired.
    /// </summary>
    public event OnProcessUsercmdsDelegate Pre;

    /// <summary>
    /// Event triggered after an item can be acquired.
    /// </summary>
    public event OnProcessUsercmdsDelegate Post;
}
