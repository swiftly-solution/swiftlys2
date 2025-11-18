
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgLANServerAvailableImpl : TypedProtobuf<CMsgLANServerAvailable>, CMsgLANServerAvailable
{
  public CMsgLANServerAvailableImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong LobbyId
  { get => Accessor.GetUInt64("lobby_id"); set => Accessor.SetUInt64("lobby_id", value); }

}
