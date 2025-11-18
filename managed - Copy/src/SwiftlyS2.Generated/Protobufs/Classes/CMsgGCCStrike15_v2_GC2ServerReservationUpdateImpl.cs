
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_GC2ServerReservationUpdateImpl : TypedProtobuf<CMsgGCCStrike15_v2_GC2ServerReservationUpdate>, CMsgGCCStrike15_v2_GC2ServerReservationUpdate
{
  public CMsgGCCStrike15_v2_GC2ServerReservationUpdateImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint ViewersExternalTotal
  { get => Accessor.GetUInt32("viewers_external_total"); set => Accessor.SetUInt32("viewers_external_total", value); }


  public uint ViewersExternalSteam
  { get => Accessor.GetUInt32("viewers_external_steam"); set => Accessor.SetUInt32("viewers_external_steam", value); }

}
