using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hltv_versioninfo"
/// </summary>
public interface EventHltvVersioninfo : IGameEvent<EventHltvVersioninfo>
{

    static EventHltvVersioninfo IGameEvent<EventHltvVersioninfo>.Create( nint address ) => new EventHltvVersioninfoImpl(address);

    static string IGameEvent<EventHltvVersioninfo>.GetName() => "hltv_versioninfo";

    static uint IGameEvent<EventHltvVersioninfo>.GetHash() => 0xAB9E0AFCu;
    /// <summary>
    /// type: long
    /// </summary>
    public int Version { get; set; }

}
