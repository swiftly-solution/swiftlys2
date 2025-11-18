
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDemoSaveGame : ITypedProtobuf<CDemoSaveGame>
{
  static CDemoSaveGame ITypedProtobuf<CDemoSaveGame>.Wrap(nint handle, bool isManuallyAllocated) => new CDemoSaveGameImpl(handle, isManuallyAllocated);


  public byte[] Data { get; set; }


  public ulong SteamId { get; set; }


  public ulong Signature { get; set; }


  public int Version { get; set; }

}
