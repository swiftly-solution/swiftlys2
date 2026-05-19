using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface ISimulateUserCommandsController
{
    /// <summary>
    /// The player who received usercmds.
    /// </summary>
    public IPlayer Player { get; set; }

    /// <summary>
    /// The result of the hook, used to determine whether to block the original function or not.
    /// </summary>
    public HookResult Result { get; set; }
}

public delegate void OnSimulateUserCommandsDelegate( ref ISimulateUserCommandsController controller );

public interface ISimulateUserCommandsEvents
{
    /// <summary>
    /// Event triggered before the simulation of user commands happen.
    /// </summary>
    public event OnSimulateUserCommandsDelegate Pre;

    /// <summary>
    /// Event triggered after the simulation of user commands has been finished.
    /// </summary>
    public event OnSimulateUserCommandsDelegate Post;
}
