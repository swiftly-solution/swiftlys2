
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_CloseCaptionDirect : ITypedProtobuf<CCSUsrMsg_CloseCaptionDirect>, INetMessage<CCSUsrMsg_CloseCaptionDirect>, IDisposable
{
  static int INetMessage<CCSUsrMsg_CloseCaptionDirect>.MessageId => 316;
  
  static string INetMessage<CCSUsrMsg_CloseCaptionDirect>.MessageName => "CCSUsrMsg_CloseCaptionDirect";

  static CCSUsrMsg_CloseCaptionDirect ITypedProtobuf<CCSUsrMsg_CloseCaptionDirect>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_CloseCaptionDirectImpl(handle, isManuallyAllocated);


  public uint Hash { get; set; }


  public int Duration { get; set; }


  public bool FromPlayer { get; set; }

}
