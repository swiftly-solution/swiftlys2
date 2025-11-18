
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_GC2ServerReservationUpdate : ITypedProtobuf<CMsgGCCStrike15_v2_GC2ServerReservationUpdate>
{
  static CMsgGCCStrike15_v2_GC2ServerReservationUpdate ITypedProtobuf<CMsgGCCStrike15_v2_GC2ServerReservationUpdate>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_GC2ServerReservationUpdateImpl(handle, isManuallyAllocated);


  public uint ViewersExternalTotal { get; set; }


  public uint ViewersExternalSteam { get; set; }

}
