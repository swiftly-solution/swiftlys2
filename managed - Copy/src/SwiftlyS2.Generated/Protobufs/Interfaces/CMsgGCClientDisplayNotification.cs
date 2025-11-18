
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCClientDisplayNotification : ITypedProtobuf<CMsgGCClientDisplayNotification>
{
  static CMsgGCClientDisplayNotification ITypedProtobuf<CMsgGCClientDisplayNotification>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCClientDisplayNotificationImpl(handle, isManuallyAllocated);


  public string NotificationTitleLocalizationKey { get; set; }


  public string NotificationBodyLocalizationKey { get; set; }


  public IProtobufRepeatedFieldValueType<string> BodySubstringKeys { get; }


  public IProtobufRepeatedFieldValueType<string> BodySubstringValues { get; }

}
