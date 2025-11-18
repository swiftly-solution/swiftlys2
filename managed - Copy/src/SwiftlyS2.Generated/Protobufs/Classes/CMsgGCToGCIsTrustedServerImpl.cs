
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCToGCIsTrustedServerImpl : TypedProtobuf<CMsgGCToGCIsTrustedServer>, CMsgGCToGCIsTrustedServer
{
  public CMsgGCToGCIsTrustedServerImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong SteamId
  { get => Accessor.GetUInt64("steam_id"); set => Accessor.SetUInt64("steam_id", value); }

}
