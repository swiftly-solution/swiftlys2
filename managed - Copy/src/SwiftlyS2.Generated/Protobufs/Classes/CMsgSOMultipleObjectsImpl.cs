
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSOMultipleObjectsImpl : TypedProtobuf<CMsgSOMultipleObjects>, CMsgSOMultipleObjects
{
  public CMsgSOMultipleObjectsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CMsgSOMultipleObjects_SingleObject> ObjectsModified
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgSOMultipleObjects_SingleObject>(Accessor, "objects_modified"); }


  public ulong Version
  { get => Accessor.GetUInt64("version"); set => Accessor.SetUInt64("version", value); }


  public CMsgSOIDOwner OwnerSoid
  { get => new CMsgSOIDOwnerImpl(NativeNetMessages.GetNestedMessage(Address, "owner_soid"), false); }

}
