
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgReplayUploadedToYouTubeImpl : TypedProtobuf<CMsgReplayUploadedToYouTube>, CMsgReplayUploadedToYouTube
{
  public CMsgReplayUploadedToYouTubeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string YoutubeUrl
  { get => Accessor.GetString("youtube_url"); set => Accessor.SetString("youtube_url", value); }


  public string YoutubeAccountName
  { get => Accessor.GetString("youtube_account_name"); set => Accessor.SetString("youtube_account_name", value); }


  public ulong SessionId
  { get => Accessor.GetUInt64("session_id"); set => Accessor.SetUInt64("session_id", value); }

}
