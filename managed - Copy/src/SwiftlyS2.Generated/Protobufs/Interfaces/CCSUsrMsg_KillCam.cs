
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_KillCam : ITypedProtobuf<CCSUsrMsg_KillCam>, INetMessage<CCSUsrMsg_KillCam>, IDisposable
{
  static int INetMessage<CCSUsrMsg_KillCam>.MessageId => 330;
  
  static string INetMessage<CCSUsrMsg_KillCam>.MessageName => "CCSUsrMsg_KillCam";

  static CCSUsrMsg_KillCam ITypedProtobuf<CCSUsrMsg_KillCam>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_KillCamImpl(handle, isManuallyAllocated);


  public int ObsMode { get; set; }


  public int FirstTarget { get; set; }


  public int SecondTarget { get; set; }

}
