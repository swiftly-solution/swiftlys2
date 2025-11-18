
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCRequestAnnouncementsResponseImpl : TypedProtobuf<CMsgGCRequestAnnouncementsResponse>, CMsgGCRequestAnnouncementsResponse
{
  public CMsgGCRequestAnnouncementsResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string AnnouncementTitle
  { get => Accessor.GetString("announcement_title"); set => Accessor.SetString("announcement_title", value); }


  public string Announcement
  { get => Accessor.GetString("announcement"); set => Accessor.SetString("announcement", value); }


  public string NextmatchTitle
  { get => Accessor.GetString("nextmatch_title"); set => Accessor.SetString("nextmatch_title", value); }


  public string Nextmatch
  { get => Accessor.GetString("nextmatch"); set => Accessor.SetString("nextmatch", value); }

}
