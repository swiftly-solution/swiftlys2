using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "hltv_versioninfo"
/// </summary>
internal class EventHltvVersioninfoImpl : GameEvent<EventHltvVersioninfo>, EventHltvVersioninfo
{

    public EventHltvVersioninfoImpl( nint address ) : base(address)
    {
    }

    public int Version { get => Accessor.GetInt32("version"); set => Accessor.SetInt32("version", value); }
}
