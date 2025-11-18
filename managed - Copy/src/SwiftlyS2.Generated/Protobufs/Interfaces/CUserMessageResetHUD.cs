
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageResetHUD : ITypedProtobuf<CUserMessageResetHUD>, INetMessage<CUserMessageResetHUD>, IDisposable
{
  static int INetMessage<CUserMessageResetHUD>.MessageId => 115;
  
  static string INetMessage<CUserMessageResetHUD>.MessageName => "CUserMessageResetHUD";

  static CUserMessageResetHUD ITypedProtobuf<CUserMessageResetHUD>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageResetHUDImpl(handle, isManuallyAllocated);


}
