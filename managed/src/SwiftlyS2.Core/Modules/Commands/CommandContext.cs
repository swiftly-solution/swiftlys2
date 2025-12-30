using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Players;
using SwiftlyS2.Core.Scheduler;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.Commands;

namespace SwiftlyS2.Core.Commands;

internal class CommandContext : ICommandContext
{
    public bool IsSentByPlayer { get; init; }

    public IPlayer? Sender { get; init; }

    public string Prefix { get; init; }

    public bool IsSlient { get; init; }

    public string CommandName { get; init; }

    public string[] Args { get; init; }

    public void Reply( string message )
    {
        if (IsSentByPlayer)
        {
            Sender?.SendMessage(MessageType.Chat, message);
        }
        else
        {
            NativeEngineHelpers.SendMessageToConsole(message + Environment.NewLine);
        }
    }

    public Task ReplyAsync( string message )
    {
        return SchedulerManager.QueueOrNow(() => Reply(message));
    }

    public CommandContext( int playerId, string[] args, string commandName, string prefix, bool slient )
    {
        IsSentByPlayer = playerId != -1;
        Sender = playerId != -1 ? new Player(playerId) : null;
        Prefix = prefix;
        IsSlient = slient;
        CommandName = commandName;
        Args = args;
    }

    public override string ToString()
    {
        return $"CommandContext {{ Sender: {(IsSentByPlayer ? $"Player: {Sender?.Controller.PlayerName ?? "Unknown"}" : "Console")}, Command: {Prefix}{CommandName}, Args: [{(Args.Length > 0 ? string.Join(" ", Args) : "None")}], Mode: {(IsSlient ? "Silent" : "Normal")} }}";
    }
}