
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_AccountPrivacySettingsImpl : TypedProtobuf<CMsgGCCStrike15_v2_AccountPrivacySettings>, CMsgGCCStrike15_v2_AccountPrivacySettings
{
  public CMsgGCCStrike15_v2_AccountPrivacySettingsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_AccountPrivacySettings_Setting> Settings
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_AccountPrivacySettings_Setting>(Accessor, "settings"); }

}
