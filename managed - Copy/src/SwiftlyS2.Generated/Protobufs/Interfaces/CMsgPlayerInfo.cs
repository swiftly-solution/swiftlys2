
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgPlayerInfo : ITypedProtobuf<CMsgPlayerInfo>
{
  static CMsgPlayerInfo ITypedProtobuf<CMsgPlayerInfo>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgPlayerInfoImpl(handle, isManuallyAllocated);


  public string Name { get; set; }


  public ulong Xuid { get; set; }


  public int Userid { get; set; }


  public ulong Steamid { get; set; }


  public bool Fakeplayer { get; set; }


  public bool Ishltv { get; set; }

}
