
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_AccountPrivacySettings_SettingImpl : TypedProtobuf<CMsgGCCStrike15_v2_AccountPrivacySettings_Setting>, CMsgGCCStrike15_v2_AccountPrivacySettings_Setting
{
  public CMsgGCCStrike15_v2_AccountPrivacySettings_SettingImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint SettingType
  { get => Accessor.GetUInt32("setting_type"); set => Accessor.SetUInt32("setting_type", value); }


  public uint SettingValue
  { get => Accessor.GetUInt32("setting_value"); set => Accessor.SetUInt32("setting_value", value); }

}
