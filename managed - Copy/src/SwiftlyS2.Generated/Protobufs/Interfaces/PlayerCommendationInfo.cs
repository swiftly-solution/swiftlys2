
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface PlayerCommendationInfo : ITypedProtobuf<PlayerCommendationInfo>
{
  static PlayerCommendationInfo ITypedProtobuf<PlayerCommendationInfo>.Wrap(nint handle, bool isManuallyAllocated) => new PlayerCommendationInfoImpl(handle, isManuallyAllocated);


  public uint CmdFriendly { get; set; }


  public uint CmdTeaching { get; set; }


  public uint CmdLeader { get; set; }

}
