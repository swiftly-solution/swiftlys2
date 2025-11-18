using SwiftlyS2.Shared.Events;

namespace SwiftlyS2.Core.Events;

internal class OnClientSteamAuthorizeFailEvent : IOnClientSteamAuthorizeFailEvent
{

  public int PlayerId { get; set; }
} 