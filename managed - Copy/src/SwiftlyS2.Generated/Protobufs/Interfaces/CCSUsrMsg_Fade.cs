
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_Fade : ITypedProtobuf<CCSUsrMsg_Fade>, INetMessage<CCSUsrMsg_Fade>, IDisposable
{
  static int INetMessage<CCSUsrMsg_Fade>.MessageId => 313;
  
  static string INetMessage<CCSUsrMsg_Fade>.MessageName => "CCSUsrMsg_Fade";

  static CCSUsrMsg_Fade ITypedProtobuf<CCSUsrMsg_Fade>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_FadeImpl(handle, isManuallyAllocated);


  public int Duration { get; set; }


  public int HoldTime { get; set; }


  public int Flags { get; set; }


  public Color Clr { get; set; }

}
