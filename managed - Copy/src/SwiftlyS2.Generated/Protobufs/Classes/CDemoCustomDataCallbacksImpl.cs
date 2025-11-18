
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDemoCustomDataCallbacksImpl : TypedProtobuf<CDemoCustomDataCallbacks>, CDemoCustomDataCallbacks
{
  public CDemoCustomDataCallbacksImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldValueType<string> SaveId
  { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "save_id"); }

}
