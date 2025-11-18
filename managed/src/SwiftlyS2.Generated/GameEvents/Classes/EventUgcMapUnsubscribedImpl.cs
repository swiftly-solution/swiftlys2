using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEventDefinitions;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "ugc_map_unsubscribed"
/// </summary>
internal class EventUgcMapUnsubscribedImpl : GameEvent<EventUgcMapUnsubscribed>, EventUgcMapUnsubscribed
{

    public EventUgcMapUnsubscribedImpl( nint address ) : base(address)
    {
    }

    public ulong PublishedFileId { get => Accessor.GetUInt64("published_file_id"); set => Accessor.SetUInt64("published_file_id", value); }
}
