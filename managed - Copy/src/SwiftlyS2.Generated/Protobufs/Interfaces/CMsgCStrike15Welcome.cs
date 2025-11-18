
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgCStrike15Welcome : ITypedProtobuf<CMsgCStrike15Welcome>
{
  static CMsgCStrike15Welcome ITypedProtobuf<CMsgCStrike15Welcome>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgCStrike15WelcomeImpl(handle, isManuallyAllocated);


  public uint StoreItemHash { get; set; }


  public uint Timeplayedconsecutively { get; set; }


  public uint TimeFirstPlayed { get; set; }


  public uint LastTimePlayed { get; set; }


  public uint LastIpAddress { get; set; }


  public ulong Gscookieid { get; set; }


  public ulong Uniqueid { get; set; }

}
