using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when a client processes user commands.
/// This callback is a hot path, be careful with it and don't do anything expensive.
/// </summary>
public interface IOnClientProcessUsercmdsEvent {

  /// <summary>
  /// The player ID of the client that processed the user commands.
  /// </summary>
  public int PlayerId { get; }

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
}