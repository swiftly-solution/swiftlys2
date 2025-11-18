using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "user_data_downloaded"
/// fired when achievements/stats are downloaded from Steam or XBox Live
/// </summary>
internal class EventUserDataDownloadedImpl : GameEvent<EventUserDataDownloaded>, EventUserDataDownloaded
{

  public EventUserDataDownloadedImpl(nint address) : base(address)
  {
  }
}
