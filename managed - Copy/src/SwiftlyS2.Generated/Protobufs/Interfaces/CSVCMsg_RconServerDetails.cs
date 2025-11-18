
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_RconServerDetails : ITypedProtobuf<CSVCMsg_RconServerDetails>, INetMessage<CSVCMsg_RconServerDetails>, IDisposable
{
  static int INetMessage<CSVCMsg_RconServerDetails>.MessageId => 71;
  
  static string INetMessage<CSVCMsg_RconServerDetails>.MessageName => "CSVCMsg_RconServerDetails";

  static CSVCMsg_RconServerDetails ITypedProtobuf<CSVCMsg_RconServerDetails>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_RconServerDetailsImpl(handle, isManuallyAllocated);


  public byte[] Token { get; set; }


  public string Details { get; set; }

}
