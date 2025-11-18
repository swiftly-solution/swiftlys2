
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgTEMuzzleFlash : ITypedProtobuf<CMsgTEMuzzleFlash>, INetMessage<CMsgTEMuzzleFlash>, IDisposable
{
  static int INetMessage<CMsgTEMuzzleFlash>.MessageId => 417;
  
  static string INetMessage<CMsgTEMuzzleFlash>.MessageName => "CMsgTEMuzzleFlash";

  static CMsgTEMuzzleFlash ITypedProtobuf<CMsgTEMuzzleFlash>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgTEMuzzleFlashImpl(handle, isManuallyAllocated);


  public Vector Origin { get; set; }


  public QAngle Angles { get; set; }


  public float Scale { get; set; }


  public uint Type { get; set; }

}
