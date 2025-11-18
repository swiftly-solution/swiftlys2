using SwiftlyS2.Shared.Events;
using SwiftlyS2.Shared.Misc;

namespace SwiftlyS2.Core.Events;

internal class OnClientConnectedEvent : IOnClientConnectedEvent
{

  public int PlayerId { get; set; }

  public HookResult Result { get; set; } = HookResult.Continue;

}
