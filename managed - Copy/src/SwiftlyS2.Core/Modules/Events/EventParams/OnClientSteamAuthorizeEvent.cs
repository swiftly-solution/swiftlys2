using SwiftlyS2.Shared.Events;

namespace SwiftlyS2.Core.Events;

internal class OnClientSteamAuthorizeEvent : IOnClientSteamAuthorizeEvent
{

  public int PlayerId { get; set; }
} 