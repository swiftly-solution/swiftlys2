
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDemoClassInfoImpl : TypedProtobuf<CDemoClassInfo>, CDemoClassInfo
{
  public CDemoClassInfoImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CDemoClassInfo_class_t> Classes
  { get => new ProtobufRepeatedFieldSubMessageType<CDemoClassInfo_class_t>(Accessor, "classes"); }

}
