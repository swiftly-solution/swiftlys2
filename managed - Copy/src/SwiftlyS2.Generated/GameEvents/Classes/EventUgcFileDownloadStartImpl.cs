using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "ugc_file_download_start"
/// </summary>
internal class EventUgcFileDownloadStartImpl : GameEvent<EventUgcFileDownloadStart>, EventUgcFileDownloadStart
{

  public EventUgcFileDownloadStartImpl(nint address) : base(address)
  {
  }

  // id of this specific content (may be image or map)
  public ulong HContent
  { get => Accessor.GetUInt64("hcontent"); set => Accessor.SetUInt64("hcontent", value); }

  // id of the associated content package
  public ulong PublishedFileId
  { get => Accessor.GetUInt64("published_file_id"); set => Accessor.SetUInt64("published_file_id", value); }
}
