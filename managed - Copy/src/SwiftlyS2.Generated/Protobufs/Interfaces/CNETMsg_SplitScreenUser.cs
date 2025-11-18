
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CNETMsg_SplitScreenUser : ITypedProtobuf<CNETMsg_SplitScreenUser>, INetMessage<CNETMsg_SplitScreenUser>, IDisposable
{
  static int INetMessage<CNETMsg_SplitScreenUser>.MessageId => 3;
  
  static string INetMessage<CNETMsg_SplitScreenUser>.MessageName => "CNETMsg_SplitScreenUser";

  static CNETMsg_SplitScreenUser ITypedProtobuf<CNETMsg_SplitScreenUser>.Wrap(nint handle, bool isManuallyAllocated) => new CNETMsg_SplitScreenUserImpl(handle, isManuallyAllocated);


  public int Slot { get; set; }

}
