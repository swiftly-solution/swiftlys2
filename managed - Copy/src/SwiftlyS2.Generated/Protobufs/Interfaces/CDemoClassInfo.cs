
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDemoClassInfo : ITypedProtobuf<CDemoClassInfo>
{
  static CDemoClassInfo ITypedProtobuf<CDemoClassInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CDemoClassInfoImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CDemoClassInfo_class_t> Classes { get; }

}
