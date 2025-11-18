
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CDemoSpawnGroupsImpl : TypedProtobuf<CDemoSpawnGroups>, CDemoSpawnGroups
{
  public CDemoSpawnGroupsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldValueType<byte[]> Msgs
  { get => new ProtobufRepeatedFieldValueType<byte[]>(Accessor, "msgs"); }

}
