
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDemoClassInfo_class_t : ITypedProtobuf<CDemoClassInfo_class_t>
{
  static CDemoClassInfo_class_t ITypedProtobuf<CDemoClassInfo_class_t>.Wrap(nint handle, bool isManuallyAllocated) => new CDemoClassInfo_class_tImpl(handle, isManuallyAllocated);


  public int ClassId { get; set; }


  public string NetworkName { get; set; }


  public string TableName { get; set; }

}
