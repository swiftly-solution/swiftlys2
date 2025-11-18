
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_Client2GCTextMsgImpl : TypedProtobuf<CMsgGCCStrike15_v2_Client2GCTextMsg>, CMsgGCCStrike15_v2_Client2GCTextMsg
{
  public CMsgGCCStrike15_v2_Client2GCTextMsgImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Id
  { get => Accessor.GetUInt32("id"); set => Accessor.SetUInt32("id", value); }


  public IProtobufRepeatedFieldValueType<byte[]> Args
  { get => new ProtobufRepeatedFieldValueType<byte[]>(Accessor, "args"); }

}
