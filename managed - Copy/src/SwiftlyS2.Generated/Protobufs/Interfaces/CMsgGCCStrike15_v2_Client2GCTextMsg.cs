
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_Client2GCTextMsg : ITypedProtobuf<CMsgGCCStrike15_v2_Client2GCTextMsg>
{
  static CMsgGCCStrike15_v2_Client2GCTextMsg ITypedProtobuf<CMsgGCCStrike15_v2_Client2GCTextMsg>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_Client2GCTextMsgImpl(handle, isManuallyAllocated);


  public uint Id { get; set; }


  public IProtobufRepeatedFieldValueType<byte[]> Args { get; }

}
