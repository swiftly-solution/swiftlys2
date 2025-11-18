
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_BetaEnrollment : ITypedProtobuf<CMsgGCCStrike15_v2_BetaEnrollment>
{
  static CMsgGCCStrike15_v2_BetaEnrollment ITypedProtobuf<CMsgGCCStrike15_v2_BetaEnrollment>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_BetaEnrollmentImpl(handle, isManuallyAllocated);


  public uint Eresult { get; set; }

}
