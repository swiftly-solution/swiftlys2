
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_HLTVStatus : ITypedProtobuf<CSVCMsg_HLTVStatus>, INetMessage<CSVCMsg_HLTVStatus>, IDisposable
{
  static int INetMessage<CSVCMsg_HLTVStatus>.MessageId => 62;
  
  static string INetMessage<CSVCMsg_HLTVStatus>.MessageName => "CSVCMsg_HLTVStatus";

  static CSVCMsg_HLTVStatus ITypedProtobuf<CSVCMsg_HLTVStatus>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_HLTVStatusImpl(handle, isManuallyAllocated);


  public string Master { get; set; }


  public int Clients { get; set; }


  public int Slots { get; set; }


  public int Proxies { get; set; }

}
