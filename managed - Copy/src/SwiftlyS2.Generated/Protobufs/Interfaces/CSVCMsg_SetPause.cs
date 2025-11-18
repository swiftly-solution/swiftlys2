
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_SetPause : ITypedProtobuf<CSVCMsg_SetPause>, INetMessage<CSVCMsg_SetPause>, IDisposable
{
  static int INetMessage<CSVCMsg_SetPause>.MessageId => 43;
  
  static string INetMessage<CSVCMsg_SetPause>.MessageName => "CSVCMsg_SetPause";

  static CSVCMsg_SetPause ITypedProtobuf<CSVCMsg_SetPause>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_SetPauseImpl(handle, isManuallyAllocated);


  public bool Paused { get; set; }

}
