
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageGameTitle : ITypedProtobuf<CUserMessageGameTitle>, INetMessage<CUserMessageGameTitle>, IDisposable
{
  static int INetMessage<CUserMessageGameTitle>.MessageId => 107;
  
  static string INetMessage<CUserMessageGameTitle>.MessageName => "CUserMessageGameTitle";

  static CUserMessageGameTitle ITypedProtobuf<CUserMessageGameTitle>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageGameTitleImpl(handle, isManuallyAllocated);


}
