using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.Commands;

public interface ICommandContext {

  public bool IsSentByPlayer { get; }

  public IPlayer? Sender { get; }

  public string Prefix { get; }

  public bool IsSlient { get; }

  public string[] Args { get; }

  public void Reply(string message);
  
}