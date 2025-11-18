
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageTextMsgImpl : NetMessage<CUserMessageTextMsg>, CUserMessageTextMsg
{
  public CUserMessageTextMsgImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint Dest
  { get => Accessor.GetUInt32("dest"); set => Accessor.SetUInt32("dest", value); }


  public IProtobufRepeatedFieldValueType<string> Param
  { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "param"); }

}
