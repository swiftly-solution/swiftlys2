
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_GC2ClientTextMsgImpl : TypedProtobuf<CMsgGCCStrike15_v2_GC2ClientTextMsg>, CMsgGCCStrike15_v2_GC2ClientTextMsg
{
  public CMsgGCCStrike15_v2_GC2ClientTextMsgImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Id
  { get => Accessor.GetUInt32("id"); set => Accessor.SetUInt32("id", value); }


  public uint Type
  { get => Accessor.GetUInt32("type"); set => Accessor.SetUInt32("type", value); }


  public byte[] Payload
  { get => Accessor.GetBytes("payload"); set => Accessor.SetBytes("payload", value); }

}
