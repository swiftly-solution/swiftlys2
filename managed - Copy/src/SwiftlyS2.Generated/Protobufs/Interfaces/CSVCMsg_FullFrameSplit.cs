
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_FullFrameSplit : ITypedProtobuf<CSVCMsg_FullFrameSplit>, INetMessage<CSVCMsg_FullFrameSplit>, IDisposable
{
  static int INetMessage<CSVCMsg_FullFrameSplit>.MessageId => 70;
  
  static string INetMessage<CSVCMsg_FullFrameSplit>.MessageName => "CSVCMsg_FullFrameSplit";

  static CSVCMsg_FullFrameSplit ITypedProtobuf<CSVCMsg_FullFrameSplit>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_FullFrameSplitImpl(handle, isManuallyAllocated);


  public int Tick { get; set; }


  public int Section { get; set; }


  public int Total { get; set; }


  public byte[] Data { get; set; }

}
