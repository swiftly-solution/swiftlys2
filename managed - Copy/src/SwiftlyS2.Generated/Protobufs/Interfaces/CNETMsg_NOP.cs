
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CNETMsg_NOP : ITypedProtobuf<CNETMsg_NOP>, INetMessage<CNETMsg_NOP>, IDisposable
{
  static int INetMessage<CNETMsg_NOP>.MessageId => 0;
  
  static string INetMessage<CNETMsg_NOP>.MessageName => "CNETMsg_NOP";

  static CNETMsg_NOP ITypedProtobuf<CNETMsg_NOP>.Wrap(nint handle, bool isManuallyAllocated) => new CNETMsg_NOPImpl(handle, isManuallyAllocated);


}
