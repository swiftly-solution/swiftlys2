
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSOSingleObjectImpl : TypedProtobuf<CMsgSOSingleObject>, CMsgSOSingleObject
{
  public CMsgSOSingleObjectImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int TypeId
  { get => Accessor.GetInt32("type_id"); set => Accessor.SetInt32("type_id", value); }


  public byte[] ObjectData
  { get => Accessor.GetBytes("object_data"); set => Accessor.SetBytes("object_data", value); }


  public ulong Version
  { get => Accessor.GetUInt64("version"); set => Accessor.SetUInt64("version", value); }


  public CMsgSOIDOwner OwnerSoid
  { get => new CMsgSOIDOwnerImpl(NativeNetMessages.GetNestedMessage(Address, "owner_soid"), false); }

}
