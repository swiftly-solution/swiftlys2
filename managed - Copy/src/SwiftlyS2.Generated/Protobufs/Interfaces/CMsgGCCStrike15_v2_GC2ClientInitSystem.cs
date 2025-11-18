
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_GC2ClientInitSystem : ITypedProtobuf<CMsgGCCStrike15_v2_GC2ClientInitSystem>
{
  static CMsgGCCStrike15_v2_GC2ClientInitSystem ITypedProtobuf<CMsgGCCStrike15_v2_GC2ClientInitSystem>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_GC2ClientInitSystemImpl(handle, isManuallyAllocated);


  public bool Load { get; set; }


  public string Name { get; set; }


  public string Outputname { get; set; }


  public byte[] KeyData { get; set; }


  public byte[] ShaHash { get; set; }


  public int Cookie { get; set; }


  public string Manifest { get; set; }


  public byte[] SystemPackage { get; set; }


  public bool LoadSystem { get; set; }

}
