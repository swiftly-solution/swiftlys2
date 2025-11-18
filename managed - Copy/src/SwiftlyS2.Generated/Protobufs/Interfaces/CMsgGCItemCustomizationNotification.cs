
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCItemCustomizationNotification : ITypedProtobuf<CMsgGCItemCustomizationNotification>
{
  static CMsgGCItemCustomizationNotification ITypedProtobuf<CMsgGCItemCustomizationNotification>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCItemCustomizationNotificationImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldValueType<ulong> ItemId { get; }


  public uint Request { get; set; }


  public IProtobufRepeatedFieldValueType<ulong> ExtraData { get; }

}
