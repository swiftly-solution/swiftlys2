using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "ugc_map_info_received"
/// </summary>
internal class EventUgcMapInfoReceivedImpl : GameEvent<EventUgcMapInfoReceived>, EventUgcMapInfoReceived
{

  public EventUgcMapInfoReceivedImpl(nint address) : base(address)
  {
  }

  public ulong PublishedFileId
  { get => Accessor.GetUInt64("published_file_id"); set => Accessor.SetUInt64("published_file_id", value); }
}
