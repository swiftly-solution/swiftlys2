
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgReplayUploadedToYouTube : ITypedProtobuf<CMsgReplayUploadedToYouTube>
{
  static CMsgReplayUploadedToYouTube ITypedProtobuf<CMsgReplayUploadedToYouTube>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgReplayUploadedToYouTubeImpl(handle, isManuallyAllocated);


  public string YoutubeUrl { get; set; }


  public string YoutubeAccountName { get; set; }


  public ulong SessionId { get; set; }

}
