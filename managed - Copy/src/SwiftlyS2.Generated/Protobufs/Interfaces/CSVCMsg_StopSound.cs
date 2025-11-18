
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_StopSound : ITypedProtobuf<CSVCMsg_StopSound>, INetMessage<CSVCMsg_StopSound>, IDisposable
{
  static int INetMessage<CSVCMsg_StopSound>.MessageId => 59;
  
  static string INetMessage<CSVCMsg_StopSound>.MessageName => "CSVCMsg_StopSound";

  static CSVCMsg_StopSound ITypedProtobuf<CSVCMsg_StopSound>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_StopSoundImpl(handle, isManuallyAllocated);


  public uint Guid { get; set; }

}
