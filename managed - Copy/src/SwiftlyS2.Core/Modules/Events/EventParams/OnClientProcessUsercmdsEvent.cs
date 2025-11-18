using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.Events;

internal class OnClientProcessUsercmdsEvent : IOnClientProcessUsercmdsEvent {
  public int PlayerId { get; set; }
  public required List<CSGOUserCmdPB> Usercmds { get; set; }
  public bool Paused { get; set; }
  public float Margin { get; set; }
}