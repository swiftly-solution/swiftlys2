
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_GC2ClientRefuseSecureMode : ITypedProtobuf<CMsgGCCStrike15_v2_GC2ClientRefuseSecureMode>
{
  static CMsgGCCStrike15_v2_GC2ClientRefuseSecureMode ITypedProtobuf<CMsgGCCStrike15_v2_GC2ClientRefuseSecureMode>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_GC2ClientRefuseSecureModeImpl(handle, isManuallyAllocated);


  public string FileReport { get; set; }


  public bool OfferInsecureMode { get; set; }


  public bool OfferSecureMode { get; set; }


  public bool ShowUnsignedUi { get; set; }


  public bool KickUser { get; set; }


  public bool ShowTrustedUi { get; set; }


  public bool ShowWarningNotTrusted { get; set; }


  public bool ShowWarningNotTrusted2 { get; set; }


  public string FilesPreventedTrusted { get; set; }

}
