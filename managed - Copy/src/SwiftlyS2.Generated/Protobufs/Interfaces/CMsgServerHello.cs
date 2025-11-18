
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgServerHello : ITypedProtobuf<CMsgServerHello>
{
  static CMsgServerHello ITypedProtobuf<CMsgServerHello>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgServerHelloImpl(handle, isManuallyAllocated);


  public uint Version { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CMsgSOCacheHaveVersion> SocacheHaveVersions { get; }


  public uint LegacyClientSessionNeed { get; set; }


  public uint ClientLauncher { get; set; }


  public byte[] LegacySteamdatagramRouting { get; set; }


  public uint RequiredInternalAddr { get; set; }


  public byte[] SteamdatagramLogin { get; set; }


  public uint SocacheControl { get; set; }

}
