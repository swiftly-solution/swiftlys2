
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface MLDemoHeader : ITypedProtobuf<MLDemoHeader>
{
  static MLDemoHeader ITypedProtobuf<MLDemoHeader>.Wrap(nint handle, bool isManuallyAllocated) => new MLDemoHeaderImpl(handle, isManuallyAllocated);


  public string MapName { get; set; }


  public int TickRate { get; set; }


  public uint Version { get; set; }


  public uint SteamUniverse { get; set; }

}
