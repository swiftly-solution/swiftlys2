
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_ResetHud : ITypedProtobuf<CCSUsrMsg_ResetHud>, INetMessage<CCSUsrMsg_ResetHud>, IDisposable
{
  static int INetMessage<CCSUsrMsg_ResetHud>.MessageId => 309;
  
  static string INetMessage<CCSUsrMsg_ResetHud>.MessageName => "CCSUsrMsg_ResetHud";

  static CCSUsrMsg_ResetHud ITypedProtobuf<CCSUsrMsg_ResetHud>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_ResetHudImpl(handle, isManuallyAllocated);


  public bool Reset { get; set; }

}
