
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDemoFileInfo : ITypedProtobuf<CDemoFileInfo>
{
  static CDemoFileInfo ITypedProtobuf<CDemoFileInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CDemoFileInfoImpl(handle, isManuallyAllocated);


  public float PlaybackTime { get; set; }


  public int PlaybackTicks { get; set; }


  public int PlaybackFrames { get; set; }


  public CGameInfo GameInfo { get; }

}
