
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface TournamentTeam : ITypedProtobuf<TournamentTeam>
{
  static TournamentTeam ITypedProtobuf<TournamentTeam>.Wrap(nint handle, bool isManuallyAllocated) => new TournamentTeamImpl(handle, isManuallyAllocated);


  public int TeamId { get; set; }


  public string TeamTag { get; set; }


  public string TeamFlag { get; set; }


  public string TeamName { get; set; }


  public IProtobufRepeatedFieldSubMessageType<TournamentPlayer> Players { get; }

}
