
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_GC2ClientRefuseSecureModeImpl : TypedProtobuf<CMsgGCCStrike15_v2_GC2ClientRefuseSecureMode>, CMsgGCCStrike15_v2_GC2ClientRefuseSecureMode
{
  public CMsgGCCStrike15_v2_GC2ClientRefuseSecureModeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string FileReport
  { get => Accessor.GetString("file_report"); set => Accessor.SetString("file_report", value); }


  public bool OfferInsecureMode
  { get => Accessor.GetBool("offer_insecure_mode"); set => Accessor.SetBool("offer_insecure_mode", value); }


  public bool OfferSecureMode
  { get => Accessor.GetBool("offer_secure_mode"); set => Accessor.SetBool("offer_secure_mode", value); }


  public bool ShowUnsignedUi
  { get => Accessor.GetBool("show_unsigned_ui"); set => Accessor.SetBool("show_unsigned_ui", value); }


  public bool KickUser
  { get => Accessor.GetBool("kick_user"); set => Accessor.SetBool("kick_user", value); }


  public bool ShowTrustedUi
  { get => Accessor.GetBool("show_trusted_ui"); set => Accessor.SetBool("show_trusted_ui", value); }


  public bool ShowWarningNotTrusted
  { get => Accessor.GetBool("show_warning_not_trusted"); set => Accessor.SetBool("show_warning_not_trusted", value); }


  public bool ShowWarningNotTrusted2
  { get => Accessor.GetBool("show_warning_not_trusted_2"); set => Accessor.SetBool("show_warning_not_trusted_2", value); }


  public string FilesPreventedTrusted
  { get => Accessor.GetString("files_prevented_trusted"); set => Accessor.SetString("files_prevented_trusted", value); }

}
