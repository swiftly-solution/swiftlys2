
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_GC2ClientInitSystem_Response : ITypedProtobuf<CMsgGCCStrike15_v2_GC2ClientInitSystem_Response>
{
  static CMsgGCCStrike15_v2_GC2ClientInitSystem_Response ITypedProtobuf<CMsgGCCStrike15_v2_GC2ClientInitSystem_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_GC2ClientInitSystem_ResponseImpl(handle, isManuallyAllocated);


  public bool Success { get; set; }


  public string Diagnostic { get; set; }


  public byte[] ShaHash { get; set; }


  public int Response { get; set; }


  public int ErrorCode1 { get; set; }


  public int ErrorCode2 { get; set; }


  public long Handle { get; set; }


  public EInitSystemResult EinitResult { get; set; }


  public int AuxSystem1 { get; set; }


  public int AuxSystem2 { get; set; }

}
