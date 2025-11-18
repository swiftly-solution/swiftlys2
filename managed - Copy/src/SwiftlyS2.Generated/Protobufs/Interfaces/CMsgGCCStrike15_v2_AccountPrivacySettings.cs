
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_AccountPrivacySettings : ITypedProtobuf<CMsgGCCStrike15_v2_AccountPrivacySettings>
{
  static CMsgGCCStrike15_v2_AccountPrivacySettings ITypedProtobuf<CMsgGCCStrike15_v2_AccountPrivacySettings>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_AccountPrivacySettingsImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_AccountPrivacySettings_Setting> Settings { get; }

}
