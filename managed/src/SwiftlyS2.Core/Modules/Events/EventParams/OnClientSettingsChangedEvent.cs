using SwiftlyS2.Shared.Events;

namespace SwiftlyS2.Core.Events;

internal class OnClientSettingsChangedEvent : IOnClientSettingsChangedEvent
{
    public int PlayerId { get; set; }
}
