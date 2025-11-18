using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hostname_changed"
/// </summary>
public interface EventHostnameChanged : IGameEvent<EventHostnameChanged>
{

    static EventHostnameChanged IGameEvent<EventHostnameChanged>.Create( nint address ) => new EventHostnameChangedImpl(address);

    static string IGameEvent<EventHostnameChanged>.GetName() => "hostname_changed";

    static uint IGameEvent<EventHostnameChanged>.GetHash() => 0x81496EB7u;
    /// <summary>
    /// type: string
    /// </summary>
    public string Hostname { get; set; }

}
