
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCLCMsg_BaselineAck : ITypedProtobuf<CCLCMsg_BaselineAck>, INetMessage<CCLCMsg_BaselineAck>, IDisposable
{
  static int INetMessage<CCLCMsg_BaselineAck>.MessageId => 23;
  
  static string INetMessage<CCLCMsg_BaselineAck>.MessageName => "CCLCMsg_BaselineAck";

  static CCLCMsg_BaselineAck ITypedProtobuf<CCLCMsg_BaselineAck>.Wrap(nint handle, bool isManuallyAllocated) => new CCLCMsg_BaselineAckImpl(handle, isManuallyAllocated);


  public int BaselineTick { get; set; }


  public int BaselineNr { get; set; }

}
