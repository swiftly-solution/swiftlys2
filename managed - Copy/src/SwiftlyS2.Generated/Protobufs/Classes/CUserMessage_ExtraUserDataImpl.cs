
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessage_ExtraUserDataImpl : NetMessage<CUserMessage_ExtraUserData>, CUserMessage_ExtraUserData
{
  public CUserMessage_ExtraUserDataImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Item
  { get => Accessor.GetInt32("item"); set => Accessor.SetInt32("item", value); }


  public long Value1
  { get => Accessor.GetInt64("value1"); set => Accessor.SetInt64("value1", value); }


  public long Value2
  { get => Accessor.GetInt64("value2"); set => Accessor.SetInt64("value2", value); }


  public IProtobufRepeatedFieldValueType<byte[]> Detail1
  { get => new ProtobufRepeatedFieldValueType<byte[]>(Accessor, "detail1"); }


  public IProtobufRepeatedFieldValueType<byte[]> Detail2
  { get => new ProtobufRepeatedFieldValueType<byte[]>(Accessor, "detail2"); }

}
