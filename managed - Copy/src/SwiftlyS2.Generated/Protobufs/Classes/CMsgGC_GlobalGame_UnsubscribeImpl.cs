
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGC_GlobalGame_UnsubscribeImpl : TypedProtobuf<CMsgGC_GlobalGame_Unsubscribe>, CMsgGC_GlobalGame_Unsubscribe
{
  public CMsgGC_GlobalGame_UnsubscribeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Timeleft
  { get => Accessor.GetInt32("timeleft"); set => Accessor.SetInt32("timeleft", value); }

}
