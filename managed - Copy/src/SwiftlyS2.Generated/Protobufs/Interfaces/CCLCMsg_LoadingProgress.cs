
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCLCMsg_LoadingProgress : ITypedProtobuf<CCLCMsg_LoadingProgress>, INetMessage<CCLCMsg_LoadingProgress>, IDisposable
{
  static int INetMessage<CCLCMsg_LoadingProgress>.MessageId => 27;
  
  static string INetMessage<CCLCMsg_LoadingProgress>.MessageName => "CCLCMsg_LoadingProgress";

  static CCLCMsg_LoadingProgress ITypedProtobuf<CCLCMsg_LoadingProgress>.Wrap(nint handle, bool isManuallyAllocated) => new CCLCMsg_LoadingProgressImpl(handle, isManuallyAllocated);


  public int Progress { get; set; }

}
