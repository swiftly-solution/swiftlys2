
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_CmdKeyValues : ITypedProtobuf<CSVCMsg_CmdKeyValues>, INetMessage<CSVCMsg_CmdKeyValues>, IDisposable
{
  static int INetMessage<CSVCMsg_CmdKeyValues>.MessageId => 52;
  
  static string INetMessage<CSVCMsg_CmdKeyValues>.MessageName => "CSVCMsg_CmdKeyValues";

  static CSVCMsg_CmdKeyValues ITypedProtobuf<CSVCMsg_CmdKeyValues>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_CmdKeyValuesImpl(handle, isManuallyAllocated);


  public byte[] Data { get; set; }

}
