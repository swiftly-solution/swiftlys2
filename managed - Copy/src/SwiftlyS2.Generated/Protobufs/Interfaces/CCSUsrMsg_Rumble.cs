
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_Rumble : ITypedProtobuf<CCSUsrMsg_Rumble>, INetMessage<CCSUsrMsg_Rumble>, IDisposable
{
  static int INetMessage<CCSUsrMsg_Rumble>.MessageId => 314;
  
  static string INetMessage<CCSUsrMsg_Rumble>.MessageName => "CCSUsrMsg_Rumble";

  static CCSUsrMsg_Rumble ITypedProtobuf<CCSUsrMsg_Rumble>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_RumbleImpl(handle, isManuallyAllocated);


  public int Index { get; set; }


  public int Data { get; set; }


  public int Flags { get; set; }

}
