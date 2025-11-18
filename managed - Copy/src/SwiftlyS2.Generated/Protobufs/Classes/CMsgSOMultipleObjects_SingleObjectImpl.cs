
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSOMultipleObjects_SingleObjectImpl : TypedProtobuf<CMsgSOMultipleObjects_SingleObject>, CMsgSOMultipleObjects_SingleObject
{
  public CMsgSOMultipleObjects_SingleObjectImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int TypeId
  { get => Accessor.GetInt32("type_id"); set => Accessor.SetInt32("type_id", value); }


  public byte[] ObjectData
  { get => Accessor.GetBytes("object_data"); set => Accessor.SetBytes("object_data", value); }

}
