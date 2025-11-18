
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCItemCustomizationNotificationImpl : TypedProtobuf<CMsgGCItemCustomizationNotification>, CMsgGCItemCustomizationNotification
{
  public CMsgGCItemCustomizationNotificationImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldValueType<ulong> ItemId
  { get => new ProtobufRepeatedFieldValueType<ulong>(Accessor, "item_id"); }


  public uint Request
  { get => Accessor.GetUInt32("request"); set => Accessor.SetUInt32("request", value); }


  public IProtobufRepeatedFieldValueType<ulong> ExtraData
  { get => new ProtobufRepeatedFieldValueType<ulong>(Accessor, "extra_data"); }

}
