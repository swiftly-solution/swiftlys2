
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSVCMsg_ClassInfo_class_t : ITypedProtobuf<CSVCMsg_ClassInfo_class_t>
{
  static CSVCMsg_ClassInfo_class_t ITypedProtobuf<CSVCMsg_ClassInfo_class_t>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_ClassInfo_class_tImpl(handle, isManuallyAllocated);


  public int ClassId { get; set; }


  public string ClassName { get; set; }

}
