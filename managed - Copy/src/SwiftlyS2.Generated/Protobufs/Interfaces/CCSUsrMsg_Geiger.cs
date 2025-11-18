
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_Geiger : ITypedProtobuf<CCSUsrMsg_Geiger>, INetMessage<CCSUsrMsg_Geiger>, IDisposable
{
  static int INetMessage<CCSUsrMsg_Geiger>.MessageId => 302;
  
  static string INetMessage<CCSUsrMsg_Geiger>.MessageName => "CCSUsrMsg_Geiger";

  static CCSUsrMsg_Geiger ITypedProtobuf<CCSUsrMsg_Geiger>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_GeigerImpl(handle, isManuallyAllocated);


  public int Range { get; set; }

}
