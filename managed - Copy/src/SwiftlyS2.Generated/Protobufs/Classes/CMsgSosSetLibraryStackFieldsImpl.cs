
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSosSetLibraryStackFieldsImpl : NetMessage<CMsgSosSetLibraryStackFields>, CMsgSosSetLibraryStackFields
{
  public CMsgSosSetLibraryStackFieldsImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint StackHash
  { get => Accessor.GetUInt32("stack_hash"); set => Accessor.SetUInt32("stack_hash", value); }


  public byte[] PackedFields
  { get => Accessor.GetBytes("packed_fields"); set => Accessor.SetBytes("packed_fields", value); }

}
