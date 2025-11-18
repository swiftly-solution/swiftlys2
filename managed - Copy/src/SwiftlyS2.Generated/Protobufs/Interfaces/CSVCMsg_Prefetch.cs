
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_Prefetch : ITypedProtobuf<CSVCMsg_Prefetch>, INetMessage<CSVCMsg_Prefetch>, IDisposable
{
  static int INetMessage<CSVCMsg_Prefetch>.MessageId => 56;
  
  static string INetMessage<CSVCMsg_Prefetch>.MessageName => "CSVCMsg_Prefetch";

  static CSVCMsg_Prefetch ITypedProtobuf<CSVCMsg_Prefetch>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_PrefetchImpl(handle, isManuallyAllocated);


  public int SoundIndex { get; set; }


  public PrefetchType ResourceType { get; set; }

}
