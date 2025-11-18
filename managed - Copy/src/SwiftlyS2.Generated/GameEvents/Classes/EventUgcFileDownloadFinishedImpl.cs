using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "ugc_file_download_finished"
/// </summary>
internal class EventUgcFileDownloadFinishedImpl : GameEvent<EventUgcFileDownloadFinished>, EventUgcFileDownloadFinished
{

  public EventUgcFileDownloadFinishedImpl(nint address) : base(address)
  {
  }

  // id of this specific content (may be image or map)
  public ulong HContent
  { get => Accessor.GetUInt64("hcontent"); set => Accessor.SetUInt64("hcontent", value); }
}
