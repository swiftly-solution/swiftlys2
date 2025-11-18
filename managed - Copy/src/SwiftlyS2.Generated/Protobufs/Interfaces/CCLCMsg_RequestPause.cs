
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCLCMsg_RequestPause : ITypedProtobuf<CCLCMsg_RequestPause>, INetMessage<CCLCMsg_RequestPause>, IDisposable
{
  static int INetMessage<CCLCMsg_RequestPause>.MessageId => 33;
  
  static string INetMessage<CCLCMsg_RequestPause>.MessageName => "CCLCMsg_RequestPause";

  static CCLCMsg_RequestPause ITypedProtobuf<CCLCMsg_RequestPause>.Wrap(nint handle, bool isManuallyAllocated) => new CCLCMsg_RequestPauseImpl(handle, isManuallyAllocated);


  public RequestPause_t PauseType { get; set; }


  public int PauseGroup { get; set; }

}
