
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCClientDisplayNotificationImpl : TypedProtobuf<CMsgGCClientDisplayNotification>, CMsgGCClientDisplayNotification
{
  public CMsgGCClientDisplayNotificationImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string NotificationTitleLocalizationKey
  { get => Accessor.GetString("notification_title_localization_key"); set => Accessor.SetString("notification_title_localization_key", value); }


  public string NotificationBodyLocalizationKey
  { get => Accessor.GetString("notification_body_localization_key"); set => Accessor.SetString("notification_body_localization_key", value); }


  public IProtobufRepeatedFieldValueType<string> BodySubstringKeys
  { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "body_substring_keys"); }


  public IProtobufRepeatedFieldValueType<string> BodySubstringValues
  { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "body_substring_values"); }

}
