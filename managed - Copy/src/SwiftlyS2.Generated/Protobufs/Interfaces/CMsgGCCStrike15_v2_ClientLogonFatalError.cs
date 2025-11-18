
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_ClientLogonFatalError : ITypedProtobuf<CMsgGCCStrike15_v2_ClientLogonFatalError>
{
  static CMsgGCCStrike15_v2_ClientLogonFatalError ITypedProtobuf<CMsgGCCStrike15_v2_ClientLogonFatalError>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_ClientLogonFatalErrorImpl(handle, isManuallyAllocated);


  public uint Errorcode { get; set; }


  public string Message { get; set; }


  public string Country { get; set; }

}
