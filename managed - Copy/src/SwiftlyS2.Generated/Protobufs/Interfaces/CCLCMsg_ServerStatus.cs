
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCLCMsg_ServerStatus : ITypedProtobuf<CCLCMsg_ServerStatus>, INetMessage<CCLCMsg_ServerStatus>, IDisposable
{
  static int INetMessage<CCLCMsg_ServerStatus>.MessageId => 31;
  
  static string INetMessage<CCLCMsg_ServerStatus>.MessageName => "CCLCMsg_ServerStatus";

  static CCLCMsg_ServerStatus ITypedProtobuf<CCLCMsg_ServerStatus>.Wrap(nint handle, bool isManuallyAllocated) => new CCLCMsg_ServerStatusImpl(handle, isManuallyAllocated);


  public bool Simplified { get; set; }

}
