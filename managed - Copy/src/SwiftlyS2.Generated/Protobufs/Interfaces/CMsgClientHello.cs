
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgClientHello : ITypedProtobuf<CMsgClientHello>
{
  static CMsgClientHello ITypedProtobuf<CMsgClientHello>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgClientHelloImpl(handle, isManuallyAllocated);


  public uint Version { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CMsgSOCacheHaveVersion> SocacheHaveVersions { get; }


  public uint ClientSessionNeed { get; set; }


  public uint ClientLauncher { get; set; }


  public uint PartnerSrcid { get; set; }


  public uint PartnerAccountid { get; set; }


  public uint PartnerAccountflags { get; set; }


  public uint PartnerAccountbalance { get; set; }


  public uint SteamLauncher { get; set; }

}
