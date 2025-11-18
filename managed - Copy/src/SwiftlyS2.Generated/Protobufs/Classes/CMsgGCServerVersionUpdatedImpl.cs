
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCServerVersionUpdatedImpl : TypedProtobuf<CMsgGCServerVersionUpdated>, CMsgGCServerVersionUpdated
{
  public CMsgGCServerVersionUpdatedImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint ServerVersion
  { get => Accessor.GetUInt32("server_version"); set => Accessor.SetUInt32("server_version", value); }

}
