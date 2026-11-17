using SwiftlyS2.Shared.Events;

namespace SwiftlyS2.Core.Events;

internal class OnClientConnectionEstablishedEvent : IOnClientConnectionEstablishedEvent
{
    public required int PlayerId { get; init; }
}
