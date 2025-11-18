
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CSVCMsg_ClassInfo : ITypedProtobuf<CSVCMsg_ClassInfo>, INetMessage<CSVCMsg_ClassInfo>, IDisposable
{
  static int INetMessage<CSVCMsg_ClassInfo>.MessageId => 42;
  
  static string INetMessage<CSVCMsg_ClassInfo>.MessageName => "CSVCMsg_ClassInfo";

  static CSVCMsg_ClassInfo ITypedProtobuf<CSVCMsg_ClassInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_ClassInfoImpl(handle, isManuallyAllocated);


  public bool CreateOnClient { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CSVCMsg_ClassInfo_class_t> Classes { get; }

}
