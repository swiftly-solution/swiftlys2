
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCClientVersionUpdatedImpl : TypedProtobuf<CMsgGCClientVersionUpdated>, CMsgGCClientVersionUpdated
{
  public CMsgGCClientVersionUpdatedImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint ClientVersion
  { get => Accessor.GetUInt32("client_version"); set => Accessor.SetUInt32("client_version", value); }

}
