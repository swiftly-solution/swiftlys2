
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSOCacheVersionImpl : TypedProtobuf<CMsgSOCacheVersion>, CMsgSOCacheVersion
{
  public CMsgSOCacheVersionImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Version
  { get => Accessor.GetUInt64("version"); set => Accessor.SetUInt64("version", value); }

}
