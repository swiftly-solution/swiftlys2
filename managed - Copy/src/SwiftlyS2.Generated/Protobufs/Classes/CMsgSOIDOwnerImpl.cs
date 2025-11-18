
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSOIDOwnerImpl : TypedProtobuf<CMsgSOIDOwner>, CMsgSOIDOwner
{
  public CMsgSOIDOwnerImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Type
  { get => Accessor.GetUInt32("type"); set => Accessor.SetUInt32("type", value); }


  public ulong Id
  { get => Accessor.GetUInt64("id"); set => Accessor.SetUInt64("id", value); }

}
