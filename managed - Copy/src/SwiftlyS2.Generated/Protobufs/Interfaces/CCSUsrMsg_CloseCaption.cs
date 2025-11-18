
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_CloseCaption : ITypedProtobuf<CCSUsrMsg_CloseCaption>, INetMessage<CCSUsrMsg_CloseCaption>, IDisposable
{
  static int INetMessage<CCSUsrMsg_CloseCaption>.MessageId => 315;
  
  static string INetMessage<CCSUsrMsg_CloseCaption>.MessageName => "CCSUsrMsg_CloseCaption";

  static CCSUsrMsg_CloseCaption ITypedProtobuf<CCSUsrMsg_CloseCaption>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_CloseCaptionImpl(handle, isManuallyAllocated);


  public uint Hash { get; set; }


  public int Duration { get; set; }


  public bool FromPlayer { get; set; }


  public string Cctoken { get; set; }

}
