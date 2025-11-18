
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientAuthKeyCode : ITypedProtobuf<CMsgGCCStrike15_v2_ClientAuthKeyCode>
{
  static CMsgGCCStrike15_v2_ClientAuthKeyCode ITypedProtobuf<CMsgGCCStrike15_v2_ClientAuthKeyCode>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientAuthKeyCodeImpl(handle, isManuallyAllocated);


  public uint Eventid { get; set; }


  public string Code { get; set; }

}
