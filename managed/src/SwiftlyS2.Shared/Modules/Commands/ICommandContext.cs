using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.Commands;

public interface ICommandContext
{
    /// <summary>
    /// Gets a value indicating whether the command was sent by a player.
    /// </summary>
    public bool IsSentByPlayer { get; }

    /// <summary>
    /// Gets the player who sent the command, or null if the command was not sent by a player.
    /// </summary>
    public IPlayer? Sender { get; }

    /// <summary>
    /// Gets the command name itself.
    /// </summary>
    public string Prefix { get; }

    /// <summary>
    /// Gets a value indicating whether the command should be executed silently without broadcasting to other players.
    /// </summary>
    public bool IsSlient { get; }

    /// <summary>
    /// Gets the array of arguments passed with the command.
    /// </summary>
    public string[] Args { get; }

    /// <summary>
    /// Sends a reply message to the command sender.
    /// </summary>
    /// <param name="message">The message to send as a reply.</param>
    public void Reply( string message );
}