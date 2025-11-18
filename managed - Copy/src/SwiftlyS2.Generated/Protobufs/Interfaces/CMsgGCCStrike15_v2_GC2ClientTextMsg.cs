
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_GC2ClientTextMsg : ITypedProtobuf<CMsgGCCStrike15_v2_GC2ClientTextMsg>
{
  static CMsgGCCStrike15_v2_GC2ClientTextMsg ITypedProtobuf<CMsgGCCStrike15_v2_GC2ClientTextMsg>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_GC2ClientTextMsgImpl(handle, isManuallyAllocated);


  public uint Id { get; set; }


  public uint Type { get; set; }


  public byte[] Payload { get; set; }

}
