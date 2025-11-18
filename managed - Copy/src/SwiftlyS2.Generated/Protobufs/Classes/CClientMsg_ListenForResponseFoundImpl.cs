
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CClientMsg_ListenForResponseFoundImpl : TypedProtobuf<CClientMsg_ListenForResponseFound>, CClientMsg_ListenForResponseFound
{
  public CClientMsg_ListenForResponseFoundImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int PlayerSlot
  { get => Accessor.GetInt32("player_slot"); set => Accessor.SetInt32("player_slot", value); }

}
